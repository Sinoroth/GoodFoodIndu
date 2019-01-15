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
    public class FoodIngredientsController : Controller
    {
        private CateringContext db = new CateringContext();

        // GET: FoodIngredients
        public ActionResult Index()
        {
            return View(db.foodIngredients.ToList());
        }

        // GET: FoodIngredients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodIngredient foodIngredient = db.foodIngredients.Find(id);
            if (foodIngredient == null)
            {
                return HttpNotFound();
            }
            return View(foodIngredient);
        }

        // GET: FoodIngredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodIngredientId,Ingredient")] FoodIngredient foodIngredient)
        {
            if (ModelState.IsValid)
            {
                db.foodIngredients.Add(foodIngredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodIngredient);
        }

        // GET: FoodIngredients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodIngredient foodIngredient = db.foodIngredients.Find(id);
            if (foodIngredient == null)
            {
                return HttpNotFound();
            }
            return View(foodIngredient);
        }

        // POST: FoodIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodIngredientId,Ingredient")] FoodIngredient foodIngredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodIngredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodIngredient);
        }

        // GET: FoodIngredients/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodIngredient foodIngredient = db.foodIngredients.Find(id);
            if (foodIngredient == null)
            {
                return HttpNotFound();
            }
            return View(foodIngredient);
        }

        // POST: FoodIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FoodIngredient foodIngredient = db.foodIngredients.Find(id);
            db.foodIngredients.Remove(foodIngredient);
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
