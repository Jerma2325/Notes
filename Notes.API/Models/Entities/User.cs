﻿using System.ComponentModel.DataAnnotations;

namespace Notes.API.Models.Entities
{
    public class User
    {
        [Key]
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
