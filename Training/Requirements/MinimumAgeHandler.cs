using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Training.WebApi.Requirements
{
    public class MinimumAgeHandler : AuthorizationHandler<MininumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeHandler> _logger;

        public MinimumAgeHandler(ILogger<MinimumAgeHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MininumAgeRequirement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);
            if(dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                _logger.LogInformation("Access Granted");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Access Denied");
            }

            return Task.CompletedTask;
        }
    }
}
