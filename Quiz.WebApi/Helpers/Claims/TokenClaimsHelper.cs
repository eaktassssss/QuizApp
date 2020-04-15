using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Quiz.WebApi.Helpers.Claims
{
    public class TokenClaimsHelper :ControllerBase
    {
        public int GetByRequestUserId()
        {
            var claims = User.Claims;
            var claim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return Convert.ToInt32(claim);
        }
    }
}
