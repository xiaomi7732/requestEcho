using System.Collections.Generic;

namespace Microsoft.RequestEcho
{
    public interface IARMCertCache
    {
        IEnumerable<string> ValidThumbprints { get; }
    }
}