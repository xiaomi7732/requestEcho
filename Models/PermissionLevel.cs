using System;

namespace Microsoft.RequestEcho
{
    [Flags]
    public enum PermissionLevel
    {
        /// <summary>Readonly permission.</summary>
        ReadOnly = 1,
        
        /// <summary>Readwrite permission.</summary>
        ReadWrite = 3,
    }
}