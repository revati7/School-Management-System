using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class CashierController : Controller
    {
        SMSTEntities smsContext = new SMSTEntities();

        //Cashier Dashboard -- Cashier/DashBoard
        public ActionResult DashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //Students List -- Cashier/Students
        public ActionResult Students()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    var query = from students in smsContext.tbl_Student select students;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("CashierLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Students(int gradeID, string name)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    if (gradeID == 0)
                    {
                        var query = from students in smsContext.tbl_Student where students.StudentName == name select students;
                        return View(query);
                    }
                    else if (name == "")
                    {
                        var query = from students in smsContext.tbl_Student where students.GradeID == gradeID select students;
                        return View(query);
                    }
                    else
                    {
                        var query = from students in smsContext.tbl_Student where students.GradeID == gradeID && students.StudentName == name select students;
                        return View(query);
                    }
                }
                else
                {
                    return RedirectToAction("Cashier", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Fees Payment -- Cashier/Payment
        public ActionResult Payment(int? studID, int? gradeID)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    tbl_Fees obj = new tbl_Fees();
                    obj.StudentID = studID;
                    obj.GradeID = gradeID;
                    return View(obj);
                }
                else
                {
                    return RedirectToAction("CashierLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Payment(int StudentID, int GradeID, float Paid, float Total)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                    {
                        tbl_Fees pay = new tbl_Fees();
                        pay.PaymentDate = DateTime.Now;
                        pay.StudentID = StudentID;
                        pay.GradeID = GradeID;
                        pay.Paid = Paid;
                        pay.Total = Total;
                        pay.Pending = pay.Total - Paid;
                        smsContext.tbl_Fees.Add(pay);
                        smsContext.SaveChanges();
                        TempData["message"] = "Payment Completed Successfully!!";
                        var query = smsContext.tbl_Fees.Where(x => x.StudentID == StudentID && x.GradeID == GradeID).FirstOrDefault();
                        return RedirectToAction("Invoice","Cashier", new { id = query.InvoiceID});
                    }
                    else
                    {
                        return RedirectToAction("Cashier", "Home");
                    }
                }
                return View();
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Fees Verify -- Cashier/Verify
        public ActionResult Verify(int? studID, int? gradeID)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    var query = smsContext.tbl_Fees.Where(x => x.StudentID == studID && x.GradeID == gradeID).FirstOrDefault();
                    if(query == null)
                    {
                        return RedirectToAction("Payment", "Cashier", new { studID, gradeID});
                    }
                    else if(query != null && query.Pending != 0)
                    {
                        return RedirectToAction("Update", "Cashier", new { studID, gradeID });
                    }
                    else
                    {
                        TempData["message"] = "Fees Paid Already!!";
                        return RedirectToAction("Invoice", "Cashier", new { id = query.InvoiceID });
                    }
                }
                else
                {
                    return RedirectToAction("CashierLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "No prior payment record.";
                return RedirectToAction("Students", "Cashier");
            }
        }

        //Fees Payment -- Cashier/Payment
        public ActionResult Update(int? studID, int? gradeID)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    var query = smsContext.tbl_Fees.Where(x => x.StudentID == studID && x.GradeID == gradeID && x.Pending != 0).FirstOrDefault();
                    return View(query);
                }
                else
                {
                    return RedirectToAction("CashierLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "No prior payment record.";
                return RedirectToAction("Students","Cashier");
            }
        }

        [HttpPost]
        public ActionResult Update(int StudentID, int GradeID, float Pending, float Total, float Paid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                    {
                        tbl_Fees fees = smsContext.tbl_Fees.Where(t => t.StudentID == StudentID && t.GradeID == GradeID && t.Pending != 0).FirstOrDefault();
                        fees.StudentID = StudentID;
                        fees.GradeID = GradeID;
                        fees.PaymentDate = DateTime.Now;
                        fees.Paid = fees.Paid + Paid;
                        fees.Pending = Pending - Paid;
                        fees.Total = Total;
                        smsContext.SaveChanges();
                        TempData["message"] = "Updated Successfully.";
                        var query = smsContext.tbl_Fees.Where(x => x.StudentID == StudentID && x.GradeID == GradeID).FirstOrDefault();
                        return RedirectToAction("Invoice", "Cashier", new { id = query.InvoiceID });
                    }
                    else
                    {
                        return RedirectToAction("Cashier", "Home");
                    }
                }
                return View();
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        //Details Student -- Admin/StudentDetails
        public ActionResult Invoice(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    tbl_Fees fees = new tbl_Fees();
                    fees = smsContext.tbl_Fees.Find(id);
                    return View(fees);
                }
                else
                {
                    return RedirectToAction("CashierLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        //Attendance Records -- Teacher/Records
        public ActionResult Records()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    var query = from fees in smsContext.tbl_Fees select fees;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("CashierLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Records(Nullable<DateTime> date, int gradeID)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    if (date == null)
                    {
                        var query = from fees in smsContext.tbl_Fees where fees.GradeID == gradeID select fees;
                        return View(query);
                    }
                    else if (gradeID == 0)
                    {
                        var query = from fees in smsContext.tbl_Fees where fees.PaymentDate == date select fees;
                        return View(query);
                    }
                    else
                    {
                        var query = from fees in smsContext.tbl_Fees where fees.PaymentDate == date && fees.GradeID == gradeID select fees;
                        return View(query);
                    }
                }
                else
                {
                    return RedirectToAction("CashierLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Change Password Cashier -- Cashier/ChangePassword
        public ActionResult ChangePassword()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                {
                    tbl_User teacher = new tbl_User();
                    string mail = Session["Email"].ToString();
                    var validate = smsContext.tbl_User.Where(x => x.Email == mail).FirstOrDefault();
                    if (validate != null)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("CashierLogin", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("CashierLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        [HttpPost]
        public ActionResult ChangePassword(string ConfirmPassword, string Password, string OldPassword)
        {
            try
            {
                if (ConfirmPassword != "" && Password != "" && OldPassword != "")
                {
                    if (ModelState.IsValid)
                    {
                        if (Session["UserID"] != null && Session["UserRole"].ToString() == "Cashier")
                        {

                            int id = Convert.ToInt32(Session["UserID"]);
                            var user = smsContext.tbl_User.Where(u => u.UserID == id).FirstOrDefault();

                            //encrypting password using SHA1 encryption
                            var oldPassword = "";
                            oldPassword = Crypto.SHA1(OldPassword);
                            var password = Crypto.SHA1(Password);
                            var confirmPassword = Crypto.SHA1(ConfirmPassword);
                            if (user.Password == oldPassword)
                            {


                                // executes the appropriate commands to implement the changes to the database  
                                if (password == confirmPassword)
                                {
                                    user.Password = password;
                                    smsContext.SaveChanges();
                                    TempData["message"] = "Password Changed Successfully!";
                                    return RedirectToAction("DashBoard");
                                }
                                else
                                {
                                    TempData["message"] = "New Password and Confirmed Password does not match!";
                                    return View();
                                }


                            }
                            else
                            {

                                TempData["message"] = "Incorrect Password!";
                                return View();
                            }
                        }
                        else
                        {
                            return RedirectToAction("CashierLogin", "Home");
                        }
                    }
                }
                else
                {
                    if (OldPassword == "")
                        ModelState.AddModelError("OldPassword", "Blank Field not allowed!");
                    if (Password == "")
                        ModelState.AddModelError("Password", "Blank Field not allowed!!");
                    if (ConfirmPassword == "")
                        ModelState.AddModelError("ConfirmPassword", "Blank Field not allowed!");
                    return View();

                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
            return View();
        }

    }
}