﻿using Mocanu.Data;
using Mocanu.Models.LModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mocanu.Controllers
{
    public class FoodViewController : Controller
    {
        public ActionResult FoodPage()
        {
            ViewBag.Message = "";

            return View();
        }

        private FoodOrderView getView(Food food)
        {
            FoodOrderView foodOrderView = new FoodOrderView();
            foodOrderView.FoodName = food.FoodName;
            foodOrderView.FoodIngredients = food.FoodIngredients;
            foodOrderView.ImageLink = food.ImageLink;
            foodOrderView.Price = food.Price;
            foodOrderView.Quantity = food.Quantity;
            foodOrderView.FoodType = food.FoodType;

            CateringContext cateringContext = new CateringContext();

            if (Request.IsAuthenticated)
            {
                foodOrderView.NumberInOrder = 0;
                foreach (var f in cateringContext.currentOrders)
                {
                    if (f.FoodName == food.FoodName)
                    {
                        foodOrderView.NumberInOrder = f.NumberInOrder;
                        break;
                    }
                }
            }
            else
            {

                HttpCookieCollection MyCookieColl = Request.Cookies;
                HttpCookie MyCookie;

                String[] arr1 = MyCookieColl.AllKeys;
                CateringContext context = new CateringContext();

                for (int i = 0; i < arr1.Length; i++)
                {
                    MyCookie = MyCookieColl[arr1[i]];
                    if (MyCookie.Name == food.FoodName)
                    {
                        if (Int32.TryParse(MyCookie.Value, out int temp) == true)
                        {
                            foodOrderView.NumberInOrder = Int32.Parse(MyCookie.Value);
                            break;
                        }
                    }
                }
            }


            return foodOrderView;
        }

    }
}