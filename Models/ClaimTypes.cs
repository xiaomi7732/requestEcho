namespace Microsoft.RequestEcho
{
    static class ClaimTypes
    {
        private const string ClaimTypePrefix = "https://diagservices.azure.com/";
        public const string PermissionLevel = ClaimTypePrefix + "PermissionLevel";
        public const string AppId = ClaimTypePrefix + "AppId";
    }
}