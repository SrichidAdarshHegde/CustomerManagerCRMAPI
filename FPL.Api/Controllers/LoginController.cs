using FPL.Dal.DataModel;
using System;
using System.Web.Http;
using Application.Security;
using System.Data.Entity;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace FPL.Api.Controllers
{
    public class LoginController : ApiController
    {
       private CustomerManagerEntities db = new CustomerManagerEntities();

        public class User
        {
            public int UserId { get; set; }
            public string userName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginUserModel model)
        {
            var user = await db.Table_UserRegistration.FirstOrDefaultAsync(x => x.UserName == model.UserName);
            if (user == null || user.Password != model.Password)
            {
                return Ok("fail");
            }
            var token = GenerateJwtToken(user);
            var data = new Table_UserRegistrationVM()
            {
                Email = user.Email,
                RoleId = user.RoleId,
                Token = token,
                UserId = user.Id,
                userName = user.UserName,
            };
            return Ok(data);
        }
        public class Table_UserRegistrationVM
        {
            public int UserId { get; set; }
            public string userName { get; set; }
            public string Email { get; set; }
            public string Token { get; set; }

            public int? RoleId { get; set; }

        }

        private string GenerateJwtToken(Table_UserRegistration user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keys = user.Email + user.Email + user.UserName;
                var key = Encoding.ASCII.GetBytes(keys);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[]
                    {
                    new Claim("email", user.Email),
                    new Claim("username", user.UserName),
                    new Claim("roleid", user.RoleId.ToString()),
                    new Claim("userID", user.Id.ToString()),
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
    }
}