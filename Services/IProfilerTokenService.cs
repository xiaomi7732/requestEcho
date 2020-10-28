namespace Microsoft.RequestEcho
{
    public interface IProfilerTokenService
    {
        string IssueSecurityToken(TokenContract basedOn);
    }
}