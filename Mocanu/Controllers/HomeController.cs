using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mocanu.Data;
using Mocanu.Models;
using Mocanu.Models.LModels;
using Microsoft.AspNet.Identity;

namespace Mocanu.Controllers
{
    public class HomeController : Controller
    {
        CateringContext db = new CateringContext();

        // GET: Home/Index
        public ActionResult Index()
        {

            return View();
        }

        // GET: Home/Plate
        [HttpGet]
        public ActionResult Plate()
        {
            TransactionViewModel transactionViewModel = new TransactionViewModel();

            int totalCost = 0;

            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                Client client = new Client();

                foreach (var user in db.Clients)
                {
                    if (user.ClientId == userId)
                    {
                        client = user;
                        break;
                    }
                }

                var NumberedList = new List<NumberedViewModel>();

                transactionViewModel.ClientId = userId;
                transactionViewModel.Email = client.Email;


                transactionViewModel.Address = client.Address;
                transactionViewModel.ID_Card_Number = client.ID_Card_Number;
                transactionViewModel.ID_Card_Series = client.ID_Card_Series;

                foreach (var food in db.currentOrders)
                {
                    if (food.NumberInOrder > 0)
                    {
                        CateringContext temp = new CateringContext();
                        Food newFood = temp.foods.Find(food.FoodName);

                        NumberedList.Add(new NumberedViewModel()
                        {
                            Name = food.FoodName,
                            Number = food.NumberInOrder,
                            Price = newFood.Price
                        }
                    );
                        totalCost += food.NumberInOrder * newFood.Price;
                    }
                }

                transactionViewModel.TotalCost = totalCost;
                transactionViewModel.Foods = NumberedList;
            }
            else
            {
                CateringContext context = new CateringContext();
                var NumberedList = new List<NumberedViewModel>();

                foreach (var food in db.currentOrders)
                {
                    if (food.NumberInOrder > 0)
                    {
                        CateringContext temp = new CateringContext();
                        Food newFood = temp.foods.Find(food.FoodName);

                        NumberedList.Add(new NumberedViewModel()
                        {
                            Name = food.FoodName,
                            Number = food.NumberInOrder,
                            Price = newFood.Price
                        }
                    );
                        totalCost += food.NumberInOrder * newFood.Price;
                    }
                }

                transactionViewModel.TotalCost = totalCost;
                transactionViewModel.Foods = NumberedList;
            }
            return View(transactionViewModel);
        }


        public ActionResult Plate(TransactionViewModel model)
        {
            if (model.Address == null || model.ID_Card_Number == null || model.ID_Card_Series == null)
            {
                return RedirectToAction("Plate");

            }
            Transaction transaction = new Transaction();
            transaction.TotalCost = model.TotalCost;
            transaction.ID_Card_Number = model.ID_Card_Number;
            transaction.ID_Card_Series = model.ID_Card_Series;
            transaction.Address = model.Address;
            transaction.Date = DateTime.Now;

            if (Request.IsAuthenticated)
            {
                transaction.Email = model.Email;
                transaction.ClientId = model.ClientId;

                foreach (var food in db.currentOrders)
                {
                    if (food.NumberInOrder > 0)
                    {
                        db.TransactionToFoods.Add(new TransactionToFood
                        {
                            Number = food.NumberInOrder,
                            FoodName = food.FoodName,
                            TransactionId = transaction.Id
                        });
                    }

                    db.currentOrders.Remove(food);
                }
            }
            else
            {
                transaction.Email = "Guest@a.net";
                CateringContext ct = new CateringContext();

                foreach (var food in db.currentOrders)
                {
                    if (food.NumberInOrder > 0)
                    {
                        db.TransactionToFoods.Add(new TransactionToFood
                        {
                            Number = food.NumberInOrder,
                            FoodName = food.FoodName,
                            TransactionId = transaction.Id
                        });
                    }

                    db.currentOrders.Remove(food);
                }

            }

            db.transactions.Add(transaction);
            db.SaveChanges();
            return RedirectToAction("PostOrder");
        }

        // GET: Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult PostOrder()
        {
            var userId = User.Identity.GetUserId();
            Client client = db.Clients.Find(userId);
            return View(client);
        }

        [HttpPost]
        public ActionResult PostOrder(Client client)
        {
            foreach (var cl in db.Clients)
            {
                if (cl.ClientId == client.ClientId)
                {
                    client.UserScore = cl.UserScore;
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

    }
}