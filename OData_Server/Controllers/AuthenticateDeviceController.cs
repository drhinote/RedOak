using Microsoft.IdentityModel.Tokens;
using Roi.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace OData_Server
{
    public class AuthenticateDeviceController : ApiController
    {

        public class DeviceData
        {
            public string Serial { get; set; }
            public string CompanyName { get; set; }
            public Guid CompanyId { get; set; }
            public string Token { get; set; }
        }

        public IHttpActionResult Post([FromBody] DeviceData data)
        {
            using (var ctx = new RoiDb())
            {
				var dev = ctx.Devices.FirstOrDefault(u => u.Serial == data.Serial);
				//var dev = ctx.Devices.FirstOrDefault(u => u.Serial == data.Serial && u.Enabled);
				if (dev == null) return NotFound();
				var res = new DeviceData();
				res.Serial = data.Serial;
				res.CompanyName = dev.Company.Name;
				res.CompanyId = dev.CompanyId;
				res.Token = GenerateToken(res.Serial, new List<Company> { dev.Company });
				return Ok(res);
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
