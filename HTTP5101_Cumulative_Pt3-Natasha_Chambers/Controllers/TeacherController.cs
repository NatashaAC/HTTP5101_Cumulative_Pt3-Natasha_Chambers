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
        /// <param name="SearchKey"> A teacher's first name, last name or both </param>
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
        /// <param name="id"> Id of a teacher </param>
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
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        // GET: /Teacher/DeleteConfirmation/{id}
        /// <summary>
        ///     View that displays a specific teacher that will be deleted from the database
        /// </summary>
        /// <param name="id"> Id of a teacher </param>
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
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        // POST: /Teacher/Delete/{id}
        /// <summary>
        ///     Deletes a specific teacher from the database and redirects to the List of teachers
        /// </summary>
        /// <param name="id"> Id of a teacher </param>
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
        ///     Routes to a dynamically generated "Teacher New" Page. 
        ///     Gathers information from the database.
        /// </summary>
        /// <returns> A dynamic "New Teacher" webpage which asks the user for new information 
        /// as part of a form. </returns>
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
        /// <param name="TeacherFname"> First Name of teacher </param>
        /// <param name="TeacherLname"> Last Name of teacher </param>
        /// <param name="EmployeeNumber"> Teacher's employee number </param>
        /// <param name="HireDate"> Date teacher was hiered </param>
        /// <param name="Salary"> Amount of money teacher makes </param>
        /// <returns> A dynamic webpage which provides a new teacher's information. </returns>
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
        ///     Gathers information from the school database.
        /// </summary>
        /// <param name="id">Id of a Teacher</param>
        /// <returns>
        ///     A dynamic "Update Teacher" webpage which provides the current information of the 
        ///     teacher and asks the user for new information as part of a form.
        /// </returns>
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

        // POST: /Teacher/Update/{id}
        /// <summary>
        ///     Receives a POST request containing information about an existing teacher in the database, 
        ///     with new values. Conveys this information to the API, 
        ///     and redirects to the "Teacher Show" page of our updated author.
        /// </summary>
        /// <param name="id"> Id of a teacher </param>
        /// <param name="TeacherFname"> First Name of a teacher </param>
        /// <param name="TeacherLname"> Last name of a teacher </param>
        /// <param name="EmployeeNumber"> Teacher's employee number </param>
        /// <param name="Salary"> Amount of money a teacher makes </param>
        /// <returns> A dynamic webpage which provides the current information of the teacher. </returns>
        /// <example> 
        ///     POST: /Teacher/Update/6
        ///     {
        ///         "TeacherFname":"Thomas",
        ///         "TeacherLname":"Hawkins",
        ///         "EmployeeNumber":"T393",
        ///         "HireDate":"2016-10-08",
        ///         "Salary":"54.45"
        ///     }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, decimal Salary)
        {
            // New Teacher Object
            Teacher SelectedTeacher = new Teacher();
            SelectedTeacher.TeacherFname = TeacherFname;
            SelectedTeacher.TeacherLname = TeacherLname;
            SelectedTeacher.EmployeeNumber = EmployeeNumber;
            SelectedTeacher.Salary = Salary;

            // Instantiating
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, SelectedTeacher );

            return RedirectToAction("Show/" + id);
        }
    }
}