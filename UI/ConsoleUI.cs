using System;
using System.Collections.Generic;
using System.Text;
using QuanLyDiemHocSinh.Data;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.UI
{
    public static class ConsoleUI
    {
        private static List<Student> students = new List<Student>();
        private static string studentFilePath = "students.json";
        private static string loginFilePath = "login.json";
        private static string reportFilePath = "Report.json";
        private static string notificationFilePath = "notifications.json";

        public static void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Load dữ liệu đăng nhập
            List<LoginInfo> loginInfos = FileHandler.LoadLoginData(loginFilePath);
            if (loginInfos.Count == 0)
            {
                loginInfos.Add(new LoginInfo("admin", "admin", "admin"));
                loginInfos.Add(new LoginInfo("t1", "t1", "teacher", "Math"));
                loginInfos.Add(new LoginInfo("s1", "s1", "student"));
                FileHandler.SaveLoginData(loginFilePath, loginInfos);
            }

            LoginInfo loginInfo = Login(loginInfos);
            if (loginInfo == null) return;

            students = FileHandler.LoadDataFromFile(studentFilePath);
            Summary.SetStudents(students);

            // Gọi menu theo role
            if (loginInfo.Role == "student")
            {
                RunStudentMenu(loginInfo);
            }
            else if (loginInfo.Role == "teacher")
            {
                if (string.IsNullOrEmpty(loginInfo.Subject))
                {
                    Console.WriteLine("Vui lòng nhập môn học bạn giảng dạy:");
                    loginInfo.Subject = Console.ReadLine();
                    FileHandler.SaveLoginData(loginFilePath, loginInfos);
                }
                RunTeacherMenu(loginInfo);
            }
            else if (loginInfo.Role == "admin")
            {
                RunAdminMenu();
            }
        }

        // =========================
        // LOGIN
        // =========================
        private static LoginInfo Login(List<LoginInfo> loginInfos)
        {
            Console.WriteLine("=== ĐĂNG NHẬP HỆ THỐNG ===");
            Console.Write("Tên đăng nhập: ");
            string username = Console.ReadLine();
            Console.Write("Mật khẩu: ");
            string password = Console.ReadLine();

            foreach (LoginInfo info in loginInfos)
            {
                if (info.Username == username && info.Password == password)
                {
                    Console.WriteLine(">> Đăng nhập thành công!\n");
                    return info;
                }
            }
            Console.WriteLine("Sai tên đăng nhập hoặc mật khẩu.");
            return null;
        }

        // =========================
        // STUDENT MENU
        // =========================
        private static void RunStudentMenu(LoginInfo loginInfo)
        {
            while (true)
            {
                Menu.ShowStudentMenu();
                Console.Write("Chọn: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Program.SearchStudentAndDisplayScores();
                        break;
                    case "2":
                        Program.ViewReports(loginInfo.Username);
                        break;
                    case "3":
                        Program.ViewNotifications(loginInfo.Username);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ!");
                        break;
                }
            }
        }

        // =========================
        // TEACHER MENU
        // =========================
        private static void RunTeacherMenu(LoginInfo loginInfo)
        {
            while (true)
            {
                Menu.ShowTeacherMenu();
                Console.Write("Chọn: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Program.AddOrUpdateScore(loginInfo.Subject);
                        break;
                    case "2":
                        Summary.SummarizeBySubject(loginInfo.Subject);
                        break;
                    case "3":
                        Program.SendReport(loginInfo.Username, loginInfo.Subject);
                        break;
                    case "4":
                        Program.ViewNotifications(loginInfo.Username);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ!");
                        break;
                }
            }
        }

        // =========================
        // ADMIN MENU
        // =========================
        private static void RunAdminMenu()
        {
            while (true)
            {
                Menu.ShowAdminMenu();
                Console.Write("Chọn: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Program.AddStudent();
                        break;
                    case "2":
                        Program.AddOrUpdateScore();
                        break;
                    case "3":
                        Program.UpdateStudentInfo();
                        break;
                    case "4":
                        Program.SearchStudentAndDisplayScores();
                        break;
                    case "5":
                        Summary.SummarizeByClass();
                        break;
                    case "6":
                        Program.SendNotification();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ!");
                        break;
                }
            }
        }
    }
}
