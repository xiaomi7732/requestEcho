using System;

namespace Microsoft.RequestEcho
{
    public class TokenContract
    {
        public PermissionLevel PermissionLevel { get; set; }
        public Guid AppId { get; set; }
    }
}