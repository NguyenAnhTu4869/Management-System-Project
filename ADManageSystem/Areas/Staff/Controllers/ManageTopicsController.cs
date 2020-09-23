using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ADManageSystem.Models;

namespace ADManageSystem.Areas.Staff.Controllers
{
    [Authorize(Roles = "Staff")]
    public class ManageTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string RoleCondition = "Trainer";

        // GET: Staff/ManageTopics
        public ActionResult Index(int? classId)
        {
            var topics = db.Topics.Include(t => t.Classes).Include(t => t.ApplicationUsers);
            ViewBag.ClassId = classId;
            ViewBag.ClassCode = db.Classes.Where(c => c.Id == classId).First().Code;
            return View(topics.Where(x => x.ClassID == classId).ToList());

        }

        // GET: Staff/ManageTopics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Staff/ManageTopics/Create
        public ActionResult Create(string classCode)
        {

            // ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.Code == classCode), "Id", "Code");
            ViewBag.ClassId = db.Classes.Where(x => x.Code == classCode).First().Id;
            ViewBag.ApplicationUserId = new SelectList(db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleCondition)).ToList(), "Id", "UserName");
            return View();
        }

        //
        // POST: Staff/ManageTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ApplicationUserId,ClassId")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index", new { classId = topic.ClassID });
            }

            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Code", topic.ClassID);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", topic.ApplicationUserID);
            return View(topic);
        }

        //
        // GET: Staff/ManageTopics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }

            //ViewBag.ClassId = new SelectList(db.Classes.Where(x => x.Code == topic.Class.Code), "Id", "Code", topic.ClassId);
            ViewBag.ApplicationUserId = new SelectList(db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(RoleCondition)).ToList(), "Id", "UserName", topic.ApplicationUserID);
            return View(topic);
        }

        // POST: Staff/ManageTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ApplicationUserID,ClassID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { classId = topic.ClassID });
            }
            ViewBag.ClassId = new SelectList(db.Classes, "Id", "Code", topic.ClassID);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FirstName", topic.ApplicationUserID);
            return View(topic);
        }

        // GET: Staff/ManageTopics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Staff/ManageTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Topic topic = db.Topics.Find(id);
            var ClassId = topic.ClassID;
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index", new { classId = ClassId });
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
