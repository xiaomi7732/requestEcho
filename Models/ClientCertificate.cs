using System;

namespace Microsoft.RequestEcho
{
    public class ClientCertificate
    {
        public string Thumbprint { get; set; }
        public DateTime NotBefore { get; set; }
        
        public DateTime NotAfter { get; set; }
        
        /// <summary>
        /// Gets or sets the certificate in Base64.
        /// </summary>
        /// <value></value>
        public string Certificate { get; set; }
    }
}