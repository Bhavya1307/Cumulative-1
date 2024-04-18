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
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
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

        //GET : /Teacher/Update/{id}
        /// <summary>
        /// Routes to the update page and shows the current info.
        /// </summary>
        /// <param name="id">Id of the teacher</param>
        /// <returns>Update page containing all the current details of a teacher and asks user to add new details if they want to.</returns>
        /// <example>GET : /Teacher/Update/{id}</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        //POST : /Teacher/Upadate/{id}
        /// <summary>
        /// Recieves a POST request containing info about a teacher with newly entered values. And redirects it to the show page.
        /// </summary>
        /// <param name="id">Id of a teavher to update</param>
        /// <param name="TeacherFname">Updated First name</param>
        /// <param name="TeacherLname">Updated Last name</param>
        /// <param name="EmployeeNumber">Updated Employee number</param>
        /// <param name="HireDate">Updated Hire date</param>
        /// <param name="Salary">Updated Salary</param>
        /// <returns>Updated details about the teacher</returns>
        /// <example>POST : /Teacher/Update/1</example>
        /// FORM DATA / POST DATA / REQUEST DATA
        /// {
        /// "TeacherFname":"Alexander",
        /// "TeacherLname":"Bennett",
        /// "EmployeeNumber":"T378",
        /// "HireDate":"2016-08-05",
        /// "Salary":"55.30"
        /// }
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime? HireDate, Decimal? Salary)
        {
            if (String.IsNullOrEmpty(TeacherFname) || String.IsNullOrEmpty(TeacherLname) || String.IsNullOrEmpty(EmployeeNumber) || HireDate == null || Salary == null)
            {
                ViewBag.Msg = "Missing Information!!!";
                return View("New");
            }

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.HireDate = (DateTime)HireDate;
            TeacherInfo.Salary = Salary ?? 0;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);
            return RedirectToAction("Show/" + id);
        }
    }
}