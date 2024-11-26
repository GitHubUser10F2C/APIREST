using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.src.Data;
using ApiRest.src.DTOs;
using ApiRest.src.Interfaces;
using ApiRest.src.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.src.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAll(int page, int limit)
        {
            var usersQuery = _context.Users.AsQueryable();
            var skipNumber = (page - 1) * limit;
            var users = await usersQuery
                .Skip(skipNumber)
                .Take(limit)
                .Select(u => u.ToUserDto())
                .ToListAsync();

            return users;
        }

        public async Task<UserDto?> GetById(string id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => u.ToUserDto())
                .FirstOrDefaultAsync();

            return user;
        }
    }
}