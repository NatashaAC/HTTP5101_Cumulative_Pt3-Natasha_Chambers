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
    public class ClassDataController : ApiController
    {
        // Context Class that allows access to the School Database 
        private SchoolDbContext School = new SchoolDbContext();

        // Objective: 
        //  Access the data stored in the classes table
        //  display a list of the classes by class name
        /// <summary>
        ///     Returns a list of the data within the classes table
        ///     in the school database
        /// </summary>
        /// <returns> A list of Classes (code, name, start date, fnish date)</returns>
        /// <example>
        ///     GET api/ClassData/ListClasses -> Class Object, Class Object, etc..
        /// </example>
        [HttpGet]
        [Route("api/ClassData/ListClasses/{SearchKey?}")]
        public IEnumerable<Class> ListClasses(string SearchKey = null)
        {
            // Instance of Connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM classes WHERE LOWER(classname) LIKE LOWER(@key)" +
                "OR LOWER(classcode) LIKE LOWER(@key)";

            // Parameters to protect Query from SQL Injection Attacks
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            // Variable thats stores the results from the SQL Querys
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Empty list of Classes
            List<Class> Classes = new List<Class> { };

            // Loop through Result set rows and add to classes lists
            while (ResultSet.Read())
            {
                // New Class Object
                Class NewClass = new Class();

                // Access the Columns in the Classes tab;es
                NewClass.ClassId = Convert.ToInt32(ResultSet["classid"]);
                NewClass.ClassCode = ResultSet["classcode"].ToString();
                NewClass.ClassName = ResultSet["classname"].ToString();
                NewClass.StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                NewClass.FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

                // Add the NewClass to empty Classes list
                Classes.Add(NewClass);
            }

            // Close Connection
            Conn.Close();

            // Return populated list 
            return Classes;
        }

        /// <summary>
        ///     Returns the information of a class based on the class id
        /// </summary>
        /// <param name="id">An interger</param>
        /// <returns>A Class</returns>
        /// <example> 
        ///     GET api/ClassData/FindClass/9 
        /// </example>
        /// <example>
        ///     GET api/ClassData/FindClass/3
        /// </example>
        [HttpGet]
        [Route("api/ClassData/FindClass/{id}")]
        public Class FindClass(int id)
        {
            // New Class Object
            Class NewClass = new Class();

            // Access School database
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection
            Conn.Open();

            // New query for School database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "SELECT * FROM classes WHERE classid = @id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            // Store Query results
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop through rows
            while (ResultSet.Read())
            {
                // Access columns in the Classes table
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                DateTime StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                DateTime FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
            }

            // Close connection
            Conn.Close();

            return NewClass;
        }

        /// <summary>
        ///     Deletes a Class and their information from the Classes table
        /// </summary>
        /// <param name="id">an integer, that corresponds to the classid</param>
        /// <return> Nothing </return>
        [HttpPost]
        public void DeleteClass(int id)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "DELETE FROM classes WHERE classid = @id";

            // Parameters to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }

        /// <summary>
        ///     Adds a Class to the classes table
        /// </summary>
        /// <param name="NewClass">A class object</param>
        /// <return> Nothing </return>
        [HttpPost]
        public void AddClass([FromBody] Class NewClass)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "INSERT INTO classes (classcode, startdate, finishdate, classname) " +
                "VALUES (@classcode, @startdate, @finishdate, @classname)";

            // Parameters for SQL Query to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("@classcode", NewClass.ClassCode);
            cmd.Parameters.AddWithValue("@startdate", NewClass.StartDate);
            cmd.Parameters.AddWithValue("@finishdate", NewClass.FinishDate);
            cmd.Parameters.AddWithValue("@classname", NewClass.ClassName);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close Connection
            Conn.Close();
        }

        public void UpdateClass(int id, [FromBody] Class ClassInfo)
        {
            // Instance of connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open connection between web server and database
            Conn.Open();

            // Create new query for database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query
            cmd.CommandText = "UPDATE classes SET classcode = @classcode, classname = @classname, " +
                "startdate = @startdate, finishdate = @finishdate WHERE classid = @classid";

            // Parameters for SQL Query to protect against SQL Injection Attacks
            cmd.Parameters.AddWithValue("@classcode", ClassInfo.ClassCode);
            cmd.Parameters.AddWithValue("@classname", ClassInfo.ClassName);
            cmd.Parameters.AddWithValue("@startdate", ClassInfo.StartDate);
            cmd.Parameters.AddWithValue("@finishdate", ClassInfo.FinishDate);
            cmd.Parameters.AddWithValue("@classid", id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            // Close Connection
            Conn.Close();
        }
    }
}
