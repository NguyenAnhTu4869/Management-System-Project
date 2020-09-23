using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ADManageSystem.Models;
using Microsoft.AspNet.Identity;

namespace ADManageSystem.Areas.Trainee.Controllers
{
    [Authorize(Roles = "Trainee")]
    public class ViewClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trainee/ViewClasses
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var classes = db.Classes.Include(c => c.Courses).Include(c => c.Courses.Categories);
            return View(classes.Where(x => x.TraineeClasses.Select(y => y.ApplicationUserID).Contains(userID)).ToList());
        }

        // GET: Trainee/ViewClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }
    }
}
