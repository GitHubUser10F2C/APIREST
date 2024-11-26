using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.src.DTOs;
using ApiRest.src.Interfaces;
using ApiRest.src.Mappers;
using ApiRest.src.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            if (page < 1 || limit < 1)
            {
                return BadRequest("Page and limit must be greater than 0.");
            }

            try
            {
                var users = await _userRepository.GetAll(page, limit);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el email ya está registrado
            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email already registered.");
            }

            // Crear el nuevo usuario
            var newUser = new AppUser
            {
                UserName = userDto.Email,
                Nombre = userDto.Nombre,
                Apellidos = userDto.Apellidos,
                Email = userDto.Email,
            };

            var result = await _userManager.CreateAsync(newUser, userDto.Password);

            if (!result.Succeeded)
            {
                return StatusCode(500, "Internal server error creating user.");
            }

            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser.ToUserDto());
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Buscar al usuario por ID
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!string.IsNullOrEmpty(updateUserDto.Email) && updateUserDto.Email != user.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(updateUserDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email already registered.");
                }
                user.Email = updateUserDto.Email;
            }
            if (!string.IsNullOrEmpty(updateUserDto.Nombre))
            {
                user.Nombre = updateUserDto.Nombre;
            }
            if (!string.IsNullOrEmpty(updateUserDto.Apellidos))
            {
                user.Apellidos = updateUserDto.Apellidos;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(500, "Internal server error creating user.");
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.EstaEliminado = true;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return StatusCode(500, "Internal server error. Could not update user.");
        }
    }
}