using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_mit_IndividialAccounts.Configurations;
using WebAPI_mit_IndividialAccounts.Models;

namespace WebAPI_mit_IndividialAccounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;

        public AuthController(IOptionsSnapshot<JwtBearerTokenSettings> jwtTokenOptions, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            jwtBearerTokenSettings = jwtTokenOptions.Value;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDetails userDetails)
        {
            if (!ModelState.IsValid || userDetails == null)
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });

            //Anlegen eines Benutzers
            IdentityUser identityUser = new IdentityUser() { UserName = userDetails.UserName, Email = userDetails.Email };
            IdentityResult result = await userManager.CreateAsync(identityUser, userDetails.Password);

            //Wenn das Anlegen eines User gescheitert ist
            if (!result.Succeeded)
            {
                //Werden alle Fehler von result.Errors in das ModelStateDictonary gespeicher und an den Client zurück gesendet
                ModelStateDictionary dictionary = new ModelStateDictionary();

                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }
                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Reigstration Successful" });
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            IdentityUser identityUser;

            if (!ModelState.IsValid
                 || credentials == null
                 || (identityUser = await ValidateUser(credentials)) == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            //Wenn wir uns hier befinden, sind wir schon Angemeldet

            //Wir wollen unser Token erhalten. 
            object token = GenerateToken(identityUser);
            return Ok(new { Token = token, Message = "Success" });
        }


        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Well, What do you want to do here ?
            // Wait for token to get expired OR 
            // Maintain token cache and invalidate the tokens after logout method is called
            return Ok(new { Token = "", Message = "Logged Out" });
        }


        private async Task<IdentityUser> ValidateUser(LoginCredentials credentials)
        {
            //Gibt es den User in der Datenbank
            IdentityUser identityUser = await userManager.FindByNameAsync(credentials.Username);


            //Wenn ja, prüfe Password
            if (identityUser != null)
            {
                //eingegebenes Password wird mit dem hinterlegten Passwort (im Hash-Form) verglichen
                PasswordVerificationResult result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);

                //Wenn passwort stimmt, gebe identityUser zurück
                return result == PasswordVerificationResult.Failed ? null : identityUser;
            }


            return null;
        }


        private object GenerateToken(IdentityUser identityUser)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //SecurityKey aus appsettings.json
            byte[] key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);


            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email)
                }),

                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }
    }
}
