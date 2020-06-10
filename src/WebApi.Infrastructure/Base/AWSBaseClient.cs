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
                AWSCredentials awsCredentials;
                if (chain.TryGetAWSCredentials("DEV", out awsCredentials))
                {
                    return awsCredentials;
                }
                return awsCredentials;
            }
        }
        public string region{ get; set; }
    }
}
