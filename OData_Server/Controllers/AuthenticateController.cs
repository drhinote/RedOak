using Microsoft.IdentityModel.Tokens;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OData_Server
{
    public class AuthenticateController : ApiController
    {
      
        public class UserData
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public IHttpActionResult Post([FromBody] UserData data)
        {
            using (var ctx = new RoiDb())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                var user = ctx.Testers.FirstOrDefault(u => u.Name == data.UserName && u.Active);
                if (user == null) return NotFound();
                if (!user.TestPassword(data.Password)) return NotFound();
                ctx.Entry(user).Collection(u => u.Companies).Load();
                return Ok(new { data.UserName, user.Companies, token = GenerateToken(data.UserName, user.Companies) });
            }
        }

               
        //aCob14AVV0pBN2KxIzadHOr1YGdVU2zwztulCedA8U0bAtNREq7cM4HlHiMiAJJc
        private const string Secret = "YUNvYjE0QVZWMHBCTjJLeEl6YWRIT3IxWUdkVlUyend6dHVsQ2VkQThVMGJBdE5SRXE3Y000SGxIaU1pQUpKYw==";

        public static string GenerateToken(string username, ICollection<Company> companies, int expireMinutes = 600)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var claims = new List<Claim>();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(companies.Select(c => new Claim(ClaimTypes.GroupSid, c.Id.ToString())).Concat(new[] { new Claim(ClaimTypes.Name, username) }).ToList()),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
