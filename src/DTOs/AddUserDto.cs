using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.src.DTOs
{
public class AddUserDto
{
    [MaxLength(15)]
    public required string Nombre { get; set; }
    [MaxLength(100)]
    public required string Apellidos { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    public required string Email { get; set; }

    [MinLength(8)]
    [MaxLength(30)]
    public required string Password { get; set; }
}
}