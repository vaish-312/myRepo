using AspMvcAssign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AspMvcAssign.Controllers
{
    public class AccountController : Controller
    {
        public static string id;
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(UserData user)
        {
            using (var context = new Assignment2Entities4())
            {
                bool IsValid1 = context.DemoDatas.Any(x => x.Username == user.Username && x.Password == user.Password);
                var IsValid = context.DemoDatas.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();

             if (IsValid != null || IsValid1)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    id = IsValid.Username;
                    HttpContext.Session["MyName"] = id;
                    using (var db=new Assignment2Entities3())
                    {
                        InfoRec data = new InfoRec()
                        {
                            Activity = "Login",
                            Time=DateTime.Now,
                           
                           
                        };

                        db.InfoRecs.Add(data);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "Details");
                }
                else
                {
                    ViewBag.error = "Username And Password Did Not Match ";
                }
                /* ModelState.AddModelError("", "Username and password incorrect");*/
                return View();
            }
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(DemoData model)
        {
            using (var db = new Assignment2Entities4())
            {
                try
                {
                    db.DemoDatas.Add(model);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.showerror = "";
                    return View("SignUp");
                }
               

            }
            return RedirectToAction("Login");

        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            using (var db = new Assignment2Entities3())
            {
                InfoRec data = new InfoRec()
                {
                    Activity = "Logout",
                    Time = DateTime.Now
                };

                db.InfoRecs.Add(data);
                db.SaveChanges();
            }
            return RedirectToAction("Login");
        }
    }
}