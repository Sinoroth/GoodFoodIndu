using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mocanu.Data;
using Mocanu.Models;
using Mocanu.Models.LModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mocanu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClientsController : Controller
    {
        private CateringContext db = new CateringContext();

        private ApplicationDbContext db1 = new ApplicationDbContext();

        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,Email,Address,CNP,ID_Card_Series,ID_Card_Number,FirstName,LastName,TelephoneNumber,Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db1));


                var PasswordHash = new PasswordHasher();

                var user = new ApplicationUser()
                {
                    UserName = client.FirstName,
                    Address = client.Address,
                    CNP = client.CNP,
                    ID_Card_Number = client.ID_Card_Number,
                    ID_Card_Series = client.ID_Card_Series,
                    PhoneNumber = client.TelephoneNumber,
                    PasswordHash = PasswordHash.HashPassword(client.Password),
                    Email = client.Email
                };

                UserManager.Create(user);
                UserManager.AddToRole(user.Id, "User");
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,Email,Address,CNP,ID_Card_Series,ID_Card_Number,FirstName,LastName,TelephoneNumber,Password")] Client client)
        {
            if (ModelState.IsValid)
            {
                var PasswordHash = new PasswordHasher();

                foreach (var user in db1.Users)
                {
                    if (user.Id == client.ClientId)
                    {
                        user.UserName = client.FirstName;
                        user.Address = client.Address;
                        user.CNP = client.CNP;
                        user.ID_Card_Number = client.ID_Card_Number;
                        user.ID_Card_Series = client.ID_Card_Series;
                        user.PhoneNumber = client.TelephoneNumber;
                        user.PasswordHash = PasswordHash.HashPassword(client.Password);
                        user.Email = client.Email;
                    }
                }
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(string id)
        {
            Client client = db.Clients.Find(id);
            client.isSuspended = !client.isSuspended;
            db.SaveChanges();
            return RedirectToAction("Index");/*
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);*/
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Suspend")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            //Client client = db.Clients.Find(id);
            //db.Clients.Remove(client);
            Client client = db.Clients.Find(id);
            client.isSuspended = !client.isSuspended;
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