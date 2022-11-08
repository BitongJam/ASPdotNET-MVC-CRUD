using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class StudentController : Controller
    {
        static string sampleVal = string.Empty;
        // 1. *************RETRIEVE ALL STUDENT DETAILS ******************
        // GET: Student
        public ActionResult Index()
        {
            StudentDBHandle dbhandle = new StudentDBHandle();
            ModelState.Clear();
            ViewBag.Message = sampleVal;
            return View(dbhandle.GetStudent());

        }

        // 2. *************ADD NEW STUDENT ******************
        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(StudentModel smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StudentDBHandle sdb = new StudentDBHandle();
                    if (sdb.AddStudent(smodel))
                    {
                        ViewBag.Message = "Student Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch(Exception x)
            {
                ViewBag.Message = x.Message;
                return View();
            }
        }

        // 3. ************* EDIT STUDENT DETAILS ******************
        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            StudentDBHandle sdb = new StudentDBHandle();
            return View(sdb.GetStudent().Find(smodel => smodel.Id == id));
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StudentModel smodel)
        {
            try
            {
                StudentDBHandle sdb = new StudentDBHandle();
                sdb.UpdateDetails(smodel);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // 4. ************* DELETE STUDENT DETAILS ******************
        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                StudentDBHandle sdb = new StudentDBHandle();
                if (sdb.DeleteStudent(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                    sampleVal = "Delete Success";
                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
    }
