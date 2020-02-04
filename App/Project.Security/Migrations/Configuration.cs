using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.Security.Models;

namespace Project.Security.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            AddRoles(context);
            AddResources(context);
            AddPermissions(context);
            AddUsers(context);
        }

        private static void AddUsers(ApplicationDbContext db)
        {
            var roles = db.Roles.ToList();
            var adminRole = roles.FirstOrDefault(x => x.Name == "Admin");
            var systemRole = roles.FirstOrDefault(x => x.Name == "SystemAdmin");

            var id = "";
            var users = new List<ApplicationUser>();

            id = Guid.NewGuid().ToString();
            users.Add(new ApplicationUser() { Id = id, FirstName = "System", LastName = "Admin", Email = "system@admin.com", UserName = "sysadmin", PasswordHash = new PasswordHasher().HashPassword("123qwe"), Roles = { new IdentityUserRole() { UserId = id, RoleId = systemRole.Id } } });

            id = Guid.NewGuid().ToString();
            users.Add(new ApplicationUser() {Id = id, FirstName = "Super", LastName = "Admin", Email = "super@admin.com", UserName = "superadmin", PasswordHash = new PasswordHasher().HashPassword("123qwe"), Roles = { new IdentityUserRole() { UserId = id, RoleId = adminRole.Id } } });   


            if (!db.Users.Any())
            {
                foreach (var user in users)
                {
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    db.Users.Add(user);
                }                
            }
            else
            {
                foreach (var user in users.Where(user => db.Users.FirstOrDefault(x => x.UserName == user.UserName) == null))
                {
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    db.Users.Add(user);
                }
            }

            db.SaveChanges();
        }


        private static void AddPermissions(ApplicationDbContext db)
        {
            var permissions = new List<SecurityModels.Permission>();

            var roles = db.Roles.ToList();
            var privateSesources =  db.Resources.Where(x=> x.IsPublic == false).ToList();

            if (!db.Permissions.Any())
            {
                var role = roles.First(x => x.Name == "SystemAdmin");
                permissions.AddRange(from resource in privateSesources
                                     let id = Guid.NewGuid().ToString()
                                     select new SecurityModels.Permission()
                                     {
                                         Id = id,
                                         RoleId = role.Id,
                                         RoleName = role.Name,
                                         ResourceId = resource.Id
                                     });

            }

            db.Permissions.AddRange(permissions);
            db.SaveChanges();
        }        


        private static void AddResources(ApplicationDbContext db)
        {
            var resources = new List<SecurityModels.Resource>
            {
                new SecurityModels.Resource {Id = "", Name = "Login", Route = "root.login", IsPublic = true},
                new SecurityModels.Resource {Id = "", Name = "Access Denied", Route = "root.access-denied", IsPublic = true},
                new SecurityModels.Resource {Id = "", Name = "About", Route = "root.about", IsPublic = true},
                new SecurityModels.Resource {Id = "", Name = "Contact", Route = "root.contact", IsPublic = true},

                new SecurityModels.Resource {Id = "", Name = "Home", Route = "root.home", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Dashboard", Route = "root.dashboard", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Profile", Route = "root.profile", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "User", Route = "root.user", IsPublic = false},

                new SecurityModels.Resource {Id = "", Name = "Role", Route = "root.role", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Resource", Route = "root.resource", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Permission", Route = "root.permission", IsPublic = false},

                new SecurityModels.Resource {Id = "", Name = "Customer", Route = "root.customer.list", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Customer Detail", Route = "root.customer.detail", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Case", Route = "root.case", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Case Create", Route = "root.case.create", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Case Edit", Route = "root.case.edit", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Case Detail", Route = "root.case.detail", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Expense", Route = "root.expense", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Revenue", Route = "root.revenue", IsPublic = false},
                new SecurityModels.Resource {Id = "", Name = "Report", Route = "root.report", IsPublic = false},
            };

            if (!db.Resources.Any())
            {
                foreach (var resource in resources)
                {
                    resource.Id = Guid.NewGuid().ToString();
                    db.Resources.Add(resource);
                }
                db.SaveChanges();
            }
            else
            {
                foreach (var resource in resources.Where(resource => db.Resources.FirstOrDefault(x => x.Route == resource.Route) == null))
                {
                    resource.Id = Guid.NewGuid().ToString();
                    db.Resources.Add(resource);
                }
                db.SaveChanges();
            }
        }


        private static void AddRoles(ApplicationDbContext db)
        {
            var roles = new List<string>
            {
                "SystemAdmin", "Admin", "Manager", "User"
            };

            if (!db.Roles.Any())
            {
                foreach (var role in roles)
                {
                    db.Roles.Add(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = role });
                }
                db.SaveChanges();
            }

            else
            {
                foreach (var role in roles.Where(role => db.Roles.FirstOrDefault(x => x.Name == role) == null))
                {
                    db.Roles.Add(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = role });
                }
                db.SaveChanges();
            }
        }
    }
}
