using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using HTTP5101_Cumulative_Pt3_Natasha_Chambers.Models;

namespace HTTP5101_Cumulative_Pt3_Natasha_Chambers.Controllers
{
    public class TeacherController : Controller
    {
        // GET: /Teacher/List
        /// <summary>
        ///     View that displays a list of teachers from the school database
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns></returns>
        /// <example>
        ///     GET: /Teacher/List
        /// </example>
        [HttpGet]
        public ActionResult List(string SearchKey = null)
        {
            // Instantiating 
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);
        }

        // GET: /Teacher/Show/{id}
        /// <summary>
        ///     View that displays a specific teacher from the school database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>
        ///     GET: /Teacher/Show/5
        /// </example>
        [HttpGet]
        public ActionResult Show(int id)
        {
            // Checking that the method is running
            Debug.WriteLine("The SHOW Method is running and the teacher_id is " + id);

            // Instantiating 
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        // GET: /Teacher/DeleteConfirmation/{id}
        /// <summary>
        ///     View that displays a specific teacher that will be deleted from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>
        ///     /Teacher/DeleteConfirmation/9
        /// </example>
        [HttpGet]
        public ActionResult DeleteConfirmation(int id)
        {
            // Checking that the method is running
            Debug.WriteLine("The DELETE CONFIRMATION Method is running and is going to delete TEACHER ID " + id);

            // Instantiating 
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        // POST: /Teacher/Delete/{id}
        /// <summary>
        ///     Deletes a specific teacher from the database and redirects to the List of teachers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>
        ///     POST: /Teacher/Delete/5
        /// </example>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Checking that the method is running
            Debug.WriteLine("The DELETE Method is running and has deleted teacher_id " + id);

            // Instantiating 
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        // GET: /Teacher/New
        /// <summary>
        ///     View that allows a user to input information about a teacher
        /// </summary>
        /// <returns></returns>
        /// <example>
        ///     GET: /Teacher/New
        /// </example>
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        // POST: /Teacher/Create
        /// <summary>
        ///     Adds a new teacher's information to the database
        /// </summary>
        /// <param name="TeacherFname"></param>
        /// <param name="TeacherLname"></param>
        /// <param name="EmployeeNumber"></param>
        /// <param name="HireDate"></param>
        /// <param name="Salary"></param>
        /// <returns></returns>
        /// <example>
        ///     POST: /Teacher/Create
        /// </example>
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            // Checking that the method is running
            Debug.WriteLine("The CREATE Method is running!");

            // Checking that the inputs from the form has been received 
            Debug.WriteLine("Employee Number: " + EmployeeNumber + ", First name: " + TeacherFname + ", Last name: " + TeacherLname);
            Debug.WriteLine("Hire Date: " + HireDate);
            Debug.WriteLine("Salary: " + Salary);

            // Validating 
            if (TeacherFname == "" || TeacherLname == "" || EmployeeNumber == "")
            {
                return RedirectToAction("New");
            }
            else
            {
                // New Teacher Object
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                // Instantiating 
                TeacherDataController controller = new TeacherDataController();
                controller.AddTeacher(NewTeacher);

                return RedirectToAction("List");
            }
        }

        // GET: /Teacher/Update/{id}
        /// <summary>
        ///     Routes to a dynamically generated "Teacher Update" Page. 
        ///     Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the 
        /// teacher and asks the user for new information as part of a form.</returns>
        /// <example>
        ///     GET: /Teacher/Update/6
        /// </example>
        [HttpGet]
        public ActionResult Update(int id)
        {
            // Instantiating 
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        // POST: /Teacher/Update/10
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            // New Teacher Object
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            // Instantiating
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, NewTeacher);

            return RedirectToAction("Show/" + id);
        }
    }
}