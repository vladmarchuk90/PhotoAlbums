using NLayer.MVC_PAL.Util;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NLayer.MVC_PAL.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
            : base("IdentityConnection")
        {
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}