﻿using Mocanu.Models.LModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mocanu.Data
{
    public class CateringContext : DbContext
    {
        public CateringContext() : base("DefaultConnection")
        {
        }

        public DbSet<Food> foods { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<CurrentOrder> currentOrders { get; set; }

        public DbSet<FoodIngredient> foodIngredients { get; set; }
        public DbSet<FoodtoFoodIngredients> FoodtoFoodIngredients { get; set; }
        public DbSet<TransactionToFood> TransactionToFoods { get; set; }

        public DbSet<LoginLogsModels> loginLogs { get; set; }

        public System.Data.Entity.DbSet<Mocanu.Models.LModels.Client> Clients { get; set; }
        public System.Data.Entity.DbSet<Mocanu.Models.LModels.FoodOrderView> FoodOrderViews { get; set; }


    }
}