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
    public class AdminController : Controller
    {
        //DataBase Context Object
        SMSTEntities smsContext = new SMSTEntities();

        //public ActionResult Validate(string role, string action)
        //{
        //    if (role == "Admin")
        //    {
        //        return RedirectToAction(action,"Admin");
        //    }
        //    else
        //    {
        //        return RedirectToAction("AdminLogin","Home");
        //    }
        //}

        // Admin/DashBoard
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]  //For disabling back button by not storing cache files for this view
        public ActionResult DashBoard()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Maintain Teacher -- Admin/Teacher
        public ActionResult Teachers()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var query = from teachers in smsContext.tbl_Teacher select teachers;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Teachers(int gradeID)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var query = from teachers in smsContext.tbl_Teacher where teachers.GradeID == gradeID select teachers;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        //Edit Teacher -- Admin/EditTeacher
        public ActionResult EditTeacher(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Teacher teacher = new tbl_Teacher();
                    teacher = smsContext.tbl_Teacher.Find(id);
                    return View(teacher);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditTeacher(tbl_Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                    {
                        var teacherEdit = smsContext.tbl_Teacher.Where(u => u.TeacherID == teacher.TeacherID).FirstOrDefault();
                        teacherEdit.Name = teacher.Name;
                        teacherEdit.Gender = teacher.Gender;
                        teacherEdit.DOB = teacher.DOB;
                        teacherEdit.Contact = teacher.Contact;
                        teacherEdit.Email = teacher.Email;
                        teacherEdit.Name = teacher.Address;

                        if (TryUpdateModel(teacherEdit))
                        {
                            smsContext.SaveChanges();
                            TempData["message"] = "Updated Successfully!!";
                            return RedirectToAction("Teachers");
                        }
                        return RedirectToAction("Teachers");
                    }
                    else
                    {
                        return RedirectToAction("AdminLogin", "Home");
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

        //Details Teacher -- Admin/DetailsTeacher
        public ActionResult DetailsTeacher(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Teacher teacher = new tbl_Teacher();
                    teacher = smsContext.tbl_Teacher.Find(id);
                    return View(teacher);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Delete Teacher -- Admin/DeleteTeacher
        public ActionResult DeleteTeacher(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Teacher teacher = new tbl_Teacher();
                    teacher = smsContext.tbl_Teacher.Find(id);
                    return View(teacher);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteTeacher(int id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var teacher = smsContext.tbl_Teacher.Where(t => t.TeacherID == id).FirstOrDefault();
                    smsContext.tbl_Teacher.Remove(teacher);
                    smsContext.SaveChanges();
                    TempData["message"] = "Deleted Successfully!!";
                    return RedirectToAction("Teachers", "Admin");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Maintain Students -- Admin/Students
        public ActionResult Students()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var query = from students in smsContext.tbl_Student select students;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Students(int gradeID,string name)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    if (gradeID == 0)
                    {
                        var query = from students in smsContext.tbl_Student where students.StudentName == name select students;
                        return View(query);
                    }
                    else if(name == "")
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
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        //New Student -- Admin/CreateStudent
        public ActionResult CreateStudent()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Student stud = new tbl_Student();
                    stud.DOB = DateTime.Now;
                    return View(stud);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateStudent(string StudentName, int GradeID, string Gender, DateTime DOB, string BloodGroup, string Contact, string Address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                    {
                        if (StudentName != "" && StudentName != null ||
                            GradeID == 0 ||
                            Gender != "" && Gender != null ||
                            DOB != null && DOB.ToString() != "" || Contact != "" && Contact != null ||
                            Address != "" || Address != null)
                        {
                            tbl_Student newStudent = new tbl_Student();
                            newStudent.StudentName = StudentName;
                            newStudent.GradeID = GradeID;
                            newStudent.Gender = Gender;
                            newStudent.DOB = DOB;
                            newStudent.BloodGroup = BloodGroup;
                            newStudent.Contact = Contact;
                            newStudent.Address = Address;
                            smsContext.tbl_Student.Add(newStudent);
                            // executes the appropriate commands to implement the changes to the database  
                            smsContext.SaveChanges();
                            TempData["message"] = "Created Successfully!!";
                            return RedirectToAction("Students");
                        }
                        else
                        {
                            return RedirectToAction("CreateStudent", "Admin");
                        }
                    }
                    else
                    {
                        return RedirectToAction("AdminLogin", "Home");
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

        //Edit Student -- Admin/EditStudent
        public ActionResult EditStudent(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Student student = new tbl_Student();
                    student = smsContext.tbl_Student.Find(id);
                    return View(student);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditStudent(tbl_Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                    {
                        var studentEdit = smsContext.tbl_Student.Where(s => s.StudentID == student.StudentID).FirstOrDefault();
                        studentEdit.StudentName = student.StudentName;
                        studentEdit.GradeID = student.GradeID;
                        studentEdit.Gender = student.Gender;
                        studentEdit.DOB = student.DOB;
                        studentEdit.BloodGroup = student.BloodGroup;
                        studentEdit.Contact = student.Contact;
                        studentEdit.Address = student.Address;
                        if (TryUpdateModel(studentEdit))
                        {
                            smsContext.SaveChanges();
                            TempData["message"] = "Updated Successfully!!";
                            return RedirectToAction("Students");
                        }
                        return RedirectToAction("Students");
                    }
                    else
                    {
                        return RedirectToAction("AdminLogin", "Home");
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
        public ActionResult StudentDetails(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Student student = new tbl_Student();
                    student = smsContext.tbl_Student.Find(id);
                    return View(student);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Delete Student -- Admin/DeleteStudent
        public ActionResult DeleteStudent(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Student student = new tbl_Student();
                    student = smsContext.tbl_Student.Find(id);
                    return View(student);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteStudent(int id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var student = smsContext.tbl_Student.Where(t => t.StudentID == id).FirstOrDefault();
                    smsContext.tbl_Student.Remove(student);
                    smsContext.SaveChanges();
                    TempData["message"] = "Deleted Successfully!!";
                    return RedirectToAction("Students", "Admin");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        //Maintain Grades -- Admin/Grades
        public ActionResult Grades()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var query = from grades in smsContext.tbl_Grade select grades;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }
        [HttpPost]
        public ActionResult Grades(int gradeID)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var query = from grades in smsContext.tbl_Grade where grades.GradeID == gradeID select grades;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        //New User -- -- Admin/CreateGrade
        public ActionResult CreateGrade()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateGrade(string GradeName, string Description)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                    {
                        if (GradeName != "" && Description != "" && GradeName != null && Description != null)
                        {
                            tbl_Grade newGrade = new tbl_Grade();
                            newGrade.GradeName = GradeName;
                            newGrade.Description = Description;
                            smsContext.tbl_Grade.Add(newGrade);
                            // executes the appropriate commands to implement the changes to the database  
                            smsContext.SaveChanges();
                            TempData["message"] = "Craeted Successfully!!";
                            return RedirectToAction("Grades");
                        }
                        else
                        {
                            return RedirectToAction("CreateGrade", "Admin");
                        }
                    }
                    else
                    {
                        return RedirectToAction("AdminLogin", "Home");
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

        //Edit Grade -- Admin/EditGrade
        public ActionResult EditGrade(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Grade grade = new tbl_Grade();
                    grade = smsContext.tbl_Grade.Find(id);
                    return View(grade);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditGrade(tbl_Grade grade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                    {
                        var gradeEdit = smsContext.tbl_Grade.Where(g => g.GradeID == grade.GradeID).FirstOrDefault();
                        gradeEdit.GradeID = grade.GradeID;
                        gradeEdit.GradeName = grade.GradeName;
                        gradeEdit.Description = grade.Description;

                        if (TryUpdateModel(gradeEdit))
                        {
                            smsContext.SaveChanges();
                            TempData["message"] = "Updated Successfully!!";
                            return RedirectToAction("Grades");
                        }
                        return RedirectToAction("Grades");
                    }
                    else
                    {
                        return RedirectToAction("AdminLogin", "Home");
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

        //Details Grade -- Admin/GradeDetails
        public ActionResult GradeDetails(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Grade grade = new tbl_Grade();
                    grade = smsContext.tbl_Grade.Find(id);
                    return View(grade);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Delete Grade -- Admin/DeleteGrade
        public ActionResult DeleteGrade(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_Grade grade = new tbl_Grade();
                    grade = smsContext.tbl_Grade.Find(id);
                    return View(grade);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteGrade(int id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var teacher = smsContext.tbl_Teacher.Where(t => t.TeacherID == id).FirstOrDefault();
                    smsContext.tbl_Teacher.Remove(teacher);
                    smsContext.SaveChanges();
                    TempData["message"] = "Deleted Successfully!!";
                    return RedirectToAction("Teachers", "Admin");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }


        //Creating and Managing Users -- Admin/Users
        public ActionResult Users()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var query = from users in smsContext.tbl_User select users;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Users(string role)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var query = from users in smsContext.tbl_User where users.UserRole == role select users;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //New User -- -- Admin/Create
        public ActionResult Create()
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(string email, string password , string role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                    {
                        if (email != "" && password != "" && role != "")
                        {
                            tbl_User newUser = new tbl_User();
                            newUser.Email = email;
                            newUser.UserRole = role;
                            //encrypting password using SHA1 encryption
                            var hashedPassword = "";
                            hashedPassword = Crypto.SHA1(password);
                            newUser.Password = hashedPassword;
                            //Adds an entity in a pending insert state to this System.Data.Linq.Table<TEntity>and parameter is the entity which to be added  
                            smsContext.tbl_User.Add(newUser);
                            // executes the appropriate commands to implement the changes to the database  
                            smsContext.SaveChanges();
                            TempData["message"] = "Created Successfully!!";
                            return RedirectToAction("Users");
                        }
                        else
                        {
                            return RedirectToAction("Create", "Admin");
                        }
                    }
                    else
                    {
                        return RedirectToAction("AdminLogin", "Home");
                    }
                }
                TempData["message"] = "All Fields Required!";
                return View();
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Edit User-- Admin/Edit
        public ActionResult Edit(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_User user = new tbl_User();
                    user = smsContext.tbl_User.Find(id);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(tbl_User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                    {
                        var userEdit = smsContext.tbl_User.Where(u => u.UserID == user.UserID).FirstOrDefault();
                        userEdit.Email = user.Email;
                        userEdit.Password = user.Password;
                        //var hashedPassword = "";
                        //hashedPassword = Crypto.SHA1(user.Password);
                        //userEdit.Password = hashedPassword;
                        userEdit.UserRole = user.UserRole;
                        if (TryUpdateModel(userEdit))
                        {
                            smsContext.SaveChanges();
                            TempData["message"] = "Updated Successfully!!";
                            return RedirectToAction("Users");
                        }
                        return RedirectToAction("Users");
                    }
                    else
                    {
                        return RedirectToAction("AdminLogin", "Home");
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

        //Delete User-- Admin/Delete
        public ActionResult Delete(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_User user = new tbl_User();
                    user = smsContext.tbl_User.Find(id);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    var user = smsContext.tbl_User.Where(u => u.UserID == id).FirstOrDefault();
                    if (user.UserRole == "Teacher")
                    {
                        var teacher = smsContext.tbl_Teacher.Where(t => t.Email == user.Email).FirstOrDefault();
                        smsContext.tbl_Teacher.Remove(teacher);
                        smsContext.SaveChanges();
                    }
                    smsContext.tbl_User.Remove(user);
                    smsContext.SaveChanges();
                    
                    TempData["message"] = "Deleted Successfully!!";
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //Details of Users-- Admin/Details
        public ActionResult Details(int? id)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    tbl_User user = new tbl_User();
                    user = smsContext.tbl_User.Find(id);
                    return View(user);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
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