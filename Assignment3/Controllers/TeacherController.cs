using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;
using System.Diagnostics;

namespace Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        //GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns a details of Teachers who have been searched.
        /// </summary>
        /// <param name="SearchKey">To search a particular teacher</param>
        /// <example>GET /Teacher/List/{SearchKey}(optional)</example>
        /// <returns>
        /// Names for searched teachers.
        /// </returns>
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        /// <summary>
        /// Returns a details of Teachers.
        /// </summary>
        /// /// <param name="id">To search a particular teacher</param>
        /// <example>GET /Teacher/Show/{id)</example>
        /// List of teachers.
        /// </returns>
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //GET : /Teacher/DeleteConfirm/{id}
        /// <summary>
        /// It will Display a confirmation page to delete a teacher.
        /// </summary>
        /// <param name="id">The ID of a teacher</param>
        /// <returns>It will return a view of the confirmation page of deleting the teacher.</returns>
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }

        //POST: /Teacher/Delete/{id}
        /// <summary>
        /// It will Delete a teacher from the database.
        /// </summary>
        /// <param name="id">The ID of a teacher</param>
        /// <returns>It will delete the teacher and will navigate to the list page.</returns>
        
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : localhost:xx/Teacher/New -> Show a new teacher
        /// <summary>
        /// It will display the form to add a new teacher.
        /// </summary>
        /// <returns>It will return a view containing the form</returns>

        public ActionResult New()
        {
            return View();
        }

        //POST : localhost:xx/Teacher/Create
        /// <summary>
        /// It will add a new teacher in the database.
        /// </summary>
        /// <param name="TeacherFname">First name of the teacher.</param>
        /// <param name="TeacherLname">Last name of the teacher.</param>
        /// <param name="EmployeeNumber">Employee number of the teacher.</param>
        /// <param name="HireDate">Hire date of the teacher.</param>
        /// <param name="Salary">Salary of the teacher.</param>
        /// <returns>
        /// It will add a new teacher and will navigate to the list page.
        /// </returns>
        /// /// <example>
        /// Example of POST request body
        /// POST /Teacher/Create
        /// {
        ///     "TeacherFname": "Bhavya",
        ///     "TeacherLname": "Patel",
        ///     "EmployeeNumber": "T101",
        ///     "HireDate": "2024-01-01",
        ///     "Salary": 70.50
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime? HireDate, Decimal? Salary)
        {

            //Missing information validations

            if (String.IsNullOrEmpty(TeacherFname) || String.IsNullOrEmpty(TeacherLname) || String.IsNullOrEmpty(EmployeeNumber) || HireDate == null || Salary == null)
            {
                ViewBag.Msg = "Missing Information!!!";
                return View("New");
            }


            //debugging message
            //Confirmimg that we received the new teacher's info
            Debug.WriteLine("Teacher Create Method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname =  TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = (DateTime)HireDate;
            NewTeacher.Salary = Salary ?? 0;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            //redirects to List.chtml
            return RedirectToAction("List");
        }
    }
}