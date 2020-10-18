using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.WebApi.Requirements
{
    public class MininumAgeRequirement: IAuthorizationRequirement
    {
        public int MinimumAge { get; set; }
        public MininumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
