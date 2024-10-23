using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Models.DTO.Auth;
using Server.Models.Utilities;
using Server.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace Server.Services
{
    public class AuthService(
        UserRepository userRep,
        UserFactory userFactory,
        IOptions<JwtSettings> settings)
    {

        public async Task<LoginResponseDTO?> LoginUser(LoginRequestDTO dto)
        {
            var character = await userRep.LoginUser(dto);
            if (character == null) return null;

            userFactory.LoginUser(character);
            return new()
            {
                Token = AuthUser(character.Name),
                Place = character.Place,
            };
        }

        public async Task<bool> RegistrationNewUser(RegRequestDTO dto)
        {
            if (await userRep.IsUniqueUser(dto) || !await userRep.Registration(dto))
            {
                return false;
            }
            return true;
        }

        private string AuthUser(string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.Value.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, name),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(settings.Value.ExpirationMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}

