using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using HTTP5101_Cumulative_Pt3_Natasha_Chambers.Models;

namespace HTTP5101_Cumulative_Pt3_Natasha_Chambers.Controllers
{
    public class StudentDataController : ApiController
    {
        // Context Class that allows access to the School Database 
        private SchoolDbContext School = new SchoolDbContext();

        // Objective:
        //  Access the data stored in the students table
        //  display a list of the students by first and last name
        /// <summary>
        ///     Returns list of students in the students table
        ///     within the school database
        /// </summary>
        /// <returns> List of students (name, student number, enroll date)</returns>
        /// <example>
        ///     GET api/StudentData/ListStudents -> Student Object, Student Object, etc...
        /// </example>
        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey?}")]
        public IEnumerable<Student> ListStudents(string SearchKey = null)
        {
            // Instance of Connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection
            Conn.Open();

            // Create new Query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM students WHERE LOWER(studentfname) LIKE LOWER(@key) " +
                "OR LOWER(studentlname) LIKE LOWER(@key) " +
                "OR LOWER(CONCAT(studentfname, ' ', studentlname)) LIKE LOWER(@key)";

            // Parameters to protect Query from SQL Injection Attacks
            cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");
            cmd.Prepare();

            // Store results from Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Empty List of Students
            List<Student> Students = new List<Student> { };

            // Loop through Result Set rows
            while (ResultSet.Read())
            {
                // New Student Object
                Student NewStudent = new Student();

                // Access the Columns in the Students table
                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFname = ResultSet["studentfname"].ToString();
                NewStudent.StudentLname = ResultSet["studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.EnrollDate = Convert.ToDateTime(ResultSet["enroldate"]);

                // Add students to empty list
                Students.Add(NewStudent);
            }

            // Close connection
            Conn.Close();

            return Students;
        }

        /// <summary>
        ///     Returns the information of a student based on the student id
        /// </summary>
        /// <param name="id">An interger</param>
        /// <returns> A student</returns>
        /// <example>
        ///     GET api/StudentData/FindStudent/5
        /// </example>
        /// <example>
        ///     GET api/StudentData/FindStudent/22
        /// </example>
        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            // New Student Object
            Student NewStudent = new Student();

            // Access School database
            MySqlConnection Conn = School.AccessDatabase();

            // Open Connection
            Conn.Open();

            // New query for School database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM students WHERE studentid = @student_id";

            // Parameters to protect SQL Query from SQL Injection Attacks
            cmd.Parameters.AddWithValue("student_id", id);
            cmd.Prepare();

            // Store Query results
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop through rows
            while (ResultSet.Read())
            {
                // Access columns in the Students table
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrollDate = Convert.ToDateTime(ResultSet["enroldate"]);

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrollDate = EnrollDate;
            }

            // Close connection
            Conn.Close();

            return NewStudent;
        }

        /// <summary>
        ///     Deletes a Student and their information from the students table
        /// </summary>
        /// <param name="id">an integer, that corresponds to the studentid</param>
        /// <return> Nothing </return>
        [HttpPost]
        public void DeleteStudent(int id)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "DELETE FROM students WHERE studentid = @student_id";

            // Parameters to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("student_id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }

        /// <summary>
        ///     Adds a Student to the students table
        /// </summary>
        /// <param name="NewStudent">A student object</param>
        /// <return> Nothing </return>
        [HttpPost]
        public void AddStudent([FromBody] Student NewStudent)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "INSERT INTO students (studentfname, studentlname, studentnumber, enroldate) " +
                "VALUES (@studentfname, @studentlname, @studentnumber, @enroldate)";

            // Parameters for SQL Query to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("studentfname", NewStudent.StudentFname);
            cmd.Parameters.AddWithValue("studentlname", NewStudent.StudentLname);
            cmd.Parameters.AddWithValue("studentnumber", NewStudent.StudentNumber);
            cmd.Parameters.AddWithValue("enroldate", NewStudent.EnrollDate);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close Connection
            Conn.Close();
        }

        public void UpdateStudent(int id, [FromBody]Student StudentInfo)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "UPDATE students SET studentfname = @studentfname, studentlname = @studentlname, studentnumber = @studentnumber WHERE studentid = @student_id";

            // Parameters for SQL Query to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("@studentfname", StudentInfo.StudentFname);
            cmd.Parameters.AddWithValue("@studentlname", StudentInfo.StudentLname);
            cmd.Parameters.AddWithValue("@studentnumber", StudentInfo.StudentNumber);
            cmd.Parameters.AddWithValue("@student_id", id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close Connection
            Conn.Close();
        }
    }
}
