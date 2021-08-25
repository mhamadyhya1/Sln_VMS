using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using BLC;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IConfiguration _config;
        public DataController(IConfiguration config)
        {
            this._config = config;
        }

        [Route("GetSingleStudent")]
        [HttpPost]
        public Student GetSingleStudent(Params_GetSingleStudent i_Params_GetSingleStudent)
        {
            var s = new Student(this._config);
            s.id = i_Params_GetSingleStudent.id;
            s.ReadStudentFromFile();
            return s;
        }

        [Route("GetAllStudents")]
        public List<Student> GetAllStudents()
        {
            var s = new Student(this._config);
            return s.GetAllStudents();
        }

        [Route("DeleteStudent")]
        [HttpPost]
        public void DeleteStudent(Params_DeleteStudent i_Params_DeleteStudent)
        {
            var s = new Student(this._config);
            s.DeleteStudent(i_Params_DeleteStudent.id);
        }

        [Route("CreateStudent")]
        [HttpPost]
        public void CreateStudent(Student toCreate)
        {
            var s = new Student(this._config);
            s.id = toCreate.id;
            s.name = toCreate.name;
            s.email = toCreate.email;
            s.imgUrl = toCreate.imgUrl;
            s.CreateStudent(s);
        }


        [Route("UpdateStudent")]
        [HttpPost]
        public void UpdateStudent(Student toCreate)
        {
            var s = new Student(this._config);
            s.id = toCreate.id;
            s.name = toCreate.name;
            s.email = toCreate.email;
            s.imgUrl = toCreate.imgUrl;
            s.UpdateStudent(s);
        }
    }

    public class Params_GetSingleStudent
    {
        public int id { get; set; }

    }

    public class Params_DeleteStudent
    {
        public int id { get; set; }
    }



}
