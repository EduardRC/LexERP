using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexERP.Server.Helpers;
using LexERP.Server.Models;

namespace LexERP.Server.Data
{
    public class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<SeedData> logger)
        {
            // Crear roles basicos si no estan creados
            if (!context.Roles.Any())
            {
                await CreateDefaultRoles(roleManager, logger);
            }

            // crear usuarios basicos si no estan creados
            if (!context.Users.Any())
            {
                await CreateDefaultUsers(userManager, logger);
            }
        }

        private static async Task CreateDefaultRoles(RoleManager<ApplicationRole> roleManager, ILogger<SeedData> logger)
        {
            await CreateRole(roleManager, logger, "admin");
            await CreateRole(roleManager, logger, "user");
        }

        private static async Task CreateRole(RoleManager<ApplicationRole> roleManager, ILogger<SeedData> logger, string roleName)
        {
            logger.LogInformation($"Create the role `{roleName}` for application");

            var role = new ApplicationRole(roleName);
            var ir = await roleManager.CreateAsync(role);

            if (ir.Succeeded)
            {
                logger.LogDebug($"Created the role `{roleName}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default role `{roleName}` cannot be created");
                logger.LogError(exception, ApplicationFunctions.GetIdentityErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task CreateDefaultUsers(UserManager<ApplicationUser> userManager, ILogger<SeedData> logger)
        {
            await CreateUser(userManager, logger, "admin@admin.local", "Admin01!", "admin");
        }

        private static async Task CreateUser(UserManager<ApplicationUser> userManager, ILogger<SeedData> logger, string email, string password, string role)
        {
            logger.LogInformation($"Create the user `{email}` for application ");

            var user = new ApplicationUser { UserName = email, Email = email };

            var ir = await userManager.CreateAsync(user, password);

            if (ir.Succeeded)
            {
                logger.LogDebug($"Created the user `{email}` successfully");

                var createdUser = await userManager.FindByNameAsync(email);

                await AddRoleToUser(userManager, logger, createdUser, role);
            }
            else
            {
                var exception = new ApplicationException($"Default user `{email}` cannot be created");
                logger.LogError(exception, ApplicationFunctions.GetIdentityErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task AddRoleToUser(UserManager<ApplicationUser> userManager, ILogger<SeedData> logger, ApplicationUser user, string role)
        {
            logger.LogInformation($"Add default user `{user.UserName}` to role '{role}'");

            var ir = await userManager.AddToRoleAsync(user, role);

            if (ir.Succeeded)
            {
                logger.LogDebug($"Added the role '{role}' to default user `{user.UserName}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"The role `{role}` cannot be set for the user `{user.UserName}`");
                logger.LogError(exception, ApplicationFunctions.GetIdentityErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

    }
}
