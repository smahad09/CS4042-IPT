using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aromart.Models;

namespace Aromart.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsEntity products = new ProductsEntity();

        [HttpGet]
        [Route("/products")]
        public ActionResult Index()
        {
            return View(products.products.ToList());
        }

        [HttpGet]
        [Route("/products/{id?}")]
        public ActionResult Details(int id)
        {
            var prod = products.products.Find(id);

            if (prod != null)
            {
                return View(prod);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Products/Add
        public ActionResult Add()
        {
            if (Session != null) 
            {
                if (Session["isAdmin"] != null) 
                { return View(); }
                else { return Redirect("/"); }
            } 
            else
            {
                return Redirect("/");
            }
        }

        // POST: Products/Add
        [HttpPost]
        public ActionResult Add(FormCollection formData)
        {
            try
            {
                Models.product prod = new Models.product()
                {
                    productName = formData["productName"],
                    productBrand = formData["productBrand"],
                    productDesc = formData["productDesc"],
                    productPrice = double.Parse(formData["productPrice"]),
                    productImg = formData["productImg"]
                };

                products.products.Add(prod);

                products.SaveChanges();

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/{id?}
        public ActionResult Edit(int id)
        {
            if (Session != null)
            {
                if (Session["isAdmin"] != null)
                {
                    var prod = products.products.Find(id);
                    return View(prod); 
                }
                else { return Redirect("/"); }
            }
            else
            {
                return Redirect("/");
            }
        }

        // POST: Products/Edit/{id?}
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formData)
        {
            try
            {
                // TODO: Add update logic here
                if (Session["isAdmin"] != null)
                {
                    var prod = products.products.Find(id);
                    prod.productName = formData["productName"];
                    prod.productBrand = formData["productBrand"];
                    prod.productPrice = double.Parse(formData["productPrice"]);
                    prod.productDesc = formData["productDesc"];

                    string qry = $"update products set productName={prod.productName}, productBrand={prod.productBrand}, " +
                        $"productPrice={prod.productPrice}, productDesc={prod.productDesc}";

                    products.products.SqlQuery(qry);

                    products.SaveChanges();

                    return Redirect("/");

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (Session["isAdmin"] != null)
                {
                    string qry = $"Delete from products where productId={id}";
                    products.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
