using System.Security.Cryptography.X509Certificates;

namespace Microsoft.RequestEcho
{
    public interface IARMClientCertificateValidator
    {
        bool Validate(X509Certificate2 certificate);
    }
}