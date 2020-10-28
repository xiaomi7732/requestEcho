namespace Microsoft.RequestEcho
{
    static class ClaimTypes
    {
        private const string ClaimTypePrefix = "http://diagservices.azure.com/";
        public const string PermissionLevel = ClaimTypePrefix + "PermissionLevel";
        public const string AppId = ClaimTypePrefix + "AppId";
    }
}