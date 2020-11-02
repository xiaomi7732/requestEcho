using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.RequestEcho
{
    class ProfilerTokenService : IProfilerTokenService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        private static readonly TimeSpan TokenExpiry = TimeSpan.FromMinutes(5);
        private readonly ISigningKeyProvider _signingKeyProvider;

        public ProfilerTokenService(ISigningKeyProvider signingKeyProvider)
        {
            _signingKeyProvider = signingKeyProvider ?? throw new ArgumentNullException(nameof(signingKeyProvider));
        }

        public string IssueSecurityToken(TokenContract basedOn)
        {
            DateTime accessExpiration = DateTime.UtcNow.Add(TokenExpiry);
            var appIdString = basedOn.AppId.ToString("D", CultureInfo.InvariantCulture);
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: "ApplicationInsightsProfiler",
                audience: appIdString,
                claims: new[] {
                    new Claim(ClaimTypes.PermissionLevel, ((int)basedOn.PermissionLevel).ToString()),
                    new Claim(ClaimTypes.AppId, appIdString),
                },
                expires: accessExpiration,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_signingKeyProvider.SigningKey)),
                    algorithm: SecurityAlgorithms.HmacSha256)
                );

            return (_tokenHandler.WriteToken(jwtToken));
        }

        public TokenContract ValidateSecurityToken(string accessToken)
        {
            ClaimsPrincipal claims = GetSecurityTokenClaims(accessToken);

            string permissionLevelNumberInString = claims.FindFirstValue(ClaimTypes.PermissionLevel);
            if (int.TryParse(permissionLevelNumberInString, out int permissionLevelInInt))
            {
                PermissionLevel permissionLevel = (PermissionLevel)permissionLevelInInt;
                TokenContract tokenContract = new TokenContract()
                {
                    AppId = new Guid(claims.FindFirstValue(ClaimTypes.AppId)),
                    PermissionLevel = permissionLevel,
                };

                return tokenContract;
            }
            else
            {
                throw new InvalidOperationException($"Unrecognized permission level enum value: {permissionLevelNumberInString}");
            }
        }

        private ClaimsPrincipal GetSecurityTokenClaims(string accessToken)
        {
            JwtSecurityToken jwtSecurityToken = _tokenHandler.ReadJwtToken(accessToken);
            SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_signingKeyProvider.SigningKey));

            ClaimsPrincipal result = _tokenHandler.ValidateToken(accessToken, new TokenValidationParameters()
            {
                ValidIssuer = "ApplicationInsightsProfiler",
                ValidateIssuer = true,
                IssuerSigningKey = key,
                ValidateAudience = false,
            }, out _);

            return result;
        }
    }
}