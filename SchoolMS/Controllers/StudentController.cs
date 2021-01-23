using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SchoolMS.Models;
namespace SchoolMS.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student

        CrudOperationEntities1 db = new CrudOperationEntities1();
        public ActionResult Index()
        {
           var data= db.Student;
            
            return View(data);
        }
        public ActionResult Edit(int Id)
        {
            
            var data = db.Student.Find(Id);
            ViewBag.Department = new SelectList(db.Department, "Department_id", "Department_name", data.Department_id);
            return View(data);
        }


        [HttpPost]
        //from body or from form
        public ActionResult Edit(Student std)
        {
            var old = db.Student.Find(std.id);
            old.name = std.name;
            old.age = std.age;
            old.Email = std.Email;
            old.Department_id = std.Department_id;
            db.SaveChanges();
            return RedirectToAction("Index");
            


            


        }
        public ActionResult Delete(int id)
        {

            var data = db.Student.Find(id);
            ViewBag.Department = new SelectList(db.Department, "Department_id", "Department_name", data.Department_id);
            return View(data);

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            var data = db.Student.Find(id);
            db.Student.Remove(data);
            Membership.DeleteUser(data.name);
            db.SaveChanges();
            return RedirectToAction("index");

        }


    }
}