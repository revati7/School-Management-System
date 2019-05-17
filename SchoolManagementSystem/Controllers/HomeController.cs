using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        //Admin Login -- Home/AdminLogin
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(tbl_User objUser)
        {
            try
            {
                using (SMSTEntities smsContext = new SMSTEntities())
                {
                    var hashedPassword = Crypto.SHA1(objUser.Password);
                    var obj = smsContext.tbl_User.Where(a => a.Email.Equals(objUser.Email) && a.Password.Equals(hashedPassword)).First();

                    if (obj != null && obj.UserRole == "Admin")
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["Email"] = obj.Email.ToString();
                        Session["UserRole"] = obj.UserRole.ToString();
                        var id = Session["UserID"].ToString();
                        var role = Session["UserRole"].ToString();
                        return RedirectToAction("DashBoard", "Admin");
                    }
                    else
                    {
                        TempData["message"] = "Incorrect Credentials or You are not authorized.";
                        return RedirectToAction("AdminLogin");
                    }
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Incorrect Credentials";
                return View();
            }
        }

        //Cashier Login -- Home/CashierLogin
        public ActionResult CashierLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CashierLogin(tbl_User objUser)
        {
            try
            {
                using (SMSTEntities smsContext = new SMSTEntities())
                {
                    var hashedPassword = Crypto.SHA1(objUser.Password);
                    var obj = smsContext.tbl_User.Where(a => a.Email.Equals(objUser.Email) && a.Password.Equals(hashedPassword)).First();

                    if (obj != null && obj.UserRole == "Cashier")
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["Email"] = obj.Email.ToString();
                        Session["UserRole"] = obj.UserRole.ToString();
                        return RedirectToAction("DashBoard", "Cashier");
                    }
                    else
                    {
                        TempData["message"] = "Incorrect Credentials or You are not authorized.";
                        return RedirectToAction("CashierLogin");
                    }
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Incorrect Credentials";
                return View();
            }
            
        }

        //Teacher Login -- Home/TeacherLogin
        public ActionResult TeacherLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TeacherLogin(tbl_User objUser)
        {
            try
            {
                using (SMSTEntities smsContext = new SMSTEntities())
                {
                    var hashedPassword = Crypto.SHA1(objUser.Password);
                    var obj = smsContext.tbl_User.Where(a => a.Email.Equals(objUser.Email) && a.Password.Equals(hashedPassword)).First();

                    if (obj != null && obj.UserRole == "Teacher")
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["Email"] = obj.Email.ToString();
                        Session["UserRole"] = obj.UserRole.ToString();
                        return RedirectToAction("DashBoard", "Teacher");
                    }
                    else
                    {
                        TempData["message"] = "Incorrect Credentials or You are not authorized.";
                        return RedirectToAction("TeacherLogin");
                    }
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Incorrect Credentials";
                return View();
            }
        }

        //Logout -- Home/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Home", "Home");
        }

        //Home Page -- Home/Home
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}