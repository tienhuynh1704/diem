using System;
using System.Collections.Generic;
using QuanLyDiemHocSinh.Data;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.Services
{
    public class ClassService
    {
        private readonly string studentFilePath;
        private List<Student> students;

        public ClassService(string studentFilePath)
        {
            this.studentFilePath = studentFilePath;
            students = FileHandler.LoadDataFromFile(studentFilePath);
        }

        public List<Student> GetStudentsByClass(string className)
        {
            students = FileHandler.LoadDataFromFile(studentFilePath);

            List<Student> result = new List<Student>();
            int i;
            for (i = 0; i < students.Count; i = i + 1)
            {
                if (!string.IsNullOrEmpty(students[i].Class) &&
                    students[i].Class.Equals(className, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(students[i]);
                }
            }
            return result;
        }

        public List<string> GetAllClassNames()
        {
            students = FileHandler.LoadDataFromFile(studentFilePath);

            List<string> classes = new List<string>();
            int i, j;
            for (i = 0; i < students.Count; i = i + 1)
            {
                string cls = students[i].Class;
                bool found = false;

                for (j = 0; j < classes.Count; j = j + 1)
                {
                    if (classes[j].Equals(cls, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found && string.IsNullOrEmpty(cls) == false)
                {
                    classes.Add(cls);
                }
            }
            return classes;
        }
    }
}
