using System;
using System.Runtime.Serialization;

namespace QuanLyDiemHocSinh.Models
{
    [Serializable]
    public class Score : ISerializable
    {
        public string Subject { get; set; }
        public double MidtermScore { get; set; }
        public double FinalScore { get; set; }

        // Constructor mặc định
        public Score() { }

        // Constructor khởi tạo đầy đủ
        public Score(string subject, double midtermScore, double finalScore)
        {
            Subject = subject;
            MidtermScore = midtermScore;
            FinalScore = finalScore;
        }

        // Constructor đặc biệt để giải tuần tự hóa
        protected Score(SerializationInfo info, StreamingContext context)
        {
            Subject = info.GetString("Subject");
            MidtermScore = info.GetDouble("MidtermScore");
            FinalScore = info.GetDouble("FinalScore");
        }

        // Phương thức tuần tự hóa
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Subject", Subject);
            info.AddValue("MidtermScore", MidtermScore);
            info.AddValue("FinalScore", FinalScore);
        }

        // Tính điểm cuối cùng
        public double CalculateFinalScore()
        {
            return (MidtermScore * 0.4) + (FinalScore * 0.6);
        }

        // Hiển thị điểm
        public void DisplayScore()
        {
            Console.WriteLine(
                "Môn học: " + Subject +
                ", Giữa kỳ: " + MidtermScore +
                ", Cuối kỳ: " + FinalScore +
                ", Điểm cuối: " + CalculateFinalScore().ToString("F2")
            );
        }
    }
}
