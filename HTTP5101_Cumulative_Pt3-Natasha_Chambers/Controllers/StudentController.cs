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
        // GET: Student/List
        public ActionResult List(string SearchKey = null)
        {
            // Instantiating 
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);

            return View(Students);
        }

        // GET: Student/Show
        public ActionResult Show(int id)
        {
            // Instantiating 
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }

        // GET: /StudentS/DeleteConfirmation/{id}
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

        // POST: /StudentsS/Delete/{id}
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

        // GET: /Student/New
        public ActionResult New()
        {
            return View();
        }

        // POST: /Student/Create
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
    }
}