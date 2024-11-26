using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.src.DTOs
{
    public class LoginDto
    {

        [EmailAddress]
        [MaxLength(100)]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}