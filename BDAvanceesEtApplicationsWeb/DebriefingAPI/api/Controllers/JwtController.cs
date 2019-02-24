
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DDDDemo.api.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DDDDemo.api.Controllers
{

    // Ce controller est accessible même aux utilisateurs non identifiés
    // En effet, ces derniers doivent pouvoir demander un token!
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {

        private readonly JwtIssuerOptions _jwtOptions;
        public JwtController(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        // POST api/Jwt
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] DTO.LoginModel loginModel)
        {
            // Depuis ASP.NET Core 2.1, ces deux lignes sont superflues.
            // En effet, le framework prend en charge le retour d'une erreur 400 
            // incluant le détail de cette dernière si la validation ne s'est pas bien déroulée. 
            // Il est possible de désactiver ce mode de fonctionnement. Pour plus d'informations, voir https://docs.microsoft.com/en-us/aspnet/core/web-api/index?view=aspnetcore-2.1#automatic-http-400-responses 
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);

            var repository = new AuthenticationRepository();
            Model.User userFound = repository.GetUsers().FirstOrDefault(user => user.UserName == loginModel.UserName && user.Password == loginModel.Password);
            if (userFound == null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userFound.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                        ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                        ClaimValueTypes.Integer64),
                new Claim(PrivateClaims.UserId,userFound.Id.ToString())
            };

            // rappel: le token est configurable. On y ajoute ce que l'on veut comme claims!
            // un ensemble de nom de claims est "réservé" (voir JwtRegisteredClaimNames)
            // le reste est utilisable à loisir! Voir classe PrivateClaims par exemple. 
            if (userFound.Roles != null)
            {
                userFound.Roles.ToList().ForEach(roleName =>
                claims.Add(new Claim("roles", roleName)));
            }

            //IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Sérialisation et retour
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };
            return Ok(response);
        }
        private static long ToUnixEpochDate(DateTime date)
              => (long)Math.Round((date.ToUniversalTime() -
                                   new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                                  .TotalSeconds);
    }

}
