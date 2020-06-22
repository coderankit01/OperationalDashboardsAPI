using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Infrastructure.Base
{
   public  class AWSBaseClient
    {
        private  CredentialProfileStoreChain chain = new CredentialProfileStoreChain();
        protected AWSCredentials awsCredentials
        {
            get
            {
                var sharedFile = new SharedCredentialsFile();
                AWSCredentials credentials=null;
               if ( sharedFile.TryGetProfile(Config.Configurations.Profile, out var profileOptions))
                {
                    if (AWSCredentialsFactory.TryGetAWSCredentials(profileOptions, sharedFile, out  credentials))
                    {
                        return credentials;
                    }
                }
                return credentials;
               
            }
        }
        public string region{ get; set; }
    }
}
