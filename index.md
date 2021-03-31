## Welcome to AWS Operational Dashboard API

You can view/Download the code [on GitHub](https://github.com/coderankit01/OperationalDashboardsAPI/) to maintain and preview the content for your website in Markdown files.


### Overview

Monitoring and observability service built for DevOps engineers, developers, site reliability engineers (SREs), and IT managers. CloudWatch provides you with data and actionable insights to monitor your applications, respond to system-wide performance changes, optimize resource utilization, and get a unified view of operational health. CloudWatch collects monitoring and operational data in the form of logs and metrics.

CloudWatch collects monitoring and operational data in the form of logs, metrics, and events, and visualizes it using automated dashboards so you can get a unified view of your AWS resources, applications, and services that run in AWS and on-premises. You can correlate your metrics and logs to better understand the health and performance of your resources. You can also create alarms based on metric value thresholds you specify, or that can watch for anomalous metric behavior based on machine learning algorithms. To take action quickly, you can set up automated actions to notify you if an alarm is triggered and automatically start auto scaling, for example, to help reduce mean-time-to-resolution. You can also dive deep and analyze your metrics, logs, and traces, to better understand how to improve application performance.

It uses Below API to collect the data for monitoring the infrastructure.

# Cost usage Analytics
# Cost Recommendations
# Infrastructure Monitoring 
# Trusted Advisory Checks

Internally it does use the below AWS .net core SDK.
- **[CloudWatch API]**(https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/cloudwatch-getting-metrics-examples.html)
- **[Cost Usage API]**(https://docs.aws.amazon.com/aws-cost-management/latest/APIReference/API_Operations_AWS_Cost_Explorer_Service.html)
- **[Trust Advisory API]**(https://docs.aws.amazon.com/awssupport/latest/APIReference/API_Operations.html)

**Bold** and _Italic_ and `Code` text

![Image](https://cdn.comparitech.com/wp-content/uploads/2018/11/Datadog-AWS-Monitoring-overview-dashboard.webp)
