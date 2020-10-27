namespace Microsoft.RequestEcho
{
    interface ISigningKeyProvider
    {
        string SigningKey { get; }
    }
}