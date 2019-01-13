using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mocanu.Data;
using Mocanu.Models.LModels;

namespace Mocanu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TransactionsController : Controller
    {
        private CateringContext db = new CateringContext();

        public TransactionViewModel GetTransactionViewModel(Transaction transaction, int id)
        {
            TransactionViewModel NewModel = new TransactionViewModel();

            var Result = from Food in db.foods
                         select new
                         {
                             Food.FoodName,
                             Food.Price,
                             Food.Quantity,
                             Numbered = (from food in db.TransactionToFoods
                                         where (food.FoodName == Food.FoodName)
                                         & (food.TransactionId == id)
                                         select food
                                          ).Count()
                         };

            var NewTransactionViewModel = new TransactionViewModel();

            NewTransactionViewModel.Address = transaction.Address;
            NewTransactionViewModel.ClientId = transaction.ClientId;
            NewTransactionViewModel.Email = transaction.Email;
            NewTransactionViewModel.ID_Card_Number = transaction.ID_Card_Number;
            NewTransactionViewModel.ID_Card_Series = transaction.ID_Card_Series;

            var NumberedList = new List<NumberedViewModel>();

            int totalCost = 0;

            foreach (var food in Result)
            {
                CateringContext context = new CateringContext();
                int nr = 0;
                if (food.Numbered > 0)
                {
                    nr = context.TransactionToFoods.First(a => a.FoodName == food.FoodName && a.TransactionId == id).Number;
                }
                NumberedList.Add(new NumberedViewModel()
                {
                    Name = food.FoodName,
                    Number = nr,
                    Price = food.Price
                }
                );
                totalCost += nr * food.Price;
            }

            transaction.TotalCost = totalCost;

            NewTransactionViewModel.TotalCost = totalCost;
            NewTransactionViewModel.Foods = NumberedList;

            return NewTransactionViewModel;

        }

        // GET: Transactions
        public ActionResult Index()
        {
            return View(db.transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(GetTransactionViewModel(transaction, id));
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                bool userWithEmailExists = false;
                foreach (var client in db.Clients)
                {
                    if (client.Email == transaction.Email)
                    {
                        transaction.ClientId = client.ClientId;
                        userWithEmailExists = true;
                    }
                }

                if (userWithEmailExists == false) return View(transaction);
                db.transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(GetTransactionViewModel(transaction, id));
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransactionViewModel transactionViewModel)
        {
            if (ModelState.IsValid)
            {
                var transaction = db.transactions.Find(transactionViewModel.Id);

                transaction.ID_Card_Number = transactionViewModel.ID_Card_Number;
                transaction.ID_Card_Series = transactionViewModel.ID_Card_Series;
                transaction.Address = transactionViewModel.Address;
                transaction.Id = transactionViewModel.Id;
                int totalCost = 0;

                foreach (var t in db.TransactionToFoods)
                {
                    if (t.Id == transactionViewModel.Id)
                    {
                        db.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var food in transactionViewModel.Foods)
                {
                    if (food.Number > 0)
                    {
                        foreach (var tr in db.TransactionToFoods)
                        {
                            if (tr.FoodName == food.Name && tr.TransactionId == transaction.Id)
                            {
                                db.TransactionToFoods.Remove(tr);
                                break;
                            }
                        }
                        db.TransactionToFoods.Add(new TransactionToFood
                        {
                            FoodName = food.Name,
                            TransactionId = transaction.Id,
                            Number = food.Number
                        });
                        totalCost += food.Number * food.Price;
                    }
                    else
                    {
                        foreach (var tr in db.TransactionToFoods)
                        {
                            if (tr.FoodName == food.Name && tr.TransactionId == transaction.Id)
                            {
                                db.TransactionToFoods.Remove(tr);
                                break;
                            }
                        }
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactionViewModel);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(GetTransactionViewModel(transaction, id));
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.transactions.Find(id);
            db.transactions.Remove(transaction);
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