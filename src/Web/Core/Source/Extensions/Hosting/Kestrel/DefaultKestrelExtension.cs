﻿namespace DotNetCenter.Beyond.Web.Core.Extensions.Hosting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using DotNetCenter.Beyond.Web.Core.Common;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    public static class DefaultKestrelExtension
    {

        public static IWebHostBuilder UseDefaultKestrel(
                                                         this IWebHostBuilder host
                                                         , IPEndPoint endPoint = null
                                                         , Action<ListenOptions> listOptions = null
                                                         , long maxConcurrentConnections = 100
                                                         , long maxConcurrentUpgradedConnections = 100
                                                         , long maxRequestBodySize = 10 * 1024
                                                         , double minRequestBodyDataRateBytesPerSecond = 240
                                                         , int minRequestBodyDataRateGracePeriod = 10
                                                         , int minResponseDataRateGracePeriod = 10
                                                         , double minResponseBodyDataRateBytesPerSecond = 100
                                                         , int limitKeepAliveTimeout  = 30
                                                         , int RequestHeadersTimeout = 30
            )
        {
            host
                .UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = maxConcurrentConnections;
                    options.Limits.MaxConcurrentUpgradedConnections = maxConcurrentUpgradedConnections;
                    options.Limits.MaxRequestBodySize = maxRequestBodySize;
                    options.Limits.MinRequestBodyDataRate =
                        new MinDataRate(bytesPerSecond: minRequestBodyDataRateBytesPerSecond, gracePeriod: TimeSpan.FromSeconds(minRequestBodyDataRateGracePeriod));
                    options.Limits.MinResponseDataRate =
                        new MinDataRate(bytesPerSecond: minResponseBodyDataRateBytesPerSecond, gracePeriod: TimeSpan.FromSeconds(minResponseDataRateGracePeriod));
                    options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(limitKeepAliveTimeout);
                    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(RequestHeadersTimeout);
                    if (endPoint != null)
                        options.Listen(endPoint);
                    if(listOptions != null)
                        options.ConfigureEndpointDefaults(listOptions);
                });
            return host;
        }

        public static IWebHostBuilder CreateDotNetCenterWebHostBuilder<TStartup>(
                                                string envName
                                                , string contentRootPath
                                                , string webHostBuilderBasePath
                                                , string[] args
                                                , IPEndPoint endPoint = null
                                                , Action<ListenOptions> listOptions = null
                                                , long maxConcurrentConnections = 1024
                                                , long maxConcurrentUpgradedConnections = 100
                                                , long maxRequestBodySize = 10 * 1024
                                                , double minRequestBodyDataRateBytesPerSecond = 100
                                                , int minRequestBodyDataRate = 10
                                                , double minResponseBodyDataRateBytesPerSecond = 100
                                                , int minResponseDataRateGracePeriod = 10
)
    where TStartup :
    class, IAppStartup
        {
            return new WebHostBuilder()
         .UseContentRoot(contentRootPath)
         .UseEnvironment(envName)
         .ConfigureDefaultAppConfiguration(webHostBuilderBasePath, args)
         .UseDefaultKestrel(
                endPoint,
                listOptions,
                maxConcurrentConnections,
                maxConcurrentUpgradedConnections,
                maxRequestBodySize,
                minRequestBodyDataRateBytesPerSecond,
                minRequestBodyDataRate,
                minResponseDataRateGracePeriod,
                minResponseBodyDataRateBytesPerSecond)
        .UseIISIntegration()
        .ConfigureDefaultLogging()
        .UseStartup<TStartup>();
        }
    }
}



//public static async Task Main(string[] args)
//{
//    var host = CreateWebHostBuilder(args).Build();
//    await host.RunAsync();
//}
//public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//new WebHostBuilder()
//.UseContentRoot($"{Environment.CurrentDirectory}")
//.UseEnvironment($"{Environments.Development}")
//.ConfigureAppConfiguration((hostContext, configApp) =>
//{
//   configApp.SetBasePath(Directory.GetCurrentDirectory());
//   configApp.AddJsonFile("appsettings.json", optional: true);
//   configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
//   configApp.AddCommandLine(args);
//})
//.UseKestrel(options =>
//{
//    options.Limits.MaxConcurrentConnections = 100;
//    options.Limits.MaxConcurrentUpgradedConnections = 100;
//    options.Limits.MaxRequestBodySize = 10 * 1024;
//    options.Limits.MinRequestBodyDataRate =
//                new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
//    options.Limits.MinResponseDataRate =
//                new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
//            //options.Listen(IPAddress.Loopback, 80);
//            options.ConfigureEndpointDefaults(options =>
//    {

//    });
//})
//.UseIISIntegration()
//.ConfigureLogging((hostingContext, logging) =>
//{
//    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
//    logging.AddConsole();
//    logging.AddDebug();
//})
//.UseStartup<Startup>();
//    }