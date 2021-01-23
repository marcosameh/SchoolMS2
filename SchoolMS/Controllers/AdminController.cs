using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SchoolMS.Models;
using System.IO;

namespace SchoolMS.Controllers
{
    public class AdminController : Controller
    {
        CrudOperationEntities1 db = new CrudOperationEntities1();
        
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Display()
        {
            var data = db.Admin;
            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {

            var data = db.Admin.Find(Id);
            return View(data);
        }


       [HttpPost]
       //from body or from form
        public ActionResult Edit(Admin adm)
        {
            var old = db.Admin.Find(adm.Id);
            
            old.Salary = adm.Salary;
            old.Name = adm.Name;
            var user = Membership.GetUser(old.Name);
            Membership.UpdateUser(user);
            db.SaveChanges();
            
            
            return RedirectToAction("Display");
            
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {

            var data = db.Admin.Find(id);

            return View(data);

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var data = db.Admin.Find(id);
            //var url=Path.Combine("~/photo/", data.photo);

            //System.IO.File.Delete(url);
            db.Admin.Remove(data);
            Membership.DeleteUser(data.Name);
            
            db.SaveChanges();
            return RedirectToAction("Display");

        }
    }
}