﻿namespace DotNetCenter.Beyond.Web.Core.Common.DIContainerServices.DependencyContainers
{
    
    using DotNetCenter.Beyond.Web.Core.Common.DIContainerServices.Services;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class HttpContextDIC
    {
            public IServiceCollection AddHttpContextServices(ref IServiceCollection services)
            {
                services.AddTransient<SupportHttpContextServices, HttpContextServices>();
                return services;
            }
    }
}
