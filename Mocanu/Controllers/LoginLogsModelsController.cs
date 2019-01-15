using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mocanu.Data;
using Mocanu.Models.LModels;

namespace Mocanu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LoginLogsModelsController : Controller
    {
        private CateringContext db = new CateringContext();

        // GET: LoginLogsModels
        public ActionResult Index()
        {
            return View(db.loginLogs.ToList());
        }

        // GET: LoginLogsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginLogsModels loginLogsModels = db.loginLogs.Find(id);
            if (loginLogsModels == null)
            {
                return HttpNotFound();
            }
            return View(loginLogsModels);
        }

        // GET: LoginLogsModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginLogsModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserEmail,UserRole,LoginDate")] LoginLogsModels loginLogsModels)
        {
            if (ModelState.IsValid)
            {
                db.loginLogs.Add(loginLogsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loginLogsModels);
        }

        // GET: LoginLogsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginLogsModels loginLogsModels = db.loginLogs.Find(id);
            if (loginLogsModels == null)
            {
                return HttpNotFound();
            }
            return View(loginLogsModels);
        }

        // POST: LoginLogsModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserEmail,UserRole,LoginDate")] LoginLogsModels loginLogsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginLogsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loginLogsModels);
        }

        // GET: LoginLogsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginLogsModels loginLogsModels = db.loginLogs.Find(id);
            if (loginLogsModels == null)
            {
                return HttpNotFound();
            }
            return View(loginLogsModels);
        }

        // POST: LoginLogsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginLogsModels loginLogsModels = db.loginLogs.Find(id);
            db.loginLogs.Remove(loginLogsModels);
            db.SaveChanges();
            return RedirectToAction("Index");
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