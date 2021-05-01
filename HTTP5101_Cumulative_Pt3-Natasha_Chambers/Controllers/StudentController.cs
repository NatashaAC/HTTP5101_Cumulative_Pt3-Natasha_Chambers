using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using HTTP5101_Cumulative_Pt3_Natasha_Chambers.Models;

namespace HTTP5101_Cumulative_Pt3_Natasha_Chambers.Controllers
{
    public class StudentController : Controller
    {
        // GET: /Student/List
        /// <summary>
        ///     Routes to a dynamically generated "Student List" Page. 
        ///     Gathers information about all the students in the database.
        /// </summary>
        /// <param name="SearchKey"> A string that represents a student's first name, last name or both </param>
        /// <returns> 
        ///     A dynamic webpage which displays a list of students 
        /// </returns>
        /// <example>
        ///     /Student/Lisst
        /// </example>
        public ActionResult List(string SearchKey = null)
        {
            // Instantiating 
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);

            return View(Students);
        }

        // GET: /Student/Show/{id}
        /// <summary>
        ///     Routes to a dynamically generated "Student Show" Page. 
        ///     Gathers information about a specific student from the database
        /// </summary>
        /// <param name="id"> An interger that represents the Id of a Student </param>
        /// <returns> 
        ///     A dynamic webpage which provides the current information of a specific student 
        /// </returns>
        /// <example>
        ///     /Student/Show/3
        /// </example>
        public ActionResult Show(int id)
        {
            // Checking that the method is running
            Debug.WriteLine("The SHOW Method is running and the student_id is " + id);

            // Instantiating 
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }

        // GET: /Student/DeleteConfirmation/{id}
        /// <summary>
        ///     Routes to a dynamically generated "Student DeleteConfirmation" Page. 
        ///     Gathers information about a specific student that will be deleted from the database
        /// </summary>
        /// <param name="id"> An interger that represents the Id of a student </param>
        /// <returns> 
        ///     A dynamic webpage which provides the current information of the student 
        /// </returns>
        /// <example>
        ///     /Student/DeleteConfirmation/11
        /// </example>
        [HttpGet]
        public ActionResult DeleteConfirmation(int id)
        {
            // Checking that the method is running
            Debug.WriteLine("The DELETE CONFIRMATION Method is running and is going to delete STUDENT ID " + id);

            // Instantiating 
            StudentDataController controller = new StudentDataController();
            Student NewClass = controller.FindStudent(id);

            return View(NewClass);
        }

        // POST: /Student/Delete/{id}
        /// <summary>
        ///    Receives a POST request containing information about an existing student in the database, 
        ///    Conveys this information to the DeleteStudent API, inorder
        ///    to remove the specific student from the database.
        ///    Redirects to the "Student List" page.
        /// </summary>
        /// <param name="id"> Id of a student </param>
        /// <returns>  
        ///     A dynamic webpage which provides a list of students 
        /// </returns>
        /// <example>
        ///     /Student/Delete/12
        /// </example>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Checking that the method is running
            Debug.WriteLine("The DELETE Method is running and has deleted student_id " + id);

            // Instantiating 
            StudentDataController controller = new StudentDataController();
            controller.DeleteStudent(id);

