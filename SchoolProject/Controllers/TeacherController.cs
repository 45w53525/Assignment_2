using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        // GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers =  controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : Teacher/Show/{id}

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);

            return View(newTeacher);
        }


        public ActionResult Deleteconfirm(int id)
        {
            TeacherDataController controller =new TeacherDataController();
            Teacher newTeacher = controller.FindTeacher(id);
           
            return View(newTeacher);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            controller.Deleteteacher(id);
            return RedirectToAction("list");
        }




        public ActionResult New() { return View(); }

        [HttpPost]
        public ActionResult create(string Teacherfname, string Teacherlname, string Teacheremail)

        {

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = Teacherfname;
            NewTeacher.TeacherLname = Teacherlname;
            NewTeacher.TeacherEmail = Teacheremail;

            TeacherDataController controller = new TeacherDataController();
            controller.addTeacher(NewTeacher);


            return RedirectToAction("list");
        }  
            ////////////Get /Teacher/Edit/{id}//////

            public ActionResult edit(int id)
            {


            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.FindTeacher(id);

            

            return View(selectedTeacher);
            }

        ////////post /Teacher/update/{id}
        ///
        [HttpPost]

        public ActionResult Update(int id,string Teacherfname, string Teacherlname, string Teacheremail)

        {
         

          Teacher Teacherinfo = new Teacher();

            Teacherinfo.TeacherFname = Teacherfname;
            Teacherinfo.TeacherLname=Teacherlname;
            Teacherinfo.TeacherEmail=Teacheremail;

            //////update Teacher Informaton//////////

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, Teacherinfo);


            return RedirectToAction("Show/"+ id);
        }


        

    }
   
}