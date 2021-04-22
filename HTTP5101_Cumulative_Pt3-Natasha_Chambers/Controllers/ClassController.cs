using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;  
using HTTP5101_Cumulative_Pt3_Natasha_Chambers.Models;

namespace HTTP5101_Cumulative_Pt3_Natasha_Chambers.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class/List
        public ActionResult List(string SearchKey = null)
        {
            // Instantiating
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses(SearchKey);

            return View(Classes);
        }

        // GET: Class/Show
        public ActionResult Show(int id)
        {
            // Instantiating 
            ClassDataController controller = new ClassDataController();
            Class NewClass = controller.FindClass(id);

            return View(NewClass);
        }

        // POST: Class/DeleteConfirmation
        public ActionResult DeleteConfirmation(int id)
        {
            // Checking that the method is working
            Debug.WriteLine("The DELETE CONFIRMATION Method is running and is going to delete CLASS ID " + id);

            // Instantiating
            ClassDataController controller = new ClassDataController();
            Class NewClass = controller.FindClass(id);

            return View(NewClass);
        }

        // GET: Class/Delete
        public ActionResult Delete(int id)
        {
            // Check that the method is working
            Debug.WriteLine("The DLETE Method is running and has deleted CLASSID " + id);

            // Instantiating
            ClassDataController controller = new ClassDataController();
            controller.DeleteClass(id);

            return RedirectToAction("List");
        }

        // GET: /Class/New
        public ActionResult New()
        {
            return View();
        }

        // POST: /Class/Create
        [HttpPost]
        public ActionResult Create(string ClassCode, string ClassName, DateTime StartDate, DateTime FinishDate)
        {
            // Checking that the method is running
            Debug.WriteLine("The CREATE Method is running!");

            // Checking that the inputs from the form has been received 
            Debug.WriteLine("Class Code: " + ClassCode + ", Class Name: " + ClassName);
            Debug.WriteLine("From: " + StartDate + " to " + FinishDate);

            // Validating 
            if (ClassCode == "" || ClassName == "")
            {
                return RedirectToAction("New");
            }
            else
            {
                // New Teacher Object
                Class NewClass = new Class();
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;

                // Instantiating 
                ClassDataController controller = new ClassDataController();
                controller.AddClass(NewClass);

                return RedirectToAction("List");
            }
        }
    }
}