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
                   

                    netSDKFile.UnregisterProfile(aWSCredentials.ProfileName);
                    CredentialProfileOptions options = new CredentialProfileOptions()
                    {
                        AccessKey = aWSCredentials.AccessKey,
                        SecretKey = aWSCredentials.SecretKey
                    };
                    var profile = new CredentialProfile(aWSCredentials.ProfileName, options);
                    netSDKFile = new SharedCredentialsFile();
                    netSDKFile.RegisterProfile(profile);
                }
                else
                {
                    CredentialProfileOptions options = new CredentialProfileOptions()
                    {
                        AccessKey = aWSCredentials.AccessKey,
                        SecretKey = aWSCredentials.SecretKey
                    };
                    var profile = new CredentialProfile(aWSCredentials.ProfileName, options);
                    //netSDKFile = new SharedCredentialsFile(Directory.GetCurrentDirectory()+ @"\Credentials");
                    netSDKFile = new SharedCredentialsFile();
                    netSDKFile.RegisterProfile(profile);
                }


                return Ok("Registered");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message+ex.StackTrace);
            }
          
        }
        [HttpGet]
        public async Task<IActionResult> GetProfile([FromQuery]string profile)
        {
            string path = Directory.GetCurrentDirectory();
           // var sharedFile = new SharedCredentialsFile(path+ @"\Credentials");
            var sharedFile = new SharedCredentialsFile();
            sharedFile.TryGetProfile(profile, out var profileOptions);
            if(AWSCredentialsFactory.TryGetAWSCredentials(profileOptions, sharedFile, out var credentials))
            {
                var cred = await credentials.GetCredentialsAsync();
                var accessKey = cred.AccessKey.Substring(0, cred.AccessKey.Length - 3) + "***";
                var secretKey = cred.SecretKey.Substring(0, cred.SecretKey.Length - 3) + "***";
                return Ok(new { AccessKey = accessKey, SecretKey = secretKey });
            }
            return NotFound(new { Message=$"No Credentials Found for Profile:{profile}"});
        }
        [HttpGet("Environment")]
        public IActionResult GetEnvironment([FromQuery] string Name)
        {
            return Ok( Environment.GetEnvironmentVariable(Name));
        }

    }
}