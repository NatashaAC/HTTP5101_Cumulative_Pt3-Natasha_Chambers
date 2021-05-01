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
        // GET: /Class/List
        /// <summary>
        ///    Routes to a dynamically generated "Class List" Page. 
        ///    Gathers information about all the classes in the database.
        /// </summary>
        /// <param name="SearchKey"> A string that represents a class's name, or cousre code </param>
        /// <returns> A dynamic webpage which displays a list of classes </returns>
        /// <example>
        ///     /Class/List
        /// </example>
        public ActionResult List(string SearchKey = null)
        {
            // Instantiating
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses(SearchKey);

            return View(Classes);
        }

        // GET: /Class/Show/{id}
        /// <summary>
        ///     Routes to a dynamically generated "Class Show" Page. 
        ///     Gathers information about a specific class from the database
        /// </summary>
        /// <param name="id"> An interger that represents Id of a class </param>
        /// <returns> A dynamic webpage which provides the current information of a class </returns>
        /// <example>
        ///     /Class/Show/9
        /// </example>
        public ActionResult Show(int id)
        {
            // Checking that the method is running
            Debug.WriteLine("The SHOW Method is running and the class_id is " + id);

            // Instantiating 
            ClassDataController controller = new ClassDataController();
            Class SelectedClass = controller.FindClass(id);

            return View(SelectedClass);
        }

        // GET: /Class/DeleteConfirmation/{id}
        /// <summary>
        ///     Routes to a dynamically generated "Class DeleteConfirmation" Page. 
        ///     Gathers information about a specific class that will be deleted from the database
        /// </summary>
        /// <param name="id"> Id of a class </param>
        /// <returns> A dynamic webpage which provides the current information of the class </returns>
        /// <example>
        ///     /Class/DeleteConfirmation/7
        /// </example>
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

        // POST: Class/Delete/{id}
        /// <summary>
        ///    Receives a POST request containing information about an existing class in the database, 
        ///    Conveys this information to the DeleteClass API, inorder
        ///    to remove the specific class from the database.
        ///    Redirects to the "Class List" page.
        /// </summary>
        /// <param name="id"> Id of a Class </param>
        /// <returns> A dynamic webpage which provides the current information of a class </returns>
        /// <example>
        ///     /Class/Delete/3
        /// </example>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Check that the method is working
            Debug.WriteLine("The DLETE Method is running and has deleted CLASSID " + id);

            // Instantiating
            ClassDataController controller = new ClassDataController();
            controller.DeleteClass(id);

            return RedirectToAction("List");
        }

        // GET: /Class/Add
        /// <summary>
        ///     Routes to a dynamically generated "Class Add" Page. 
        ///     Gathers information about a new class from a form 
        ///     that will be added to the database.
        /// </summary>
        /// <returns> A dynamic webpage which asks the user for new information regarding a class as part of a form. </returns>
        /// <exmple>
        ///     /Class/Add
        /// </exmple>
        public ActionResult Add()
        {
            return View();
        }

        // POST: /Class/Create
        /// <summary>
        ///    Receives a POST request containing information about a new class, 
        ///    Conveys this information to the AddClass API, inorder
        ///    to add the specific class to the database.
        ///    Redirects to the "Class List" page.
        /// </summary>
        /// <param name="ClassCode"> A string that represents a Class's code </param>
        /// <param name="ClassName"> A string that represents a Class's name </param>
        /// <param name="StartDate"> Start date of a class </param>
        /// <param name="FinishDate"> Finish date of a class </param>
        /// <returns> A dynamic webpage which provides a new class's information. </returns>
        /// <example>
        ///     /Class/Create
        ///     {
        ///         "ClassCode":"CALC1101",
        ///         "ClassName":"Introduction to Calculus",
        ///         "StartDate":"2020-09-14",
        ///         "FinishDate":"2021-01-09"
        ///     }
        /// </example>
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
        ///     That asks the user for new information as part of a form.
        ///     Gathers information from the school database.
        /// </summary>
        /// <param name="id"> Id of a Class </param>
        /// <returns>
        ///     A dynamic webpage which provides the current information of a class
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

        // POST: /Class/Update/{id}
        /// <summary>
        ///     Receives a POST request containing information about an existing class in the database, 
        ///     with new values. Conveys this information to the UpdateClass API, 
        ///     and redirects to the "Class Show" page of our updated teacher.
        /// </summary>
        /// <param name="id"> Id of a class </param>
        /// <param name="ClassCode"> A string that represents a Class's code </param>
        /// <param name="ClassName"> A string that represents a Class's name </param>
        /// <param name="StartDate"> Start date of a class </param>
        /// <param name="FinishDate"> Finish date of a class </param>
        /// <returns> A dynamic webpage which provides the current information of a class </returns>
        /// <example>
        ///     /Class/Update/10
        ///     {
        ///         "ClassCode":"http5205",
        ///         "ClassName":"Career Connections",
        ///         "StartDate":"2020-09-14",
        ///         "FinishDate":"2021-01-09"
        ///     }
        /// </example>
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