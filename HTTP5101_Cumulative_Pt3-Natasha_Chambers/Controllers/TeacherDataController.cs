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
    public class TeacherDataController : ApiController
    {
        // Context Class that allows access to the School Database
        private SchoolDbContext School = new SchoolDbContext();

        // Objective: 
        //    Access the data stored in the teachers table and 
        //    display a list of the teachers by first and last name.
        //    Can search for a teacher by typing in their first name, last name,
        //    or both
        /// <summary>
        ///     Returns a list of the data within the teachers table
        ///     in the school database
        /// </summary>
        /// <returns> A list of Teacher objects (first name, last name, employee number, hire date, salary)</returns>
        /// <example>
        ///     GET api/TeacherData/ListTeachers -> Teacher Object, Teacher Object, etc..
        /// </example>
        /// <example>
        ///     GET api/TeacherData/ListTeachers/Cody -> Cody Holland's Information
        /// </example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            // Instance of Connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM teachers WHERE LOWER(teacherfname) LIKE LOWER(@key) " +
                "OR LOWER(teacherlname) LIKE LOWER(@key) " +
                "OR LOWER(CONCAT(teacherfname, ' ', teacherlname)) LIKE LOWER(@key)";

            // Parameters to protect Query from SQL Injection Attacks
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            // Variable that stores the results from the SQL Query
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            // Loop through Result Set rows and add to teachers list
            while (ResultSet.Read())
            {
                // New Teacher Object
                Teacher NewTeacher = new Teacher();

                // Access the Columns in the Teachers table
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                NewTeacher.Salary = Convert.ToDecimal(ResultSet["salary"]);

                // Add the NewTeacher to empty list
                Teachers.Add(NewTeacher);
            }

            // Close connection
            Conn.Close();

            // Return populated list
            return Teachers;
        }

        // Objective:
        //  
        /// <summary>
        ///     Returns the information of a teacher based on the teacher id 
        /// </summary>
        /// <param name="id"> An integer, that corresponds to the teacherid </param>
        /// <returns> A teacher object </returns>
        /// <example> 
        ///     GET api/TeacherData/FindTeacher/5 -> 
        ///     {
        ///         "TeacherFname":"Jessica",
        ///         "TeacherLname":"Morris",
        ///         "EmployeeNumber":"T389",
        ///         "HireDate":"2012-06-04T00:00:00",
        ///         "Salary":"48.62"
        ///     }
        /// </example>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            // New Teacher Object
            Teacher NewTeacher = new Teacher();

            // Access School database
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection
            Conn.Open();

            // New query for School database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM Teachers WHERE teacherid = @teacher_id";

            // Parameters for SQL Query to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("@teacher_id", id);
            cmd.Prepare();

            // Store Query results
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop through rows
            while (ResultSet.Read())
            {
                // Access Columns in Teachers Table
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            // Close Connection
            Conn.Close();

            return NewTeacher;
        }

        /// <summary>
        ///     Deletes a Teacher and their information from the teachers table
        /// </summary>
        /// <param name="id"> An integer, that corresponds to the teacherid </param>
        /// <returns> Nothing </returns>
        /// <example>
        ///     api/TeacherData/DeleteTeacher/5
        /// </example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "DELETE FROM teachers WHERE teacherid = @teacher_id";

            // Parameters to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("@teacher_id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }

        /// <summary>
        ///     Adds a Teacher to the teachers table
        /// </summary>
        /// <param name="NewTeacher"> A teacher object </param>
        /// <returns> Nothing </returns>
        /// <example>
        ///     
        /// </example>
        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) " +
                "VALUES (@teacherfname, @teacherlname, @employeenumber, @hiredate, @salary)";

            // Parameters for SQL Query to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.Salary);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close Connection
            Conn.Close();
        }

        /// <summary>
        ///     Updates the information of a specific Teacher
        /// </summary>
        /// <param name="id"> An integer, that corresponds to the teacherid </param>
        /// <param name="TeacherInfo"> A teacher object </param>
        /// <returns> Nothing </returns>
        /// <example>
        ///     api/TeacherData/UpdateTeacher/
        /// </example>
        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "UPDATE teachers SET teacherfname = @teacherfname, teacherlname = @teacherlname, " +
                "employeenumber = @employeenumber, salary = @salary WHERE teacherid = @teacher_id";

            // Parameters to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("@teacherfname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@teacher_id", id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close Connection
            Conn.Close();
        }
    }
}
