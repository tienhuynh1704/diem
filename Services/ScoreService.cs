using System;
using System.Collections.Generic;
using QuanLyDiemHocSinh.Data;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.Services
{
    public class ScoreService
    {
        private readonly string studentFilePath;
        private List<Student> students;

        public ScoreService(string studentFilePath)
        {
            this.studentFilePath = studentFilePath;
            students = FileHandler.LoadDataFromFile(studentFilePath);
        }

        public void AddOrUpdateScore(string teacherSubject = "")
        {
            Console.WriteLine("Nhập mã học sinh:");
            string studentId = Console.ReadLine();

            Student student = null;

            // ✅ Tìm thủ công, không dùng LINQ / Lambda
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                {
                    student = students[i];
                    break;
                }
            }

            if (student != null)
            {
                Console.WriteLine("Nhập môn học:");
                string subject = Console.ReadLine();

                if (!string.IsNullOrEmpty(teacherSubject) &&
                    !subject.Equals(teacherSubject, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Bạn không được phép sửa điểm cho môn học này.");
                    return;
                }

                Console.WriteLine("Nhập điểm giữa kỳ:");
                double midtermScore = double.Parse(Console.ReadLine());
                Console.WriteLine("Nhập điểm cuối kỳ:");
                double finalScore = double.Parse(Console.ReadLine());

                if (student.ScoreExists(subject))
                {
                    student.UpdateScore(subject, midtermScore, finalScore);
                    Console.WriteLine("Điểm môn học đã được cập nhật.");
                }
                else
                {
                    Score newScore = new Score(subject, midtermScore, finalScore);
                    student.AddScore(newScore);
                    Console.WriteLine("Điểm môn học đã được thêm.");
                }

                FileHandler.SaveDataToFile(studentFilePath, students);
            }
            else
            {
                Console.WriteLine("Không tìm thấy học sinh.");
            }
        }
    }
}
