using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NLayer.MVC_PAL.Models;

namespace NLayer.MVC_PAL.Util
{
    public class StoreIdentityDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            // creating three roles
            var roleAdmin   = new ApplicationRole { Name = "admin", Description = "Administrator of the site" };
            var roleModerator    = new ApplicationRole { Name = "moderator", Description = "Moderator" };
            var roleUser = new ApplicationRole { Name = "user", Description = "Base role" };

            // adding roles to DB
            roleManager.Create(roleAdmin);
            roleManager.Create(roleModerator);
            roleManager.Create(roleUser);

            // creating users
            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            string password = "qwerty";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                // assign roles  to user
                userManager.AddToRole(admin.Id, roleAdmin.Name);
                userManager.AddToRole(admin.Id, roleModerator.Name);
                userManager.AddToRole(admin.Id, roleUser.Name);
            }

            base.Seed(context);
        }
    }
}