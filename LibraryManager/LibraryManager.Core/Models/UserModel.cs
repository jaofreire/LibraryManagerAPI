﻿using LibraryManager.Core.Enums;

namespace LibraryManager.Core.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ERoleType Role { get; set; }

    }
}
