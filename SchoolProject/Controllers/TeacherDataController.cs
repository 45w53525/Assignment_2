using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject.Models;
using MySql.Data.MySqlClient;

namespace SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
    
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers object (including 
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connect.
            MySqlConnection Conn = School.AccessDatabase();

         
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Teacher NewTeacher = new Teacher();
                //NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                //NewTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                //NewTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                //string TeacherName = ResultSet["teacherfname"] + " "+ ResultSet["teacherlname"];
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string Teachelname = (string)ResultSet["teacherlname"];
                string TeacherNumber = "";


                if (ResultSet["employeenumber"] != System.DBNull.Value)
{
                   TeacherNumber = (string)ResultSet["employeenumber"];
                }
                string TeacherEmail = (string)ResultSet["TeacherEmail"];


                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname=TeacherFname;
                newTeacher.TeacherLname = Teachelname;
               // newTeacher.TeacherNumber = TeacherNumber;//
                newTeacher.TeacherEmail = TeacherEmail;

                //Add the Teacher Name to the List
                //Teachers.Add(NewTeacher);
                Teachers.Add(newTeacher);
            }

            
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher newTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = "+id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string Teachelname = (string)ResultSet["teacherlname"];
                string TeacherNumber = "";

                
                if (ResultSet["employeenumber"] != System.DBNull.Value)
                {
                    TeacherNumber = (string)ResultSet["employeenumber"];
                }
                


                string TeacherEmail = (string)ResultSet["TeacherEmail"];



                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = Teachelname;
               // newTeacher.TeacherNumber = TeacherNumber;//
                newTeacher.TeacherEmail = TeacherEmail;
            }

                return newTeacher;
        }
        public void addTeacher(Teacher NewTeacher)
        {

            //Create an instance of a connect.
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();


          
            string query = "insert into Teachers (Teacherfname,Teacherlname,Teacheremail,hiredate)  values (@Teacherfname,@Teacherlname,@Teacheremail,CURRENT_DATE())";

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@Teacherfname",NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@Teacherlname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@Teacheremail", NewTeacher.TeacherEmail);
        
            cmd.Prepare();


            cmd.ExecuteNonQuery();

            Conn.Close();
       
        
        }
   
        public void Deleteteacher(int teacherid) 
        {


            //Create an instance of a connect.
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();



            string query = "delete from  teachers where teacherid=@id";

            cmd.Parameters.AddWithValue("@id", teacherid);

            cmd.CommandText = query;

            cmd.Prepare ();

            cmd.ExecuteNonQuery ();
            Conn.Close();

            /////////updates Teachers /////////////
            /////Teacherid= Primary key/////////

        } public void UpdateTeacher(int Teacherid, Teacher NewTeacher)
        {
            //Create an instance of a connect.
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@teacherfname,teacherlname=@teacherlname,teacheremail=@teacheremail WHERE teacherid=@teacherid";
            cmd.Parameters.AddWithValue("@Teacherfname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@Teacherlname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@Teacheremail", NewTeacher.TeacherEmail);

            cmd.Parameters.AddWithValue("@Teacherid", Teacherid);
            cmd.ExecuteNonQuery();

            Conn.Close();


        }




    }
}
