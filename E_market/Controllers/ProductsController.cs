using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_market.Models;
using E_market.ViewModels;
using System.IO;



namespace E_market.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(string searching)
        {
            var Products = from s in db.products
                           select s;
            if (!String.IsNullOrEmpty(searching))
            {
                Products = Products.Where(s => s.category.name.Contains(searching));
            }
            return View(Products.ToList());
            //return View(db.products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            var Product = db.products.SingleOrDefault(c => c.id == id);
            var Categories = db.categories.ToList();
            ProductCategoryViewModel pcvm = new ProductCategoryViewModel
            {
                Categories = Categories,
                Product = Product
            };

            return View(pcvm);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = db.products.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var Categories = db.categories.ToList();
            ProductCategoryViewModel pcvm = new ProductCategoryViewModel
            { Categories = Categories };

            return View(pcvm);
            //return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryViewModel pcvm, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                pcvm.Product.Image = upload.FileName;
                db.products.Add(pcvm.Product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var Categories = db.categories.ToList();
            pcvm.Categories = Categories;
            return View("Create", pcvm);
            //if (ModelState.IsValid)
            //{
            //    db.products.Add(product);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            var Product = db.products.SingleOrDefault(c => c.id == id);
            var Categories = db.categories.ToList();
            ProductCategoryViewModel pcvm = new ProductCategoryViewModel
            {
                Categories = Categories,
                Product = Product
            };

            return View(pcvm);
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Product product = db.products.Find(id);
            //    if (product == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategoryViewModel pcvm)
        {
            if (ModelState.IsValid)
            {
                var ProductDB = db.products.Single(c => c.id == pcvm.Product.id);
                ProductDB.Image = pcvm.Product.Image;
                ProductDB.Name = pcvm.Product.Name;
                ProductDB.Price = pcvm.Product.Price;
                ProductDB.Description = pcvm.Product.Description;
                ProductDB.Categoryid = pcvm.Product.Categoryid;
                db.SaveChanges();
                return RedirectToAction("Index");

                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            var Categories = db.categories.ToList();
            pcvm.Categories = Categories;
            return View("Edit", pcvm);
            //return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.products.Find(id);
            db.products.Remove(product);
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

        public ActionResult AddToCart(int product_id)
        {
            if (Session["Cart"] == null) {
                List<Cart> cart = new List<Cart>();
                var product = db.carts.Find(product_id);
                cart.Add(new Cart()
                {
                    Product = product
                });
                Session["Cart"] = cart;
            }
            else
            {
                List<Cart> cart = (List<Cart>)Session["Cart"];
                var product = db.carts.Find(product_id);
                cart.Add(new Cart()
                {
                    Product = product
                });
                Session["Cart"] = cart;
            } 

            //  var product = ctx.carts.Find(product_id);

            return Redirect("Index");
        }

    }
}
