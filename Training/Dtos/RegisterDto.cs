using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.WebApi.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CreatorId { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }
    }
}
