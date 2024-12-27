using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICoreDapper.Models
{
    public class AppUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserName { get; set; } = string.Empty;

        public string NormalizedUserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string NormalizedEmail { get; set; } = string.Empty;

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { set; get; }

        public string FullName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public int AccessFailedCount { get; set; }
    }
}
