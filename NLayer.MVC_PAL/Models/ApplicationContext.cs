using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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