using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.Data
{
    public static class FileHandler
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true, // format JSON cho dễ đọc
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // giữ nguyên Unicode (có dấu tiếng Việt)
        };

        // ------------------ STUDENTS ------------------
        public static void SaveDataToFile(string filePath, List<Student> students)
        {
            string jsonString = JsonSerializer.Serialize(students, options);
            File.WriteAllText(filePath, jsonString, Encoding.UTF8);
        }

        public static List<Student> LoadDataFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Student>();

            string jsonString = File.ReadAllText(filePath, Encoding.UTF8);
            return JsonSerializer.Deserialize<List<Student>>(jsonString, options) ?? new List<Student>();
        }

        // ------------------ LOGIN ------------------
        public static void SaveLoginData(string filePath, List<LoginInfo> loginInfos)
        {
            string jsonString = JsonSerializer.Serialize(loginInfos, options);
            File.WriteAllText(filePath, jsonString, Encoding.UTF8);
        }

        public static List<LoginInfo> LoadLoginData(string filePath)
        {
            if (!File.Exists(filePath)) return new List<LoginInfo>();

            string jsonString = File.ReadAllText(filePath, Encoding.UTF8);
            return JsonSerializer.Deserialize<List<LoginInfo>>(jsonString, options) ?? new List<LoginInfo>();
        }

        // ------------------ REPORTS ------------------
        public static void SaveReportsToFile(string filePath, List<Report> reports)
        {
            string jsonString = JsonSerializer.Serialize(reports, options);
            File.WriteAllText(filePath, jsonString, Encoding.UTF8);
        }

        public static List<Report> LoadReports(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Report>();

            string jsonString = File.ReadAllText(filePath, Encoding.UTF8);
            return JsonSerializer.Deserialize<List<Report>>(jsonString, options) ?? new List<Report>();
        }

        // ------------------ NOTIFICATIONS ------------------
        public static void SaveNotifications(string filePath, List<Notification> notifications)
        {
            string jsonString = JsonSerializer.Serialize(notifications, options);
            File.WriteAllText(filePath, jsonString, Encoding.UTF8);
        }

        public static List<Notification> LoadNotifications(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Notification>();

            string jsonString = File.ReadAllText(filePath, Encoding.UTF8);
            return JsonSerializer.Deserialize<List<Notification>>(jsonString, options) ?? new List<Notification>();
        }
    }
}
