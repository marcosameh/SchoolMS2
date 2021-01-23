using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SchoolMS.Models;
using System.IO;
using System.Globalization;
using System.Threading;

namespace SchoolMS.Controllers
{
    public class AccountController : Controller
    {
        CrudOperationEntities1 db = new CrudOperationEntities1();
        public ActionResult Login()
        {



            return View();
        }
        [HttpPost]
        public ActionResult login(Admin admin)
        {

             
            if (Membership.ValidateUser(admin.Name, admin.Password))
            {
                FormsAuthentication.SetAuthCookie(admin.Name, false);
                if (Roles.IsUserInRole(admin.Name,"Admin"))
                {
                    var data = db.Admin.FirstOrDefault(x=>x.Name.Equals(admin.Name));
          
                    Session["photo"] = data.photo;
                    return RedirectToAction("index", "Admin");
                }
                else if (Roles.IsUserInRole(admin.Name,"Tutor"))
                {
                    return RedirectToAction("Index", "Student");
                }

            }

            return View();
        }
        [Authorize(Roles ="Admin")]
        public ActionResult AdminRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminRegistration(Admin Adm,HttpPostedFileBase photo)
        {
            string Folderpath = Server.MapPath("~/photo/");
            string photoname = Guid.NewGuid() + Path.GetFileName(photo.FileName);
            string fullpath = Path.Combine(Folderpath, photoname);
            photo.SaveAs(fullpath);
            Adm.photo = photoname;
            Membership.CreateUser(Adm.Name, Adm.Password,Adm.ConfirmPassWord);
            Roles.AddUserToRole(Adm.Name,"Admin");

            db.Admin.Add(Adm);
            db.SaveChanges();
            return RedirectToAction("Display","Admin");
        }
      
        [Authorize(Roles = "Admin")]
        public ActionResult TutorRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TutorRegistration(Tutor tutor)
        {
            Membership.CreateUser(tutor.Name, tutor.PassWord,tutor.ConfirmPassword);
            Roles.AddUserToRole(tutor.Name, "Tutor");

            db.Tutor.Add(tutor);
            db.SaveChanges();
            return RedirectToAction("index", "Tutor");
        }
       
        public ActionResult StudentRegistration()
        {
            ViewBag.DepList = new SelectList(db.Department, "Department_id", "Department_name");
            return View();
        }
        [HttpPost]
        public ActionResult StudentRegistration(Student Std)
        {
           

            try
            {
                if (ModelState.IsValid)
                {
                    
                    Roles.AddUserToRole(Std.name, "Student");
                    db.Student.Add(Std);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index", "Student");
                }
                else


                {
                    ViewBag.DepList = new SelectList(db.Department, "Department_id", "Department_name");

                    return View(Std);
                }

            }
            catch (Exception e)
            {

                var data = e.Message;
                return View(data);

            }
        }
        [Authorize(Roles = "Admin,Tutor")]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
           
        }
    }
}