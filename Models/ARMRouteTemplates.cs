using System;
using System.Globalization;

namespace Microsoft.RequestEcho
{
    public static class ARMRouteTemplates
    {
        private const string ProfilerTokenResourceType = "stampToken/";
        private const string Providers = "providers/";
        private const string Subscriptions = "subscriptions/";
        private const string ResourceGroups = "resourceGroups/";
        private const string Components = "components/";
        private const string InsightsProviderName = "Microsoft.Insights/";
        private const string ProfilerProviderName = "Microsoft.Profiler/";

        private const string ResourceGroupTemplate = ResourceGroups + "{resourceGroupName}/";

        private const string AppInsightsComponentTemplate = Components + "{componentName}/";

        private const string MicrosoftInsightsProviderTemplate = Providers + InsightsProviderName;
        private const string MicrosoftProfilerProviderTemplate = Providers + ProfilerProviderName;

        /// <summary>
        /// Gets the route template for resource provider registering.
        /// </summary>
        /// <value></value>
        public const string SubscriptionTemplate = Subscriptions + "{subscriptionId}/";
        
        /// <summary>
        /// Gets the route template for profiler token resource.
        /// </summary>
        public const string ProfilerTokenTemplate =
                SubscriptionTemplate + 
                ResourceGroupTemplate + 
                MicrosoftInsightsProviderTemplate + AppInsightsComponentTemplate + 
                MicrosoftProfilerProviderTemplate + ProfilerTokenResourceType;
    }
}