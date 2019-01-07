namespace Mocanu.Migrations.Identity
{
    using Mocanu.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<Mocanu.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Identity";
        }

        protected override void Seed(Mocanu.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));

            if (!roleManager.RoleExists("User"))
                roleManager.Create(new IdentityRole("User"));

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var PasswordHash = new PasswordHasher();
            if (!context.Users.Any(u => u.UserName == "admin@admin.net"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.net",
                    Email = "admin@admin.net",
                    PasswordHash = PasswordHash.HashPassword("123456")
                };

                UserManager.Create(user);
                UserManager.AddToRole(user.Id, "Admin");

            }

            if (!context.Users.Any(u => u.UserName == "Guest"))
            {
                var user = new ApplicationUser
                {
                    UserName = "Guest",
                    Email = "Guest@a.net",
                    Id = "Guest",
                    PasswordHash = PasswordHash.HashPassword("1")
                };

                UserManager.Create(user);
                UserManager.AddToRole(user.Id, "User");

            }
        }
    }
}
