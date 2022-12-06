using Aromart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aromart.Controllers
{
    public class OrderController : Controller
    {
        private ProductsEntity prod = new ProductsEntity();
        private UserEntity user = new UserEntity();
        private Aromart_Orders order = new Aromart_Orders();

        // GET: order/{id?}
        public ActionResult Order(int? id)
        {
            var product = prod.products.Find(id);

            if (Session != null)
            {
                if (Session["userId"] != null )
                {
                    ViewBag.product = product;
                    return View();
                }
                else { return Redirect("/"); }
            } 
            else
            {
                return Redirect("/user/login");
            }
            
        }

        // POST: order/{id?}
        [HttpPost]
        public ActionResult Order(int? id, FormCollection formData)
        {
            var product = prod.products.Find(id);
            if (Session != null)
            {
                if (Session["userId"] != null)
                {
                    Models.order newOrder = new Models.order()
                    {
                        userId = int.Parse(Session["userId"].ToString()),
                        productId = id,
                        city = formData["city"],
                        phone = formData["phone"],
                        state = formData["state"],
                        address = formData["address"],
                        orderTotal = product.productPrice
                    };
                    order.orders.Add(newOrder);

                    order.SaveChanges();

                    return Redirect("/");
                }
            }

            return Redirect("/user/login");
        }

    }
}
