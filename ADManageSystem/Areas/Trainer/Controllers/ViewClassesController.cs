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

namespace ADManageSystem.Areas.Trainer.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class ViewClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trainer/ViewClasses
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var classes = db.Classes.Include(c => c.Courses).Include(c => c.Courses.Categories);
            return View(classes.Where(x => x.Topics.Select(y => y.ApplicationUserID).Contains(userID)).ToList());
        }

        // GET: Trainer/ViewClasses/Details/5
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