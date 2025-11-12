using System;
using System.Collections.Generic;
using QuanLyDiemHocSinh.Data;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.Services
{
    public class StudentService
    {
        private readonly string studentFilePath;
        private List<Student> students;

        public StudentService(string studentFilePath)
        {
            this.studentFilePath = studentFilePath;
            students = FileHandler.LoadDataFromFile(studentFilePath);
        }

        public void AddStudent()
        {
            Console.WriteLine("Nhập tên học sinh:");
            string name = Console.ReadLine();
            Console.WriteLine("Nhập tuổi:");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhập địa chỉ:");
            string address = Console.ReadLine();
            Console.WriteLine("Nhập mã học sinh:");
            string studentId = Console.ReadLine();
            Console.WriteLine("Nhập lớp:");
            string className = Console.ReadLine();

            Student student = new Student(name, age, address, studentId, className);
            students.Add(student);
            FileHandler.SaveDataToFile(studentFilePath, students);
            Console.WriteLine("Thêm học sinh thành công.");
        }

        public void UpdateStudentInfo()
        {
            Console.WriteLine("Nhập mã học sinh:");
            string studentId = Console.ReadLine();
            Student student = FindStudent(studentId);

            if (student != null)
            {
                Console.WriteLine("Nhập tên mới:");
                student.Name = Console.ReadLine();
                Console.WriteLine("Nhập tuổi mới:");
                student.Age = int.Parse(Console.ReadLine());
                Console.WriteLine("Nhập địa chỉ mới:");
                student.Address = Console.ReadLine();
                Console.WriteLine("Nhập lớp mới:");
                student.Class = Console.ReadLine();

                FileHandler.SaveDataToFile(studentFilePath, students);
                Console.WriteLine("Cập nhật thông tin học sinh thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy học sinh.");
            }
        }

        public Student FindStudent(string studentId)
        {
            students = FileHandler.LoadDataFromFile(studentFilePath);
            foreach (Student s in students)
            {
                if (s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                {
                    return s;
                }
            }
            return null;
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }
    }
}
