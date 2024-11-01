using APIGestionInventario.Interfaces;
using APIGestionInventario.Models;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGestionInventario.BAL.JWT
{
    public class JWTServices : IJWTServices
    {
        private readonly IConfiguration _IConfiguration;
        public JWTServices(IConfiguration configuration)
        {
            _IConfiguration = configuration;
        }

        public string CrearTokenJWT(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_IConfiguration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new(ClaimTypes.NameIdentifier, usuario.UsuarioId),
                    new(ClaimTypes.Role, usuario.Roles.RolNombre),
                ]),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _IConfiguration["Jwt:Issuer"],
                Audience = _IConfiguration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        public string? ObtenerClaimJWT(HttpRequest httpRequest, string claimNombre)
        {
            var stream = httpRequest.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            return tokenS?.Claims.First(claim => claim.Type == claimNombre).Value;

        }
    }
}
