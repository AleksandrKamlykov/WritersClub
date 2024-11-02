using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WritersClub.Models;
using WritersClub.ViewModel;

namespace WritersClub.Auth
{
    public class TokenService
    {
        private readonly string _secret;

        public TokenService(string secret)
        {
            if (string.IsNullOrEmpty(secret) || secret.Length < 32)
            {
                throw new ArgumentException("The secret key must be at least 32 characters long.");
            }
            _secret = secret;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthUserViewModel? GetUserFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);
            try
            {
                Console.WriteLine($"Token: {token}");

                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim == null )
                {
                    throw new Exception("Invalid token claims");
                }

                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new FormatException("Invalid user ID format");
                }


                return new AuthUserViewModel() { Id = userId, Login = "" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }

    }
}
