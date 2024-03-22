using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;

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

    }
}