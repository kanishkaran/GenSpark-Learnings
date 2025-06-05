using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HealthCareAPI.Authorization
{
    public class MinExpYears : IAuthorizationRequirement
    {
        public MinExpYears(float minExp)
        {
            MinExp = minExp;
        }   

        public float MinExp { get; set; }
    }
}