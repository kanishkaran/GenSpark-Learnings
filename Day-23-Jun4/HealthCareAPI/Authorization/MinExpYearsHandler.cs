using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HealthCareAPI.Authorization
{
    public class MinExpYearsHandler : AuthorizationHandler<MinExpYears>
    {
        private readonly ILogger<MinExpYearsHandler> _logger;

        public MinExpYearsHandler(ILogger<MinExpYearsHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinExpYears requirement)
        {
            var yearsOfExperience = context.User.FindFirst("YearsOfExp");

            if (yearsOfExperience == null || !float.TryParse(yearsOfExperience.Value, out float exp))
                return Task.CompletedTask;

            if (exp >= requirement.MinExp)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}