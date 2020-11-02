using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.RequestEcho
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DiagnosticServicesAuthorizeAttribute : Attribute, IFilterFactory
    {
        private readonly PermissionLevel _permissionLevel;

        public DiagnosticServicesAuthorizeAttribute(PermissionLevel permissionLevel)
        {
            _permissionLevel = permissionLevel;
        }

        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new DiagnosticServiceAuthorizeFilter(
                serviceProvider.GetRequiredService<IProfilerTokenService>(),
                _permissionLevel,
                serviceProvider.GetRequiredService<ILogger<DiagnosticServiceAuthorizeFilter>>()
            );
        }
    }
}