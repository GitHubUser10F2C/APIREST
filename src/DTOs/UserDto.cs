using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.src.DTOs
{
    public class UserDto
    {
        public required string Id {get; set; } 
        public required string Nombre { get; set; }
        public required string Apellidos { get; set; }
        public required string Email { get; set; }
        public required bool EstaEliminado { get; set; }
    }
}