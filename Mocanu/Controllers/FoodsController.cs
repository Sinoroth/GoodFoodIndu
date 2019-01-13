using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mocanu.Data;
using Mocanu.Models.LModels;
using System.Net;


namespace Mocanu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FoodsController : Controller
    {
        private CateringContext db = new CateringContext();

        public FoodViewsModel GetFoodViewsModel(Food food, string id)
        {
            var Results = from ingredient in db.foodIngredients
                          select new
                          {
                              ingredient.Ingredient,
                              Checked = ((from Fingredient in db.FoodtoFoodIngredients
                                          where (Fingredient.FoodName == id) &
                                          (Fingredient.IngredientId == ingredient.Ingredient)
                                          select Fingredient).Count() > 0)

                          };

            var NewFoodViewModel = new FoodViewsModel();

            NewFoodViewModel.FoodName = id;
            NewFoodViewModel.Price = food.Price;
            NewFoodViewModel.FoodType = food.FoodType;
            NewFoodViewModel.Quantity = food.Quantity;
            NewFoodViewModel.ImageLink = food.ImageLink;

            var MyCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var ingredient in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel
                {
                    Name = ingredient.Ingredient,
                    Checked = ingredient.Checked
                });
            }

            NewFoodViewModel.FoodIngredients = MyCheckBoxList;

            return NewFoodViewModel;
        }


        // GET: Foods
        public ActionResult Index()
        {
            return View(db.foods.ToList());
        }

        // GET: Foods/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            return View(GetFoodViewsModel(food, id));
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodName,ImageLink,Price,Quantity")] Food food)
        {
            if (ModelState.IsValid)
            {
                food.FoodtoFoodIngredients = new List<FoodtoFoodIngredients>();
                db.foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            return View(GetFoodViewsModel(food, id));

        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodViewsModel food)
        {
            if (ModelState.IsValid)
            {
                var Food = db.foods.Find(food.FoodName);

                Food.FoodName = food.FoodName;
                Food.Price = food.Price;
                Food.Quantity = food.Quantity;
                Food.FoodType = food.FoodType;
                Food.ImageLink = food.ImageLink;

                foreach (var ingredient in db.FoodtoFoodIngredients)
                {
                    if (ingredient.FoodName == food.FoodName)
                    {
                        db.Entry(ingredient).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                Food.FoodIngredients = "(";


                foreach (var ingredient in food.FoodIngredients)
                {
                    if (ingredient.Checked)
                    {
                        db.FoodtoFoodIngredients.Add(new FoodtoFoodIngredients
                        {
                            FoodName = food.FoodName,
                            IngredientId = ingredient.Name,
                        }
                        );
                        Food.FoodIngredients += ingredient.Name + ",";
                    }
                }

                Food.FoodIngredients += ")";

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Food food = db.foods.Find(id);
            db.foods.Remove(food);
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

