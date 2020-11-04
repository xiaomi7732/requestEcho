using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.RequestEcho
{
    class ARMClientCertificateValidator : IARMClientCertificateValidator
    {
        private readonly IARMCertCache _armCertCache;

        public ARMClientCertificateValidator(IARMCertCache armCertCache)
        {
            _armCertCache = armCertCache ?? throw new System.ArgumentNullException(nameof(armCertCache));
        }

        public bool Validate(X509Certificate2 certificate)
        {
            return _armCertCache.ValidThumbprints.Any(thumbprint => string.Equals(thumbprint, certificate.Thumbprint, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}