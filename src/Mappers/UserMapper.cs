using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.src.DTOs;
using ApiRest.src.Models;

namespace ApiRest.src.Mappers
{
public static class UserMapper
{
    public static UserDto ToUserDto(this AppUser appUser)
    {
        return new UserDto
        {
            Id = appUser.Id,
            Nombre = appUser.Nombre,
            Apellidos = appUser.Apellidos,
            Email = appUser.Email ?? "default@example.com",
            EstaEliminado = appUser.EstaEliminado
        };
    }
}
}