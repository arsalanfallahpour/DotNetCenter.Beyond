﻿ namespace DotNetCenter.Beyond.Web.Core.Common.DIContainerServices.DependencyContainers
{
    using DotNetCenter.Beyond.Web.Core.Common.DIContainerServices.Services;
    using DotNetCenter.Beyond.Web.Core.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AspNetCoreDIC
    {
        public void AddAspNetCoreService(ref IServiceCollection services)
        {
            services.AddTransient<SupportHttpContextServices, HttpContextServices>();
            services.AddTransient(typeof(SupportLoggingServices<>), typeof(LoggingService<>));
            services.TryAddLoggerProviderServices();
        }
    }
}
