using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.RequestEcho
{
    class ProfilerTokenService
    {
        private static readonly TimeSpan TokenExpiry = TimeSpan.FromMinutes(5);
        private readonly ISigningKeyProvider _signingKeyProvider;

        public ProfilerTokenService(ISigningKeyProvider signingKeyProvider)
        {
            _signingKeyProvider = signingKeyProvider ?? throw new ArgumentNullException(nameof(signingKeyProvider));
        }

        public string IssueSecurityToken(TokenContract basedOn)
        {
            DateTime accessExpiration = DateTime.UtcNow.Add(TokenExpiry);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: "ApplicationInsightsProfiler",
                audience: basedOn.AppId.ToString("D", CultureInfo.InvariantCulture),
                claims: new[]{
                    new Claim(ClaimTypes.PermissionLevel, ((int)basedOn.PermissionLevel).ToString())
                },
                expires: accessExpiration,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_signingKeyProvider.SigningKey)),
                    algorithm: SecurityAlgorithms.HmacSha256)
                );

            return (new JwtSecurityTokenHandler().WriteToken(jwtToken));
        }
    }
}