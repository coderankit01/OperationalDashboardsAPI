using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalDashboard.Web.Api.Core.Models.Request;

namespace OperationDashboard.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterProfile([FromBody]AWSProfileCredentials aWSCredentials)
        {
            try
            {
                using (IAmazonS3 amazonS3 = new AmazonS3Client(aWSCredentials.AccessKey, aWSCredentials.SecretKey,RegionEndpoint.USEast1))
                {
                    try
                    {
                        var s3Resource = await amazonS3.ListBucketsAsync();
                    }
                    catch (Amazon.S3.AmazonS3Exception ex)
                    {
                        if (ex.ErrorCode.Equals("InvalidAccessKeyId") || ex.ErrorCode.Equals("SignatureDoesNotMatch"))
                        {
                            return BadRequest(new { Message = "Invalid access or secret key" });
                        }
                    }

                }
                var netSDKFile = new SharedCredentialsFile();
                CredentialProfile basicProfile;
                if (netSDKFile.TryGetProfile(aWSCredentials.ProfileName, out basicProfile))
                {
                    basicProfile.Options.AccessKey = aWSCredentials.AccessKey;
                    basicProfile.Options.SecretKey = aWSCredentials.SecretKey;

                    netSDKFile.RegisterProfile(basicProfile);
                }
                else
                {
                    CredentialProfileOptions options = new CredentialProfileOptions()
                    {
                        AccessKey = aWSCredentials.AccessKey,
                        SecretKey = aWSCredentials.SecretKey
                    };
                    var profile = new CredentialProfile(aWSCredentials.ProfileName, options);
                    netSDKFile = new SharedCredentialsFile();
                    netSDKFile.RegisterProfile(profile);
                }


                return Ok("Registered");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
        [HttpGet]
        public async Task<IActionResult> GetProfile([FromQuery]string profile)
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials;
            if (chain.TryGetAWSCredentials(profile, out awsCredentials))
            {
                var cred = await awsCredentials.GetCredentialsAsync();
                var accessKey = cred.AccessKey.Substring(0, cred.AccessKey.Length - 3) + "***";
                var secretKey = cred.SecretKey.Substring(0, cred.SecretKey.Length - 3) + "***";
                return Ok(new { AccessKey = accessKey, SecretKey = secretKey });
            }
            return NotFound(new { Message=$"No Credentials Found for Profile:{profile}"});
        }
    }
}