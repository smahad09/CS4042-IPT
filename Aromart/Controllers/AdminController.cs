using Aromart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bcrypt = BCrypt.Net.BCrypt;
namespace Aromart.Controllers
{
    public class AdminController : Controller
    {
        AdminEntity admin = new AdminEntity();
       
        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection formData)
        {
            try
            {
                // TODO: Add insert logic here
                Models.admin ad = new Models.admin()
                {
                    adminName = formData["adminName"],
                    adminPwd = bcrypt.HashPassword(formData["adminPwd"])
                };

                admin.admins.Add(ad);

                admin.SaveChanges();

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public ActionResult Login(FormCollection formData)
        {
            try
            {
                using (var ctx = new AdminEntity())
                {
                    string adName = formData["adminName"];
                    var ad = ctx.admins
                                    .Where(s => s.adminName == adName)
                                    .FirstOrDefault();
                    //wrong email, us was null error

                    if (ad == null)
                    {
                        TempData["loginFail"] = "No such Admin";
                        return RedirectToAction("Login"); 

                    }
                    else
                    {
                        bool verified = bcrypt.Verify(formData["adminPwd"], ad.adminPwd);
                        if (verified)
                        {
                            Session["adminName"] = ad.adminName;
                            Session["adminId"] = ad.adminId;
                            Session["isAdmin"] = "true";
                            return Redirect("/");
                        }
                        else
                        {
                            TempData["loginFail"] = "Wrong Credentials";
                            return RedirectToAction("Login"); ;
                        }
                    }
                }

            }
            catch
            {
                return Redirect("/");
            }
        } 
    }
}
