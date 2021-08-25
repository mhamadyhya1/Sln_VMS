using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace DALC
{
    public class DALC
    {
        private string DataPath;

        private IConfiguration _config;
        public DALC(IConfiguration config)
        {
            this._config = config;
            this.DataPath = this._config.GetSection("MySettings").GetSection("DataPath").Value;
        }
        public Data_Student FillStudentFromFile(string path)
        {
            string FileContent = File.ReadAllText(path);
            string[] parts = FileContent.Split("|");
            var ds = new Data_Student();
            ds.id = Convert.ToInt32(parts[0]);
            ds.name = parts[1];
            ds.email = parts[2];
            ds.imgUrl = parts[3];

            return ds;
        }


        public string PrepareFilePath(int id)
        {
            return string.Format(@"{0}\{1}.{2}", DataPath, id, "txt");
        }

        public List<Data_Student> GetAllStudents()
        {
            var oList = new List<Data_Student>();
            string[] files = Directory.GetFiles(DataPath);
            foreach (var file in files)
            {
                var ds = FillStudentFromFile(file);
                oList.Add(ds);
            }            
            return oList;
        }

        public Data_Student ReadStudentFromFile(int id)
        {
            var FilePath = PrepareFilePath(id);
            var ds = FillStudentFromFile(FilePath);
            return ds;
        }

        public void DeleteStudent(int id)
        {
            var FilePath = PrepareFilePath(id);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

        public void CreateStudent(Data_Student ds)
        {
            var FilePath = PrepareFilePath(ds.id);
            string Content = string.Format("{0}|{1}|{2}|{3}",ds.id,ds.name,ds.email,ds.imgUrl);
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, Content);
            }
        }

        public void UpdateStudent(Data_Student ds)
        {
            var FilePath = PrepareFilePath(ds.id);
            string Content = string.Format("{0}|{1}|{2}|{3}", ds.id, ds.name, ds.email, ds.imgUrl);
            if (File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, Content);
            }
        }
    }

    public class Data_Student
    {
        public int id;
        public string name;
        public string email;
        public string imgUrl;
    }
}
