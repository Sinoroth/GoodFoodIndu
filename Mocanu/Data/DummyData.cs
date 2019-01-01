using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mocanu.Models.LModels;

namespace Mocanu.Data
{
    public class DummyData
    {
        public static System.Collections.Generic.List<Food> getFoods()
        {
            List<Food> foods = new List<Food>
            {

                new Food()
                {
                    FoodName = "Vita Mongoliana",
                    ImageLink = "~/Images/Mongolian-Beef-3.jpg",
                    Price = 20,
                    Quantity = 330,
                    FoodType = "Beef",
                    FoodIngredients = "",

                 },

                new Food()
                {
                    FoodName = "Snitel de Pui",
                    ImageLink = "~/Images/snitele-din-piept-de-pui.jpg",
                    Price = 6,
                    Quantity = 100,
                    FoodType = "Chicken",
                    FoodIngredients = "",

                },

                new Food()
                {
                    FoodName = "Salata cu fructe de mare",
                    ImageLink = "~/Images/salataCuFructeDeMare.jpg",
                    Price = 18,
                    Quantity = 330,
                    FoodType = "Salads",
                    FoodIngredients = "",

                },

                new Food()
                {
                    FoodName = "Salata de Cruditati cu Piept de Pui",
                    ImageLink = "~/Images/salatacucruditatcupietdepui.jpg",
                    Price = 27,
                    Quantity = 499,
                    FoodType = "Salads",
                    FoodIngredients = "",

                },

                new Food()
                {
                    FoodName = "Coaste de porc",
                    ImageLink = "~/Images/coaste-de-porc.jpg",
                    Price = 30,
                    Quantity = 400,
                    FoodType = "Pork",
                    FoodIngredients = "",
                },

                new Food()
                {
                    FoodName = "Piept de Pui",
                    ImageLink = "~/Images/pieptpui.jpg",
                    Price = 6,
                    Quantity = 100,
                    FoodType = "Chicken",
                    FoodIngredients = "",

                },

                new Food()
                {
                    FoodName = "Clatite cu Branza Dulce",
                    ImageLink = "~/Images/clatite.jpg",
                    Price = 10,
                    Quantity = 150,
                    FoodType = "Desserts",
                    FoodIngredients = "",
                },

                new Food()
                {
                    FoodName = "Salata de Fructe",
                    ImageLink = "~/Images/salatadefructe.jpg",
                    Price = 15,
                    Quantity = 200,
                    FoodType = "Desserts",
                    FoodIngredients = "",
                },

                new Food()
                {
                    FoodName = "Pepsi",
                    ImageLink = "",
                    Price = 15,
                    Quantity = 200,
                    FoodType = "Drinks",
                    FoodIngredients = "",

                },

                new Food()
                {
                    FoodName = "Mirinda",
                    ImageLink = "",
                    Price = 15,
                    Quantity = 200,
                    FoodType = "Drinks",
                    FoodIngredients = "",
                },

                new Food()
                {
                    FoodName = "Seven-up",
                    ImageLink = "",
                    Price = 15,
                    Quantity = 200,
                    FoodType = "Drinks",
                    FoodIngredients = "",
                },


            };

            return foods;
        }

    }
}