using AuthenticationAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.TableCreation;

namespace AuthenticationAPI.Services
{
    public static class TokenManager
    {

        const string UserId = "UserId";
        const string RoleName = "RoleName";

        public static string GenerateWebToken(User model, AppSettings settings)
        {
            //Create a Claims Set
            var claimsSet = new List<Claim>
            {
                new Claim("UserId", model.UserId.ToString()),
             //   new Claim("RoleName",  model.RoleName)
            };
            //Create an Identity based on the Claims Set
            var userIdentity = new ClaimsIdentity(claimsSet);

            var keybytes = Encoding.UTF8.GetBytes(settings.AppSecret);
            var signInCredetials = new SigningCredentials(key: new SymmetricSecurityKey(keybytes), algorithm: settings.Algorithm);


            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = userIdentity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signInCredetials,
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var pretoken = handler.CreateToken(descriptor);
            var writeableToken = handler.WriteToken(pretoken);
            return writeableToken;

        }
        public static User GetuserFromToken(string token, AppSettings settings, IUserServiceAsync service)
        {
            var keyBytes = Encoding.UTF8.GetBytes(settings.AppSecret);
            var signInKey = new SymmetricSecurityKey(keyBytes);
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = signInKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,

            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(
                token: token,
                validationParameters: validationParameters,
                validatedToken: out SecurityToken validatedToken
                );
            var outputToken = validatedToken as JwtSecurityToken;
            var userId = outputToken.Claims.FirstOrDefault(c => c.Type == UserId)?.Value;

            // disacard variable is denoted with underscore _
            _ = outputToken.Claims.FirstOrDefault(c => c.Type == RoleName)?.Value;  //due to _ the value get discarded or not stored but this statement will get executed
            var user = service.GetUserDetails(Convert.ToInt32(userId)).Result;
            return user;
        }

    }

}
