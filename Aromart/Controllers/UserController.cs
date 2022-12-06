using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aromart.Models;
using bcrypt = BCrypt.Net.BCrypt;
using Aromart.Controllers;
using System.Web.Routing;

namespace Aromart.Controllers
{
    public class UserController : Controller
    {
        HomeController homeController = new HomeController();
        UserEntity user = new UserEntity();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/register
        public ActionResult Register()
        {

            return View();
        }

        // POST: User/register
        [HttpPost]
        public ActionResult Register(FormCollection formData)
        {
            try
            {
                Models.user newUser = new Models.user()
                {
                    userEmail = formData["userEmail"],
                    userName = formData["userName"],
                    userPwd = bcrypt.HashPassword(formData["userPwd"], 12)
                };

                user.users.Add(newUser);

                user.SaveChanges();

                return Redirect("/");            
            
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }

        // GET: user/login
        public ActionResult Login()
        {
            return View();
        }


        // POST: user/login
        [HttpPost]
        public ActionResult Login(FormCollection formData)
        {
            try
            {
                using (var ctx = new UserEntity())
                {
                    string email = formData["userEmail"];
                    var us = ctx.users
                                    .Where(s => s.userEmail == email)
                                    .FirstOrDefault();
                    //wrong email, us was null error

                    if (us == null) 
                    {
                        TempData["loginFail"] = "No such email";
                        return RedirectToAction("Login"); ;
                        
                    }
                    else 
                    {
                        bool verified = bcrypt.Verify(formData["userPwd"], us.userPwd);
                        if (verified)
                        {
                            Session["userName"] = us.userName;
                            Session["userId"] = us.userId;
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
                return RedirectToAction("Register");
            }
        }
    }
}
