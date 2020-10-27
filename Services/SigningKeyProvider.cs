namespace Microsoft.RequestEcho
{

    class SigningKeyProvider : ISigningKeyProvider
    {
        public string SigningKey => "Make this a secret from key vault that only this service can access, and use it as the key to do the protection. This key needs to be very well protected!";
    }
}