using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_chat.Helpers
{
    /// <summary>
    /// JwtService is a helper class that provides methods for generating and verifying JWTs.
    /// </summary>
    public class JwtService
    {
        private string _secureKey = "this is a very very very secure key!";

        /// <summary>
        /// Generate is a method that creates a JWT for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the JWT is being generated.</param>
        /// <returns>A string that represents the generated JWT.</returns>
        public string Generate(int userId) 
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(userId.ToString(),
                null,
                null,
                null,
                DateTime.Now.AddDays(1));

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        /// <summary>
        /// Verify is a method that validates a given JWT.
        /// </summary>
        /// <param name="jwt">The JWT that needs to be validated.</param>
        /// <returns>A JwtSecurityToken object that represents the validated JWT.</returns>
        public JwtSecurityToken Verify(string jwt) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secureKey);
            tokenHandler.ValidateToken(jwt,
                new TokenValidationParameters 
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}