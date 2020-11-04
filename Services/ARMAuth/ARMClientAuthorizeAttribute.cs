using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.RequestEcho
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ARMClientAuthorizeAttribute : Attribute, IFilterFactory
    {

        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new ARMClientAuthorizeFilter(serviceProvider.GetRequiredService<IARMClientCertificateValidator>());
        }
    }
}