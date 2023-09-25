using Students.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Students.Controllers
{
    public class StudentController : Controller
    {

        db_testEntities dbObj = new db_testEntities();

        // GET: Student

        public ActionResult Student()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddStudent(tbl_Student model)
        {
            tbl_Student obj = new tbl_Student();
            obj.ID= model.ID;
            obj.Name = model.Name;
            obj.FName = model.FName;
            obj.Email = model.Email;
            obj.Mobile = model.Mobile;
            obj.Description = model.Description;

            if (model.ID == 0)
            {
                dbObj.tbl_Student.Add(obj);
                dbObj.SaveChanges();
            }
            else
            {
                dbObj.Entry(obj).State = (System.Data.Entity.EntityState)EntityState.Modified;
                dbObj.SaveChanges();

            }

            return Json(obj, JsonRequestBehavior.AllowGet);
            
        }



        [WebMethod]
        public JsonResult studentList()
        {
            var res = dbObj.tbl_Student.ToList();
            return Json(res);
        }




        public JsonResult stdDel(int id)
        {
            var res = dbObj.tbl_Student.Find(id);
            if (res != null)
            {
                dbObj.tbl_Student.Remove(res);
                dbObj.SaveChanges();
            }
            var list = dbObj.tbl_Student.ToList();
            return Json(list);
        }


        public ActionResult stdEdit(int id)
        {
            var res = dbObj.tbl_Student.Find(id);
            return View(res);
        }

    }
}