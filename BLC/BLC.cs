using DALC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLC
{
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }

        public string email { get; set; }

        public string imgUrl { get; set; }

        private DALC.DALC _DALC;

        private IConfiguration _config;

        public Student()
        { 
        }

        public Student(IConfiguration config)
        {
            this._config = config;
            this._DALC = new DALC.DALC(this._config);
            
        }


        public List<Student> GetAllStudents()
        {
            List<Student> oList = new List<Student>();
            List<Data_Student> oList_DS = _DALC.GetAllStudents();
            foreach (var ds in oList_DS)
            {
                var s = new Student(this._config);
                s.id = ds.id;
                s.name = ds.name;
                s.email = ds.email;
                s.imgUrl = ds.imgUrl;
                oList.Add(s);
            }
            oList = oList.OrderBy(x => x.name).ToList();
            //oList.RemoveAll(x => x.name.StartsWith("A"));
            return oList;
        }

        public void ReadStudentFromFile()
        {          
            var ds = _DALC.ReadStudentFromFile(this.id);
            this.id = ds.id;
            this.name = ds.name;
            this.email = ds.email;
            this.imgUrl = ds.imgUrl;
        }

        public void DeleteStudent(int id)
        {
            _DALC.DeleteStudent(id);
        }

        public void CreateStudent(Student s)
        {
            var ds = new Data_Student();
            ds.id = s.id;
            ds.name = s.name;
            ds.email = s.email;
            ds.imgUrl = s.imgUrl;
            _DALC.CreateStudent(ds);
        }

        public void UpdateStudent(Student s)
        {
            var ds = new Data_Student();
            ds.id = s.id;
            ds.name = s.name;
            ds.email = s.email;
            ds.imgUrl = s.imgUrl;
            _DALC.UpdateStudent(ds);
        }

    }
}
