using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3.Models;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();

        //This Controller Will access the teachers table of our blog database.
        /// <summary>
        /// Returns a details of teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// all details of teachers
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            // Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY to find teachers by firstname, lastname, hiredate or salary
            cmd.CommandText = "SELECT * FROM teachers WHERE lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat (teacherfname, ' ', teacherlname)) like lower(@key) or hiredate like (@key) or salary like (@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> {};

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                // Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmpoyeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                Decimal Salary = (Decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmpoyeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
                // Add the Teachers details to the List
                Teachers.Add(NewTeacher);
            }
        // Close the connection between the MySQL Database and the WebServer
        Conn.Close();

            // Return the final list of teacher's details
            return Teachers;
        }

        //This Controller Will find the teacher by their id.
        /// <summary>
        /// Returns a details of teachers according to the provided id
        /// </summary>
        /// <example>GET api/TeacherData/FindTeacher/{id}</example>
        /// <param name="id">Id of the teacher</param>
        /// <returns>
        /// all details of teachers searched for
        /// </returns>


        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            // Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "SELECT * FROM teachers where teacherid = "+id;

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                // Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmpoyeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                Decimal Salary = (Decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmpoyeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }
                // Return the final list of teacher's details
                return NewTeacher;
        }

        /// <summary>
        /// It will delete the teacher from the database.
        /// </summary>
        /// <param name="id">The ID of a teacher.</param>
        /// <example>POST : /api/TeacherData/DeleteTeacher/1</example>

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            // Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY to find teachers by firstname, lastname, hiredate or salary
            cmd.CommandText = "DELETE FROM teachers where teacherid=@id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// It will add a new teacher to the Database.
        /// </summary>
        /// <param name="NewTeacher">An object of Teacher.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// POST DATA
        /// {
        ///	"TeacherFname":"Bhavya",
        ///	"TeacherLname":"Patel",
        ///	"EmployeeNumber":"T101",
        ///	"HireDate":"2024-01-01"
        ///	"Salary": 70.50
        /// }
        /// </example>
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            // Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY to find teachers by firstname, lastname, hiredate or salary
            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES (@TeacherFname,@TeacherLname,@EmployeeNumber,@HireDate,@Salary)";

            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        /// <summary>
        /// Updates the Teacher info in the database
        /// </summary>
        /// <param name="id">Id of the teacher</param>
        /// <param name="TeacherInfo">An object containing all the columns of the table</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/1
        /// FORM DATA / POST DATA / REQUEST DATA
        /// {
        /// "TeacherFname":"Alexander",
        /// "TeacherLname":"Bennet",
        /// "EmployeeNumber":"T373",
        /// "HireDate":"2016-08-05",
        /// "Salary":"55.35"
        /// }
        /// </example>


        // Show evidence of using a CURL request with a JSON object to update the teacher datathrough the WebAPI instead of the teacher interface.
        // info.json
        // {
        //      "TeacherFname": "Alexander",
        //      "TeacherLname": "Bennett",
        //      "EmployeeNumber": "T379",
        //      "HireDate": "2016-08-05",
        //      "Salary": "55.35"
        // }

        // Command prompt:
        // C:\Users\rdpat\OneDrive\Desktop\Back-end\Assignment3\Assignment3>curl -H "Content-Type:application/json" -d @info.json "http://localhost:54560/api/TeacherData/UpdateTeacher/1"

        // C:\Users\rdpat\OneDrive\Desktop\Back-end\Assignment3\Assignment3>

        // Output on browser:
        //FindTeacher
        // Home
        // Help
        // Back to Teachers DeleteUpdate
        // Alexander Bennett
        // T379

        // 2016-08-05 12:00:00 AM

        // 55.35

        // © 2024 - My ASP.NET Application

        // mysql database table
        // 1
        // Alexander
        // Bennett
        // T379
        // 2016-08-05 00:00:00
        // 55.35

    public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            // Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY to find teachers by firstname, lastname, hiredate or salary
            cmd.CommandText = "UPDATE teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@HireDate, salary=@Salary where teacherid=@TeacherId";

            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }
}
