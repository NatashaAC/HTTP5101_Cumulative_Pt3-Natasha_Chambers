using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5101_Cumulative_Pt3_Natasha_Chambers.Models;

namespace HTTP5101_Cumulative_Pt3_Natasha_Chambers.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student/List
        public ActionResult List()
        {
            // Instantiating 
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents();

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
    }
}