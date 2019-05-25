using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryLab.TokenUtil
{
    public class JwtProvider
    {      

        public TokenData DecodeJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email").Value;
            var roleName = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

            if (email != null && roleName != null)
            {
                return new TokenData
                {
                    Email = email,
                    Role = roleName
                };
            }
            else
            {
                return null;
            }
        }

        public TokenData DecodeJwt(StringValues authHeader)
        {
            var stringHeader = authHeader.ToString();
            var handler = new JwtSecurityTokenHandler();
            var token = stringHeader.Substring("Bearer ".Length);
            return DecodeJwt(token);
        }

    }
}