            return RedirectToAction("List");
        }

        // GET: /Student/Add
        /// <summary>
        ///     Routes to a dynamically generated "Student Add" Page. 
        ///     Gathers information about a new student from a form 
        ///     that will be added to the database.
        /// </summary>
        /// <returns> 
        ///     A dynamic webpage which asks the user for new information regarding a student as part of a form
        /// </returns>
        /// <example>
        ///     /Student/Add
        /// </example>
        public ActionResult Add()
        {
            return View();
        }

        // POST: /Student/Create
        /// <summary>
        ///    Receives a POST request containing information about a new student, 
        ///    Conveys this information to the AddStudent API, inorder
        ///    to add the specific student to the database.
        ///    Redirects to the "Student List" page.
        /// </summary>
        /// <param name="StudentFname"> A string representing a Student's first name </param>
        /// <param name="StudentLname"> A string representing a Student's last name </param>
        /// <param name="StudentNumber"> A string representing a Student's number </param>
        /// <param name="EnrollDate"> A date representing a Student's enroll date </param>
        /// <returns> 
        ///     A dynamic webpage which provides the current information of the teacher 
        /// </returns>
        /// <example>
        ///     /Student/Create
        ///     {
        ///         "StudentFname":"Beth",
        ///         "StudentLname":"Waltz",
        ///         "StudentNumber":"N0107",
        ///         "EnrollDate":"2018-10-08",
        ///     }
        /// </example>
        [HttpPost]
        public ActionResult Create(string StudentFname, string StudentLname, string StudentNumber, DateTime EnrollDate)
        {
            // Checking that the method is running
            Debug.WriteLine("The CREATE Method is running!");

            // Checking that the inputs from the form has been received 
            Debug.WriteLine("Student Number: " + StudentNumber + ", First name: " + StudentFname + ", Last name: " + StudentLname);
            Debug.WriteLine("Enroll Date: " + EnrollDate);

            // Validating 
            if (StudentFname == "" || StudentLname == "" || StudentNumber == "")
            {
                return RedirectToAction("New");
            }
            else
            {
                // New Student Object
                Student NewStudent = new Student();
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrollDate = EnrollDate;

                // Instantiating 
                StudentDataController controller = new StudentDataController();
                controller.AddStudent(NewStudent);

                return RedirectToAction("List");
            }
        }

        // GET: /Student/Update/{id}
        /// <summary>
        ///     Routes to a dynamically generated "Student Update" Page.
        ///     That asks the user for new information as part of a form.
        ///     Gathers information from the school database.
        /// </summary>
        /// <param name="id"> An interger that represents the Id of a Student </param>
        /// <returns> 
        ///     A dynamic webpage which provides the current information of a student 
        /// </returns>
        /// <example>
        ///     /Student/Update/4
        /// </example>
        [HttpGet]
        public ActionResult Update(int id)
        {
            // Checking that the method is running
            Debug.WriteLine("The UPDATE Method is running and has updated student_id " + id);

            // Instantiating 
            StudentDataController controller = new StudentDataController();
            Student SelectedStudent = controller.FindStudent(id);

            return View(SelectedStudent);
        }

        // POST: /Student/Update/{id}
        /// <summary>
        ///    Receives a POST request containing information about an existing student, 
        ///    Conveys this information to the UpdateStudent API, inorder
        ///    to update the specific student information in the database.
        ///    Redirects to the "Student Show" page.
        /// </summary>
        /// <param name="id"> An interger that represents the Id of a Student </param>
        /// <param name="StudentFname"> A string that represents the First Name of a student </param>
        /// <param name="StudentLname"> A string that represents the Last Name of a student </param>
        /// <param name="StudentNumber"> A string that represents the Student Number of a student </param>
        /// <returns> 
        ///     A dynamic webpage which provides the current information of a student 
        /// </returns>
        /// <example>
        ///     /Student/Update/4
        ///     {
        ///         "StudentFname":"Mario",
        ///         "StudentLname":"English",
        ///         "StudentNumber":"N1686"
        ///     }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string StudentFname, string StudentLname, string StudentNumber)
        {
            // Checking that the method is running
            Debug.WriteLine("The UPDATE Method is running and has updated student_id " + id);

            // Checking that the inputs from the form has been received 
            Debug.WriteLine("Student Number: " + StudentNumber + ", First name: " + StudentFname + ", Last name: " + StudentLname);
           
            // New Student Object
            Student SelectedStudent = new Student();
            SelectedStudent.StudentFname = StudentFname;
            SelectedStudent.StudentLname = StudentLname;
            SelectedStudent.StudentNumber = StudentNumber;

            // Instantiating
            StudentDataController controller = new StudentDataController();
            controller.UpdateStudent(id, SelectedStudent);

            return RedirectToAction("Show/" + id);
        }
    }
}