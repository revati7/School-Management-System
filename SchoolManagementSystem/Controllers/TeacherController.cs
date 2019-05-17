using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        //DataBase Context Object
        SMSTEntities smsContext = new SMSTEntities();

        public ActionResult DashBoard()
        {
            if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
            {
                return View();
            }
            else
            {
                return RedirectToAction("TeacherLogin","Home");
            }
        }

        //Update Tecaher Details -- Teacher/Update
        public ActionResult Update()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                {
                    tbl_Teacher teacher = new tbl_Teacher();
                    string mail = Session["Email"].ToString();
                    var validate = smsContext.tbl_User.Where(x => x.Email == mail).FirstOrDefault();
                    if (validate != null)
                    {
                        var query = smsContext.tbl_Teacher.Where(x => x.Email == mail).FirstOrDefault();
                        if (query == null)
                        {
                            return RedirectToAction("New", "Teacher");
                        }
                        else
                        {
                            return View(query);
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update(tbl_Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                    {
                        var teacherEdit = smsContext.tbl_Teacher.Where(t => t.Email == teacher.Email).FirstOrDefault();
                        teacherEdit.Name = teacher.Name;
                        teacherEdit.Gender = teacher.Gender;
                        teacherEdit.DOB = teacher.DOB;
                        teacherEdit.Contact = teacher.Contact;
                        teacherEdit.Email = teacher.Email;
                        teacherEdit.Name = teacher.Address;
                        teacherEdit.GradeID = null;

                        if (TryUpdateModel(teacherEdit))
                        {
                            smsContext.SaveChanges();
                            TempData["message"] = "Updated Successfully.";
                            return RedirectToAction("Details");
                        }
                        return RedirectToAction("Details");
                    }
                    else
                    {
                        return RedirectToAction("TeacherLogin", "Home");
                    }
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
            return View();
        }

        //Change Password Tecaher  -- Teacher/ChangePassword
        public ActionResult ChangePassword()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
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
                        return RedirectToAction("TeacherLogin", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        [HttpPost]
        public ActionResult ChangePassword(string ConfirmPassword, string Password,string OldPassword)
        {
            try
            {
                if (ConfirmPassword != "" && Password != "" && OldPassword != "")
                {
                    if (ModelState.IsValid)
                    {
                        if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
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
                                    return RedirectToAction("Details");
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
                            return RedirectToAction("TeacherLogin", "Home");
                        }
                    }
                }
                else
                {
                    if(OldPassword=="")
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


        //New Teacher -- Teacher/New
        public ActionResult New()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult New(string Name, string Gender, DateTime DOB, string Contact, string Email, string Address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                    {
                        tbl_Teacher newTeacher = new tbl_Teacher();
                        newTeacher.Name = Name;
                        newTeacher.Gender = Gender;
                        newTeacher.DOB = DOB;
                        newTeacher.Contact = Contact;
                        newTeacher.Email = Session["Email"].ToString();
                        newTeacher.Address = Address;
                        smsContext.tbl_Teacher.Add(newTeacher);
                        // executes the appropriate commands to implement the changes to the database  
                        smsContext.SaveChanges();
                        TempData["message"] = "Details added successfully!";
                        return RedirectToAction("Details");
                    }
                    else
                    {
                        return RedirectToAction("TeacherLogin", "Home");
                    }
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
            return View();
        }


        //Details Page -- Teacher/Details
        public ActionResult Details()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                {
                    tbl_Teacher teacher = new tbl_Teacher();
                    string mail = Session["Email"].ToString();
                    teacher = smsContext.tbl_Teacher.Where(t => t.Email == mail).FirstOrDefault();
                    return View(teacher);
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Attendance Page -- Teacher/Attendance
        public ActionResult Attendance()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                {
                    string mail = Session["Email"].ToString();
                    var validate = smsContext.tbl_Teacher.Where(x => x.Email == mail).FirstOrDefault();
                    var query = from stud in smsContext.tbl_Student where stud.GradeID == validate.GradeID select stud;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Attendance(string name)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                {
                    string mail = Session["Email"].ToString();
                    var validate = smsContext.tbl_Teacher.Where(x => x.Email == mail).FirstOrDefault();
                    var query = from students in smsContext.tbl_Student where students.GradeID == validate.GradeID && students.StudentName == name select students;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

       
        //Mark Absence -- Teacher/Absence
        public ActionResult Absence(int? studID, int? gradeID)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                {
                    tbl_Absence obj = new tbl_Absence();
                    obj.StudID = studID;
                    obj.GradeID = gradeID;
                    obj.AbsenceDate = DateTime.Now;
                    return View(obj);
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Absence(int StudID, int GradeID, DateTime AbsenceDate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                    {
                        tbl_Absence absence = new tbl_Absence();
                        absence.StudID = StudID;
                        absence.GradeID = GradeID;
                        absence.AbsenceDate = AbsenceDate;
                        smsContext.tbl_Absence.Add(absence);
                        smsContext.SaveChanges();
                        TempData["message"] = "Marked Successfully!!";
                        return RedirectToAction("Attendance");
                    }
                    else
                    {
                        return RedirectToAction("TeacherLogin", "Home");
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

        //Attendance Records -- Teacher/Records
        public ActionResult Records()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                {
                    string mail = Session["Email"].ToString();
                    var validate = smsContext.tbl_Teacher.Where(x => x.Email == mail).FirstOrDefault();
                    var query = from students in smsContext.tbl_Absence where students.GradeID == validate.GradeID select students;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Records(Nullable<DateTime> date)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Teacher")
                {
                    string mail = Session["Email"].ToString();
                    var validate = smsContext.tbl_Teacher.Where(x => x.Email == mail).FirstOrDefault();
                    var query = from students in smsContext.tbl_Absence where students.GradeID == validate.GradeID && students.AbsenceDate == date select students;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("TeacherLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }
    }
}