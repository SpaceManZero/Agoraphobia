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

			// Add the Teacher and Student roles NOTE: THIS SHOULD BE PART OF DB INITIALIZATION
			var store = new RoleStore<IdentityRole>(context);
			var manager = new RoleManager<IdentityRole>(store);
			if (!context.Roles.Any(r => r.Name == "Teacher"))
			{
				var admin = new IdentityRole { Name = "Teacher" };
				manager.Create(admin);
			}
			if (!context.Roles.Any(r => r.Name == "Student"))
			{
				var member = new IdentityRole { Name = "Student" };
				manager.Create(member);
			}
			context.SaveChanges();

			// Add ActivityTypes NOTE: THIS SHOULD BE PART OF DB INITIALIZATION
			context.ActivityTypes.AddOrUpdate(new ActivityType { Type = "Lecture" });
			context.ActivityTypes.AddOrUpdate(new ActivityType { Type = "E-Learning" });
			context.ActivityTypes.AddOrUpdate(new ActivityType { Type = "Exercise" });
			context.ActivityTypes.AddOrUpdate(new ActivityType { Type = "Code-Along" });
			context.ActivityTypes.AddOrUpdate(new ActivityType { Type = "Assignment" });
			context.SaveChanges();

			ActivityType lecture = context.ActivityTypes.FirstOrDefault(c => c.Type == "Lecture");
			ActivityType elearning = context.ActivityTypes.FirstOrDefault(c => c.Type == "E-Learning");
			ActivityType exercise = context.ActivityTypes.FirstOrDefault(c => c.Type == "Exercise");
			ActivityType codeAlong = context.ActivityTypes.FirstOrDefault(c => c.Type == "Code-Along");
			ActivityType assignment = context.ActivityTypes.FirstOrDefault(c => c.Type == "Assignment");

			// Add course(s)
			context.Courses.AddOrUpdate(o => o.Name, new Course
			{
				Name = ".NET Intro",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vestibulum semper dapibus dui.Aliquam vehicula sapien mauris, vel interdum elit pharetra quis.Nulla facilisi. Sed iaculis dictum arcu eu interdum.",
				StartDate = DateTime.Today,
				EndDate = DateTime.Today.AddMonths(1)
			});
			context.Courses.AddOrUpdate(o => o.Name, new Course
			{
				Name = "Java Intro",
				Description = "Duis ac felis commodo, tempor urna sed, hendrerit ipsum. Phasellus porttitor, quam vitae tincidunt laoreet, nulla tortor ornare magna, eu tristique eros diam sed nibh. Suspendisse vitae tortor magna.",
				StartDate = DateTime.Today,
				EndDate = DateTime.Today.AddMonths(1)
			});
			context.SaveChanges();

			// Add Module(s)
			Course course1 = context.Courses.FirstOrDefault(c => c.Name == ".NET Intro");
			Course course2 = context.Courses.FirstOrDefault(c => c.Name == "Java Intro");
			context.Modules.AddOrUpdate(o => o.Name, new Module
			{
				Name = "C#",
				Description = "Duis ac felis commodo, tempor urna sed, hendrerit ipsum. Phasellus porttitor, quam vitae tincidunt laoreet, nulla tortor ornare magna, eu tristique eros diam sed nibh. Suspendisse vitae tortor magna.",
				StartDate = DateTime.Today,
				EndDate = DateTime.Today.AddDays(15),
				Course = course1
			});
			context.Modules.AddOrUpdate(o => o.Name, new Module
			{
				Name = "C# Continued",
				Description = "Duis ac felis commodo, tempor urna sed, hendrerit ipsum. Phasellus porttitor, quam vitae tincidunt laoreet, nulla tortor ornare magna, eu tristique eros diam sed nibh. Suspendisse vitae tortor magna.",
				StartDate = DateTime.Today.AddDays(16),
				EndDate = DateTime.Today.AddMonths(1),
				Course = course1
			});
			context.Modules.AddOrUpdate(o => o.Name, new Module
			{
				Name = "Java Introduction",
				Description = "Duis ac felis commodo, tempor urna sed, hendrerit ipsum. Phasellus porttitor, quam vitae tincidunt laoreet, nulla tortor ornare magna, eu tristique eros diam sed nibh. Suspendisse vitae tortor magna.",
				StartDate = DateTime.Today,
				EndDate = DateTime.Today.AddDays(15),
				Course = course2
			});
			context.SaveChanges();

			// Add activitie(s)
			Module module1 = context.Modules.FirstOrDefault(c => c.Name == "C#");
			Module module2 = context.Modules.FirstOrDefault(c => c.Name == "C# Continued");
			Module module3 = context.Modules.FirstOrDefault(c => c.Name == "Java Introduction");
			context.Activities.AddOrUpdate(o => o.Name, new Activity
			{
				Name = "Introduction",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vestibulum semper dapibus dui.Aliquam vehicula sapien mauris, vel interdum elit pharetra quis.Nulla facilisi.",
				StartDate = DateTime.Today,
				EndDate = DateTime.Today,
				Type = lecture,
				Module = module1
			});
			context.Activities.AddOrUpdate(o => o.Name, new Activity
			{
				Name = "OOP",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vestibulum semper dapibus dui.Aliquam vehicula sapien mauris, vel interdum elit pharetra quis.Nulla facilisi.",
				StartDate = DateTime.Today.AddDays(1),
				EndDate = DateTime.Today.AddDays(1),
				Type = lecture,
				Module = module1
			});
			context.Activities.AddOrUpdate(o => o.Name, new Activity
			{
				Name = "Overview",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vestibulum semper dapibus dui.Aliquam vehicula sapien mauris, vel interdum elit pharetra quis.Nulla facilisi.",
				StartDate = DateTime.Today.AddDays(16),
				EndDate = DateTime.Today.AddDays(16),
				Type = lecture,
				Module = module2
			});
			context.Activities.AddOrUpdate(o => o.Name, new Activity
			{
				Name = "Parking Garage 1.0",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vestibulum semper dapibus dui.Aliquam vehicula sapien mauris, vel interdum elit pharetra quis.Nulla facilisi.",
				StartDate = DateTime.Today.AddDays(17),
				EndDate = DateTime.Today.AddDays(17),
				Type = assignment,
				Module = module2
			});
			context.Activities.AddOrUpdate(o => o.Name, new Activity
			{
				Name = "Java Programing",
				Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vestibulum semper dapibus dui.Aliquam vehicula sapien mauris, vel interdum elit pharetra quis.Nulla facilisi.",
				StartDate = DateTime.Today,
				EndDate = DateTime.Today.AddDays(2),
				Type = elearning,
				Module = module3
			});
			context.SaveChanges();

            // Add some users
            var uStore = new UserStore<ApplicationUser>(context);
            var uManager = new UserManager<ApplicationUser>(uStore);
            // Add a teacher
            if (!context.Users.Any(u => u.Email == "teacher001@lexicon.se"))
            {
                var user = new ApplicationUser { UserName = "teacher001@lexicon.se", Email = "teacher001@lexicon.se" };
                uManager.Create(user, "Teacher_001");
                user = uManager.FindByEmail("teacher001@lexicon.se");
                uManager.AddToRole(user.Id, "Teacher");
                context.SaveChanges();
            }
            // Add a student
            if (!context.Users.Any(u => u.Email == "student001@lexicon.se"))
            {
                var user = new ApplicationUser { UserName = "student001@lexicon.se", Email = "student001@lexicon.se" };
                user.Course = context.Courses.FirstOrDefault(c => c.Name == ".NET Intro");
                uManager.Create(user, "Student_001");
                user = uManager.FindByEmail("student001@lexicon.se");
                uManager.AddToRole(user.Id, "Student");
                context.SaveChanges();
            }

            // Add another student
            if (!context.Users.Any(u => u.Email == "student002@lexicon.se"))
            {
                //Användarnamnet för studenten
                var user = new ApplicationUser { UserName = "student002@lexicon.se", Email = "student002@lexicon.se" };
                user.Course = context.Courses.FirstOrDefault(c => c.Name == ".NET Intro");
                uManager.Create(user, "Student_002");	//Lösenord för studenten
                user = uManager.FindByEmail("student002@lexicon.se");
                uManager.AddToRole(user.Id, "Student");
                context.SaveChanges();
            }
        }
    }
}
//Lorem ipsum dolor sit amet, consectetur adipiscing elit.Vestibulum semper dapibus dui.Aliquam vehicula sapien mauris, vel interdum elit pharetra quis.Nulla facilisi. Sed iaculis dictum arcu eu interdum. Duis ac felis commodo, tempor urna sed, hendrerit ipsum. Phasellus porttitor, quam vitae tincidunt laoreet, nulla tortor ornare magna, eu tristique eros diam sed nibh. Suspendisse vitae tortor magna. Maecenas imperdiet mauris in sollicitudin fermentum. Vivamus rutrum ultricies tellus ac suscipit. Pellentesque et diam nunc. Proin congue mi eget magna rutrum, quis ultrices felis dapibus. Mauris erat lorem, accumsan sit amet felis vitae, suscipit cursus ex.Interdum et malesuada fames ac ante ipsum primis in faucibus.Pellentesque consequat non nunc vitae aliquam. Vivamus mattis aliquam pellentesque.