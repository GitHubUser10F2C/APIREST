using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.src.DTOs;

namespace ApiRest.src.Interfaces
{
public interface IUserRepository
{
    Task<List<UserDto>> GetAll(int page, int limit);
    Task<UserDto?> GetById(string id);
}
}