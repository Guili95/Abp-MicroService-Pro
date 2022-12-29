<<<<<<< HEAD
﻿using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Text;

namespace Guili.Shared.Hosting.AspNetCore
{
    public static class SerilogConfigurationHelper
    {
        public static void Configure(string applicationName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            long mb = 1024 * 1024 * 10;

            string SerilogOutputTemplate = "{NewLine}时间：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}级别：{Level}{NewLine}消息：{Message}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", $"{applicationName}")
            // 输出到elasticsearch
            .WriteTo.Elasticsearch(
                 new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
                 {
                     IndexFormat = "GuiliAbpMicroServicePro-log-{0:yyyy.MM.dd}",
                     AutoRegisterTemplate = true,
                     OverwriteTemplate = true,
                     FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                     AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                     TypeName = null,
                     MinimumLogEventLevel = LogEventLevel.Verbose,
                     EmitEventFailure = EmitEventFailureHandling.RaiseCallback,
                     ModifyConnectionSettings =
                        conn =>
                        {
                            conn.ServerCertificateValidationCallback((source, certificate, chain, sslPolicyErrors) => true);
                            conn.BasicAuthentication(configuration["ElasticSearch:UserName"], configuration["ElasticSearch:PassWord"]);
                            return conn;
                        }
                 })
            //输出到文件
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Debug)
                .WriteTo.File("Logs/Debug/Debug-.txt",
                    fileSizeLimitBytes: mb,
                    rollOnFileSizeLimit: true,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: SerilogOutputTemplate,
                    encoding: Encoding.UTF8))

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Information)
                    .WriteTo.File("Logs/Info/Info-.txt",
                        fileSizeLimitBytes: mb,
                        rollOnFileSizeLimit: true,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: SerilogOutputTemplate,
                        encoding: Encoding.UTF8))

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Warning)
                    .WriteTo.File("Logs/Warn/Warn-.txt",
                        fileSizeLimitBytes: mb,
                        rollOnFileSizeLimit: true,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: SerilogOutputTemplate,
                        encoding: Encoding.UTF8))

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Error || a.Level == LogEventLevel.Fatal)
                    .WriteTo.File("Logs/Error/Error-.txt",
                        fileSizeLimitBytes: mb,
                        rollOnFileSizeLimit: true,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: SerilogOutputTemplate,
                        encoding: Encoding.UTF8))
            //输出到控制台
            .WriteTo.Async(c => c.Console(outputTemplate: SerilogOutputTemplate))
            .CreateLogger();
        }
    }
}
=======
﻿using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Text;

namespace Guili.Shared.Hosting.AspNetCore
{
    public static class SerilogConfigurationHelper
    {
        public static void Configure(string applicationName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            long mb = 1024 * 1024 * 10;

            string SerilogOutputTemplate = "{NewLine}时间：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}级别：{Level}{NewLine}消息：{Message}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", $"{applicationName}")
            // 输出到elasticsearch
            .WriteTo.Elasticsearch(
                 new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
                 {
                     IndexFormat = "GuiliAbpMicroServicePro-log-{0:yyyy.MM.dd}",
                     AutoRegisterTemplate = true,
                     OverwriteTemplate = true,
                     FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                     AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                     TypeName = null,
                     MinimumLogEventLevel = LogEventLevel.Verbose,
                     EmitEventFailure = EmitEventFailureHandling.RaiseCallback,
                     ModifyConnectionSettings =
                        conn =>
                        {
                            conn.ServerCertificateValidationCallback((source, certificate, chain, sslPolicyErrors) => true);
                            conn.BasicAuthentication(configuration["ElasticSearch:UserName"], configuration["ElasticSearch:PassWord"]);
                            return conn;
                        }
                 })
            //输出到文件
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Debug)
                .WriteTo.File("Logs/Debug/Debug-.txt",
                    fileSizeLimitBytes: mb,
                    rollOnFileSizeLimit: true,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: SerilogOutputTemplate,
                    encoding: Encoding.UTF8))

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Information)
                    .WriteTo.File("Logs/Info/Info-.txt",
                        fileSizeLimitBytes: mb,
                        rollOnFileSizeLimit: true,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: SerilogOutputTemplate,
                        encoding: Encoding.UTF8))

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Warning)
                    .WriteTo.File("Logs/Warn/Warn-.txt",
                        fileSizeLimitBytes: mb,
                        rollOnFileSizeLimit: true,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: SerilogOutputTemplate,
                        encoding: Encoding.UTF8))

                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Error || a.Level == LogEventLevel.Fatal)
                    .WriteTo.File("Logs/Error/Error-.txt",
                        fileSizeLimitBytes: mb,
                        rollOnFileSizeLimit: true,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: SerilogOutputTemplate,
                        encoding: Encoding.UTF8))
            //输出到控制台
            .WriteTo.Async(c => c.Console(outputTemplate: SerilogOutputTemplate))
            .CreateLogger();
        }
    }
}
>>>>>>> git/ids4
