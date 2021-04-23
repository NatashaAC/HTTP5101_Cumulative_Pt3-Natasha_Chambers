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
            Class SelectedClass = controller.FindClass(id);

            return View(SelectedClass);
        }

        // GET: Class/DeleteConfirmation
        [HttpGet]
        public ActionResult DeleteConfirmation(int id)
        {
            // Checking that the method is working
            Debug.WriteLine("The DELETE CONFIRMATION Method is running and is going to delete CLASS ID " + id);

            // Instantiating
            ClassDataController controller = new ClassDataController();
            Class SelectedClass = controller.FindClass(id);

            return View(SelectedClass);
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

        // GET: /Class/Update/{id}
        /// <summary>
        ///     Routes to a dynamically generated "Class Update" Page. 
        ///     Gathers information from the school database.
        /// </summary>
        /// <param name="id"> Id of a Class </param>
        /// <returns>
        ///     A dynamic "Update Class" webpage which provides the current information of the 
        ///     class and asks the user for new information as part of a form.
        /// </returns>
        /// <example>
        ///     GET: /Class/Update/9
        /// </example>
        [HttpGet]
        public ActionResult Update(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class SelectedClass = controller.FindClass(id);

            return View(SelectedClass);
        }

        // POST: /Teacher/Update/{id}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ClassCode"></param>
        /// <param name="ClassName"></param>
        /// <param name="StartDate"></param>
        /// <param name="FinishDate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(int id, string ClassCode, string ClassName, DateTime StartDate, DateTime FinishDate)
        {
            // New Class Object
            Class SelectedClass = new Class();
            SelectedClass.ClassCode = ClassCode;
            SelectedClass.ClassName = ClassName;
            SelectedClass.StartDate = StartDate;
            SelectedClass.FinishDate = FinishDate;

            // Instantiating
            ClassDataController controller = new ClassDataController();
            controller.UpdateClass(id, SelectedClass);

            return RedirectToAction("Show/" + id);
        }
    }
}