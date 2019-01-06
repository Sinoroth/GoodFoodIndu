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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
                HttpCookieCollection MyCookieColl = Request.Cookies;
                HttpCookie MyCookie;

                String[] arr1 = MyCookieColl.AllKeys;
                CateringContext context = new CateringContext();
                var NumberedList = new List<NumberedViewModel>();

                for (int i = 0; i < arr1.Length; i++)
                {
                    MyCookie = MyCookieColl[arr1[i]];
                    int price = 0;
                    bool ok = false;

                    if (MyCookie.Value == null)
                    {
                        continue;
                    }

                    foreach (var food in db.foods)
                    {
                        if (food.FoodName == MyCookie.Name)
                        {
                            price = food.Price;
                            ok = true;
                            MyCookie.Expires = DateTime.Now.AddDays(-1);
                        }
                    }
                    if (ok == false) continue;

                    if (Int32.TryParse(MyCookie.Value, out int temp) == false)
                    {
                        MyCookie.Expires = DateTime.Now.AddDays(-1);
                        continue;
                    }

                    NumberedList.Add(new NumberedViewModel()
                    {
                        Name = MyCookie.Name,
                        Number = Int32.Parse(MyCookie.Value),
                        Price = price
                    });
                    totalCost += price * Int32.Parse(MyCookie.Value);
                }

                transactionViewModel.Foods = NumberedList;
                transactionViewModel.TotalCost = totalCost;
            }
            return View(transactionViewModel);
        }

    }
}