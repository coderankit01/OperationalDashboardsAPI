﻿using System;
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
        public static readonly string DynamoDBIdentifier = "UserPool";
        public static readonly List<string> DynamoDBMetrics = new List<string>()
        {
            "AccountMaxReads",
            "SuccessfulRequestLatency",
            "AccountMaxTableLevelWrites",
            "AccountProvisionedWriteCapacityUtilization",
            "ConsumedWriteCapacityUnits",
            "AccountProvisionedReadCapacityUtilization"

        };
        public static readonly string LambdaIdentifier = "UserPool";
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

    }


        

}