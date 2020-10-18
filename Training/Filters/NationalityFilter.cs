using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Training.WebApi.Filters
{
    public class NationalityFilter : Attribute, IAuthorizationFilter
    {
        private string _nationalities;
        public NationalityFilter(string nationalities)
        {
            _nationalities = nationalities;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Get Nationality from claim
            var nationality = context.HttpContext.User.FindFirst(c => c.Type == "Nationality").Value;

            var arrNationality = _nationalities.Split(",");

            if(!arrNationality.Any(c => c == nationality))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
