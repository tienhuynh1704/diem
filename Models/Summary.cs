using System;
using System.Collections.Generic;

namespace QuanLyDiemHocSinh.Models
{
    public class Summary
    {
        private static List<Student> students;

        public static void SummarizeBySubject(string subject)
        {
            Console.WriteLine($"Tổng kết điểm cho môn {subject}:");
            foreach (Student student in students)
            {
                Score foundScore = null;
                foreach (Score score in student.Scores)
                {
                    if (score.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase))
                    {
                        foundScore = score;
                        break;
                    }
                }

                if (foundScore != null)
                {
                    Console.WriteLine(student.Name + ": " + foundScore.CalculateFinalScore().ToString("F2"));
                }
            }
        }

        public static void SummarizeByClass()
        {
            Console.WriteLine("Danh sách học sinh:");
            foreach (Student student in students)
            {
                student.DisplayInfo();
                Console.WriteLine("Điểm:");
                foreach (Score score in student.Scores)
                {
                    score.DisplayScore();
                }
                Console.WriteLine();
            }
        }

        public static void SetStudents(List<Student> studentsList)
        {
            students = studentsList;
        }
    }
}
