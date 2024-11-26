using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApiRest.src.Models
{
    public class AppUser : IdentityUser
    {
        public required string Nombre {get;set;}
        public required string Apellidos { get; set; }
        public bool EstaEliminado { get; set; } = false;
    }
}