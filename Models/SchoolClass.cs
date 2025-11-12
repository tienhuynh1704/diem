using System;
using System.Collections.Generic;

namespace QuanLyDiemHocSinh.Models
{
    [Serializable]
    public class SchoolClass
    {
        public string ClassName { get; set; }
        public List<Student> Students { get; set; }

        public SchoolClass()
        {
            Students = new List<Student>();
        }

        public SchoolClass(string className)
        {
            ClassName = className;
            Students = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(string studentId)
        {
            Student studentToRemove = null;
            foreach (Student s in Students)
            {
                if (s.StudentId == studentId)
                {
                    studentToRemove = s;
                    break;
                }
            }

            if (studentToRemove != null)
            {
                Students.Remove(studentToRemove);
            }
        }

        public void DisplayStudents()
        {
            Console.WriteLine("Danh sách học sinh của lớp " + ClassName + ":");
            foreach (Student s in Students)
            {
                s.DisplayInfo();
            }
        }
    }
}
