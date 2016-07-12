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
        public enum Selection
        {
			None,
            Course,
            Module,
            Activity
        }
        public Selection Type { get; set; }
        public Course Course { get; set; }
		public IEnumerable<Module> Modules { get; set; }
		public IEnumerable<Activity> Activities { get; set; }
	}

    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Courses/_Information
		[Authorize]
		public ActionResult _Information(string Course, string Module, string Activity)
		{
			int _courseId = -1;
			int _moduleId = -1;
			int _activityId = -1;
            if (Course != null)
            {
                try { _courseId = Convert.ToInt32(Course); }
                catch (Exception) { }
            }
            if (Module != null)
            {
                try { _moduleId = Convert.ToInt32(Module); }
                catch (Exception) { }
            }
            if (Activity != null)
            {
                try { _activityId = Convert.ToInt32(Activity); }
                catch (Exception) { }
            }

            ItemInformation model = new ItemInformation();
			model.Type = ItemInformation.Selection.None;
			if (_courseId > -1)
			{
				model.Course = db.Courses.Where(c => c.Id == _courseId).FirstOrDefault();       // Pick the selected course
                model.Type = ItemInformation.Selection.Course;
            }

			if (_moduleId > -1)
			{
				model.Modules = db.Modules.Where(m => m.Id == _moduleId);                       // Pick the selected module
                model.Type = ItemInformation.Selection.Module;
            }
			else
			{
				model.Modules = db.Modules.Where(m => m.Course.Id == _courseId);                // List all modules under the selected course
			}

			if (_activityId > -1)
			{
				model.Activities = db.Activities.Where(m => m.Id == _activityId);               // Pick the selected activity
                model.Type = ItemInformation.Selection.Activity;
            }
			else if(_moduleId > -1)
			{
				model.Activities = db.Activities.Where(m => m.Module.Id == _moduleId);          // List all activities under the selected module
			}
			else
			{
				model.Activities = db.Activities.Where(a => a.Module.Course.Id == _courseId);   // List all activities under the selected course
			}
			return PartialView("_Information", model);
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
