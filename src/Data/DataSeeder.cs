using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest.src.Models;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace ApiRest.src.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();

                if (userManager.Users.Any())
                    return;

                // Faker para generar datos ficticios
                var userFaker = new Faker<AppUser>()
                    .RuleFor(u => u.Nombre, f => f.Name.FirstName())
                    .RuleFor(u => u.Apellidos, f => f.Name.LastName())
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.UserName, (f, u) => u.Email) // El UserName será igual al Email
                    .RuleFor(u => u.EstaEliminado, f => f.Random.Bool(0.1f)); // 10% eliminados, 90% activos

                var users = userFaker.Generate(100);

                // Contraseña por defecto para todos los usuarios
                var defaultPassword = "P4ssw0rd!";

                foreach (var user in users)
                {
                    // Crear usuarios con contraseñas hasheadas
                    userManager.CreateAsync(user, defaultPassword);
                }
            }
        }
    }
}