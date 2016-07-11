using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LexiconLMS.Models;

namespace LexiconLMS.Controllers
{
	public class ItemInformation
	{
		public Course Course { get; set; }
		public IEnumerable<Module> Modules { get; set; }
		public IEnumerable<Activity> Activities { get; set; }
	}
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Courses/_Information
		[Authorize]
		public ActionResult _Information(string _courseId, string _moduleId, string _activityId)
		{
			int courseId = -1;
			int moduleId = -1;
			int activityId = -1;
			try
			{
				courseId = Convert.ToInt32(_courseId);
				moduleId = Convert.ToInt32(_moduleId);
				activityId = Convert.ToInt32(_activityId);
			}
			catch (Exception)
			{
			}
			
			ItemInformation model = new ItemInformation();
			if (courseId > -1)
			{
				model.Course = db.Courses.Where(c => c.Id == courseId).FirstOrDefault();
			}
			if (moduleId > -1)
			{
				model.Modules = db.Modules.Where(m => m.Id == moduleId);
			}
			else
			{
				model.Modules = db.Modules.Where(m => m.Course.Id == courseId);
			}
			if (activityId > -1)
			{
				model.Activities = db.Activities.Where(m => m.Id == activityId);
			}
			else if(moduleId > -1)
			{
				model.Activities = db.Activities.Where(m => m.Module.Id == moduleId);
			}
			else
			{
				model.Activities = db.Activities.Where(a => a.Module.Course.Id == courseId);
			}
			return PartialView(model);
		}

		// GET: Courses
		[Authorize]
        public ActionResult Index()
        {
			if (User.IsInRole("Student"))
			{
				Course course = db.Users.Find(User.Identity.GetUserId()).Course;
				if (course != null)
				{
					return View(db.Courses.ToList<Course>().Where(c => c.Id == course.Id));
				}
				return View(new List<Course>());
			}
			else if (User.IsInRole("Teacher"))
				return View(db.Courses.ToList());
			else
				return View(new List<Course>());
		}

		// GET: Courses/Details/5
		[Authorize]
		public ActionResult Details(int? id)
    {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (AllowAccessToCourse(id))
			{
				Course course = db.Courses.Find(id);
				if (course == null)
				{
					return HttpNotFound();
				}
				return View(course);
			}
			else
				return RedirectToAction("Index");
        }

		// GET: Courses/Create
		[Authorize(Roles = "Teacher")]
		public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Teacher")]
		public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

		// GET: Courses/Edit/5
		[Authorize(Roles = "Teacher")]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Teacher")]
		public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

		// GET: Courses/Delete/5
		[Authorize(Roles = "Teacher")]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "Teacher")]
		public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		/// <summary>
		/// Confirms that that user is a Teacher or that, if the user is a student, the user is signed up to the identified Course
		/// </summary>
		/// <param name="Id">Course Id</param>
		/// <returns>True if access should be allowed, otherwise false</returns>
		private bool AllowAccessToCourse(int? Id)
		{
			Course assignedCourse = db.Users.Find(User.Identity.GetUserId()).Course;
			if (User.IsInRole("Teacher") ||
				(User.IsInRole("Student") &&
					assignedCourse != null &&
					Id != null &&
					assignedCourse.Id == Id))
				return true;
			else
				return false;
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
