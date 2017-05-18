using Microsoft.AspNet.Identity.EntityFramework;


namespace NLayer.MVC_PAL.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public string Description { get; set; }
    }
}