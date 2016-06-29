namespace LexiconLMS.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Models;
	internal sealed class Configuration : DbMigrationsConfiguration<LexiconLMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LexiconLMS.Models.ApplicationDbContext";
        }

        protected override void Seed(LexiconLMS.Models.ApplicationDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
			var store = new RoleStore<IdentityRole>(context);
			var manager = new RoleManager<IdentityRole>(store);
			if (!context.Roles.Any(r => r.Name == "Teacher"))
			{
				var admin = new IdentityRole { Name = "Teacher" };
				manager.Create(admin);
				context.SaveChanges();
			}
			if (!context.Roles.Any(r => r.Name == "Student"))
			{
				var member = new IdentityRole { Name = "Student" };
				manager.Create(member);
				context.SaveChanges();
			}

			if (!context.Users.Any(u => u.Email == "teacher001@lexicon.se"))
			{
				var uStore = new UserStore<ApplicationUser>(context);
				var uManager = new UserManager<ApplicationUser>(uStore);
				var user = new ApplicationUser { UserName = "teacher001@lexicon.se", Email = "teacher001@lexicon.se" };

				uManager.Create(user, "Teacher_001");
				user = uManager.FindByEmail("teacher001@lexicon.se");
				uManager.AddToRole(user.Id, "Teacher");
				context.SaveChanges();
			}

			if (!context.Users.Any(u => u.Email == "student001@lexicon.se"))
			{
				var uStore = new UserStore<ApplicationUser>(context);
				var uManager = new UserManager<ApplicationUser>(uStore);
				var user = new ApplicationUser { UserName = "student001@lexicon.se", Email = "student001@lexicon.se" };

				uManager.Create(user, "Student_001");
				user = uManager.FindByEmail("student001@lexicon.se");
				uManager.AddToRole(user.Id, "Student");
				context.SaveChanges();
			}
		}
    }
}
