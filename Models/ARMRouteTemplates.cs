namespace Microsoft.RequestEcho
{
    public static class ARMRouteTemplates
    {
        private const string DiagnosticServicesReadOnlyToken = "generateDiagnosticServiceReadOnlyToken/";
        private const string DiagnosticSErvicesReadWriteToken = "generateDiagnosticServiceReadWriteToken/";
        private const string Providers = "providers/";
        private const string Subscriptions = "subscriptions/";
        private const string ResourceGroups = "resourceGroups/";
        private const string Components = "components/";
        private const string InsightsProviderName = "Microsoft.Insights/";

        private const string ResourceGroupTemplate = ResourceGroups + "{resourceGroupName}/";

        private const string AppInsightsComponentTemplate = Components + "{componentName}/";

        private const string MicrosoftInsightsProviderTemplate = Providers + InsightsProviderName;

        /// <summary>
        /// Gets the route template for resource provider registering.
        /// </summary>
        /// <value></value>
        public const string SubscriptionTemplate = Subscriptions + "{subscriptionId}/";

        /// <summary>
        /// Gets the route template for readonly diagnostic services token resource.
        /// </summary>
        public const string DiagnosticServiceReadOnlyTokenTemplate =
                SubscriptionTemplate +
                ResourceGroupTemplate +
                MicrosoftInsightsProviderTemplate + AppInsightsComponentTemplate + DiagnosticServicesReadOnlyToken;

        /// <summary>
        /// Gets the route template for readwrite diagnostic services token resource.
        /// </summary>
        public const string DiagnosticServiceReadWriteTokenTemplate =
                SubscriptionTemplate +
                ResourceGroupTemplate +
                MicrosoftInsightsProviderTemplate + AppInsightsComponentTemplate + DiagnosticSErvicesReadWriteToken;
    }
}