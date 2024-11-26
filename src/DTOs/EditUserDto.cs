using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.src.DTOs
{
public class UpdateUserDto
{
    [MaxLength(15)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Apellidos { get; set; } = string.Empty;
 
    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }
}
}