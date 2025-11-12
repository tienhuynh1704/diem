using System;
using System.Collections.Generic;

namespace QuanLyDiemHocSinh.Models
{
    [Serializable]
    public class Student : Person
    {
        public string StudentId { get; set; }
        public string Class { get; set; }
        public List<Score> Scores { get; set; }

        public Student()
        {
            Scores = new List<Score>();
        }

        public Student(string name, int age, string address, string studentId, string className)
            : base(name, age, address)
        {
            StudentId = studentId;
            Class = className;
            Scores = new List<Score>();
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Mã học sinh: " + StudentId + ", Lớp: " + Class);
        }

        public double CalculateAverageScore()
        {
            double total = 0;
            foreach (Score score in Scores)
            {
                total += score.CalculateFinalScore();
            }
            return Scores.Count > 0 ? total / Scores.Count : 0;
        }

        public void AddScore(Score score)
        {
            Scores.Add(score);
        }

        public bool ScoreExists(string subject)
        {
            foreach (Score score in Scores)
            {
                if (score.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateScore(string subject, double midtermScore, double finalScore)
        {
            Score foundScore = null;
            foreach (Score score in Scores)
            {
                if (score.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase))
                {
                    foundScore = score;
                    break;
                }
            }

            if (foundScore != null)
            {
                foundScore.MidtermScore = midtermScore;
                foundScore.FinalScore = finalScore;
                Console.WriteLine("Điểm môn học đã được cập nhật.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy môn học này.");
            }
        }
    }
}
