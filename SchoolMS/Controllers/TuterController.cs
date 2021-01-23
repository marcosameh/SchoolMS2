using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolMS.Models;
using System.Web.Security;

namespace SchoolMS.Controllers
{
    public class TuterController : Controller
    {
        // GET: Tuter
        CrudOperationEntities1 db = new CrudOperationEntities1();
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult TutorList()
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var Tutor = db.Tutor;
                return Json(new { Result = "OK", Records = Tutor });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        public JsonResult CreateTutor(Tutor tut)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                Roles.AddUserToRole(tut.Name,"Tutor");
                Membership.CreateUser(tut.Name, tut.PassWord, tut.ConfirmPassword);
                var Tutor = db.Tutor.Add(tut);
                db.SaveChanges();
                return Json(new { Result = "OK", Record = Tutor });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult UpdateTutor(Tutor tut)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var old=db.Tutor.Find(tut.Id);
                old.Name = tut.Name;
                old.Salary = tut.Salary;
                old.Age = tut.Age;
                //db.Entry(tut).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteTutor(int Id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var data = db.Tutor.Find(Id);
                db.Tutor.Remove(data);
                Membership.DeleteUser(data.Name);
                db.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}