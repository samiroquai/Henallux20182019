﻿
using System.ComponentModel.DataAnnotations;

namespace DDDDemo.DTO
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

