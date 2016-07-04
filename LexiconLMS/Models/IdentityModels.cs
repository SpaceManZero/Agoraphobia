using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LexiconLMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		
		/// <summary>
		/// The Course a User of Role Student is signed up to
		/// TODO: Limit ability to set a Course to Users of Role Student in 'set'
		/// </summary>
		public virtual Course Course { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here

			return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		public System.Data.Entity.DbSet<LexiconLMS.Models.Course> Courses { get; set; }

		public System.Data.Entity.DbSet<LexiconLMS.Models.Module> Modules { get; set; }

		public System.Data.Entity.DbSet<LexiconLMS.Models.Activity> Activities { get; set; }

		public System.Data.Entity.DbSet<LexiconLMS.Models.ActivityType> ActivityTypes { get; set; }
	}
}