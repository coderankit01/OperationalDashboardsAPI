using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Constants
{
    public static class MonitoringConstants
    {
        public static readonly string Ec2Identifier = "InstanceId";
        public static readonly List<string> Ec2Metrics = new List<string>()
        {
            "NetworkOut",
            "NetworkIn",
            "DiskWriteOps",
            "CPUUtilization",
            "DiskReadOps",
            "DiskWriteBytes",
            "DiskReadBytes"
        };
        public static readonly string S3Identifier = "BucketName";
        public static readonly List<string> S3Metrics = new List<string>()
        {
            "NumberOfObjects",
            "BucketSizeBytes"
        };
        public static readonly string RDSIdentifier = "DBInstanceIdentifier";
        public static readonly List<string> RDSMetrics = new List<string>()
        {
            "FreeStorageSpace",
            "DatabaseConnections",
            "WriteLatency",
            "CPUUtilization",
            "WriteIOPS",
            "ReadLatency",
            "ReadIOPS",
            "FreeableMemory"

        };
        public static readonly string APIGatewayIdentifier = "ApiName";
        public static readonly List<string> APIGatewayMetrics = new List<string>()
        {
            "Count",
            "IntegrationLatency",
            "Latency",
            "4XXError"

        };
        public static readonly string EBSIdentifier = "VolumeId";
        public static readonly List<string> EBSMetrics = new List<string>()
        {
            "VolumeReadBytes",
            "VolumeWriteOps",
            "VolumeTotalWriteTime",
            "VolumeTotalReadTime",
             "VolumeReadOps",
            "VolumeWriteBytes"

        };
        public static readonly string CognitoIdentifier = "UserPool";
        public static readonly List<string> CognitoMetrics = new List<string>()
        {
            "TokenRefreshSuccesses",
            "SignInSuccesses",
            "SignUpSuccesses"

        };
        public static readonly string DynamoDBIdentifier = "TableName";
        public static readonly List<string> DynamoDBMetrics = new List<string>()
        {
            "AccountMaxReads",
            "SuccessfulRequestLatency",
            "AccountMaxTableLevelWrites",
            "AccountProvisionedWriteCapacityUtilization",
            "ConsumedWriteCapacityUnits",
            "AccountProvisionedReadCapacityUtilization"

        };
        public static readonly string LambdaIdentifier = "FunctionName";
        public static readonly List<string> LambdaMetrics = new List<string>()
        {
            "Duration",
            "Invocations",
            "Errors",
            "ConcurrentExecutions",
            "Throttles",
            "UnreservedConcurrentExecutions"

        };

        public static readonly Dictionary<string, List<string>> nameSpaceAndMetrics = new Dictionary<string, List<string>>()
        {
            { "AWS/EC2",Ec2Metrics },
            { "AWS/EBS",EBSMetrics},
            { "AWS/S3",S3Metrics},
            {"AWS/RDS",RDSMetrics },
            { "AWS/ApiGateway",APIGatewayMetrics},
            { "AWS/Cognito",CognitoMetrics},
            { "AWS/DynamoDB",DynamoDBMetrics},
            { "AWS/Lambda",LambdaMetrics}
        };

        public static readonly Dictionary<string, string> nameSpaceIdentifiers = new Dictionary<string, string>()
        {
            { "AWS/EC2",Ec2Identifier },
            { "AWS/EBS",EBSIdentifier},
            { "AWS/S3",S3Identifier},
            {"AWS/RDS",RDSIdentifier },
            { "AWS/ApiGateway",APIGatewayIdentifier},
            { "AWS/Cognito",CognitoIdentifier},
            { "AWS/DynamoDB",DynamoDBIdentifier},
            { "AWS/Lambda",LambdaIdentifier}
        };
        public static readonly Dictionary<string, string> nameSpaceAndClassMapping = new Dictionary<string, string>()
        {
            { "AWS/EC2","OperationalDashboard.Web.Api.Core.Services.EC2Operations" },
            { "AWS/EBS","OperationalDashboard.Web.Api.Core.Services.EBSOperations"},
            { "AWS/S3","OperationalDashboard.Web.Api.Core.Services.S3Operations"},
            {"AWS/RDS","OperationalDashboard.Web.Api.Core.Services.RDSOperations" },
            { "AWS/ApiGateway","OperationalDashboard.Web.Api.Core.Services.ApiGatewayOperations"},
            { "AWS/Cognito","OperationalDashboard.Web.Api.Core.Services.CognitoIdentiyProviderOperations"},
            { "AWS/DynamoDB","OperationalDashboard.Web.Api.Core.Services.DynamoDBOperations"},
            { "AWS/Lambda","OperationalDashboard.Web.Api.Core.Services.LambdaOperations"}
        };

    }
    


        

}
