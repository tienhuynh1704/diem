using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using QuanLyDiemHocSinh.Data;
using QuanLyDiemHocSinh.Models;
using QuanLyDiemHocSinh.UI;

namespace QuanLyDiemHocSinh
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static string studentFilePath = "students.json";
        static string loginFilePath = "login.json";
        static string reportFilePath = "Report.json";
        static string notificationFilePath = "notifications.json";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // Load login data (if file missing, create sample
            List<LoginInfo> loginInfos = FileHandler.LoadLoginData(loginFilePath);
            if (loginInfos.Count == 0)
            {
                loginInfos.Add(new LoginInfo("admin", "admin", "admin"));
                loginInfos.Add(new LoginInfo("t1", "t1", "teacher", "Math"));
                loginInfos.Add(new LoginInfo("s1", "s1", "student"));
                FileHandler.SaveLoginData(loginFilePath, loginInfos);
            }

            LoginInfo loginInfo = Login(loginInfos);
            students = FileHandler.LoadDataFromFile(studentFilePath);
            Summary.SetStudents(students);

            if (loginInfo == null) return;

            if (loginInfo.Role == "student")
            {
                while (true)
                {
                    Menu.ShowStudentMenu();
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            SearchStudentAndDisplayScores();
                            break;
                        case "2":
                            ViewReports(loginInfo.Username);
                            break;
                        case "3":
                            ViewNotifications(loginInfo.Username);
                            break;
                        case "4":
                            return;
                        default:
                            Console.WriteLine("Tùy chọn không hợp lệ, vui lòng thử lại.");
                            break;
                    }
                }
            }
            else if (loginInfo.Role == "teacher")
            {
                if (string.IsNullOrEmpty(loginInfo.Subject))
                {
                    Console.WriteLine("Vui lòng nhập môn học bạn giảng dạy:");
                    loginInfo.Subject = Console.ReadLine();
                    FileHandler.SaveLoginData(loginFilePath, loginInfos);
                }

                while (true)
                {
                    Menu.ShowTeacherMenu();
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddOrUpdateScore(loginInfo.Subject);
                            break;
                        case "2":
                            Summary.SummarizeBySubject(loginInfo.Subject);
                            break;
                        case "3":
                            SendReport(loginInfo.Username, loginInfo.Subject);
                            break;
                        case "4":
                            ViewNotifications(loginInfo.Username);
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Tùy chọn không hợp lệ, vui lòng thử lại.");
                            break;
                    }
                }
            }
            else if (loginInfo.Role == "admin")
            {
                while (true)
                {
                    Menu.ShowAdminMenu();
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddStudent();
                            break;
                        case "2":
                            AddOrUpdateScore();
                            break;
                        case "3":
                            UpdateStudentInfo();
                            break;
                        case "4":
                            SearchStudentAndDisplayScores();
                            break;
                        case "5":
                            Summary.SummarizeByClass();
                            break;
                        case "6":
                            SendNotification();
                            break;
                        case "7":
                            return;
                        default:
                            Console.WriteLine("Tùy chọn không hợp lệ, vui lòng thử lại.");
                            break;
                    }
                }
            }
        }

        public static void SendReport(string teacherName, string subject)
        {
            Console.WriteLine("Nhập mã học sinh:");
            string studentId = Console.ReadLine();

            Console.WriteLine("Nhập nội dung báo cáo:");
            string content = Console.ReadLine();

            Report report = new Report(studentId, teacherName, content, subject);
            SaveReport(report);

            Console.WriteLine("Báo cáo đã được gửi thành công.");
        }

        public static void ViewReports(string studentId)
        {
            List<Report> reports = FileHandler.LoadReports(reportFilePath);
            List<Report> studentReports = new List<Report>();

            foreach (Report r in reports)
            {
                if (r.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                {
                    studentReports.Add(r);
                }
            }

            if (studentReports.Count > 0)
            {
                Console.WriteLine("Các báo cáo từ giáo viên:");
                foreach (Report report in studentReports)
                {
                    Console.WriteLine($"- Báo cáo từ {report.TeacherName} vào {report.Date}: {report.Content} (Môn học: {report.Subject})");
                }
            }
            else
            {
                Console.WriteLine("Không có báo cáo nào từ giáo viên cho bạn.");
            }
        }

        public class NotificationHandler
        {
            private List<Notification> notifications = FileHandler.LoadNotifications(notificationFilePath);
            private NotificationManager notificationManager = new NotificationManager();

            public void SendNotification(string sender, string recipient, string content)
            {
                Notification notification = new Notification(sender, recipient, content);
                notifications.Add(notification);
                notificationManager.RegisterNotification(notification);
                notification.OnNotificationSent += Notification_OnNotificationSent;
                notification.SendNotification();

                FileHandler.SaveNotifications(notificationFilePath, notifications);
            }

            private void Notification_OnNotificationSent(Notification notification)
            {
                Console.WriteLine($"Thông báo từ {notification.Sender}");
            }

            public void UnbindNotification(Notification notification)
            {
                notification.OnNotificationSent -= Notification_OnNotificationSent;
            }

            public List<Notification> GetNotificationsForUser(string username)
            {
                List<Notification> result = new List<Notification>();
                foreach (Notification n in notifications)
                {
                    if (n.Recipient.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(n);
                    }
                }
                return result;
            }

            public void DisplayNotifications(List<Notification> notifications)
            {
                foreach (Notification notification in notifications)
                {
                    Console.WriteLine($"Từ: {notification.Sender}, Ngày: {notification.Date}, Nội dung: {notification.Content}");
                }
            }
        }

        public static void SendNotification()
        {
            Console.WriteLine("Nhập tên người nhận (học sinh hoặc giáo viên):");
            string recipient = Console.ReadLine();
            Console.WriteLine("Nhập nội dung thông báo:");
            string content = Console.ReadLine();
            string sender = "Admin";

            NotificationHandler notificationHandler = new NotificationHandler();
            notificationHandler.SendNotification(sender, recipient, content);
        }

        public static void ViewNotifications(string username)
        {
            NotificationHandler notificationHandler = new NotificationHandler();
            List<Notification> notifications = notificationHandler.GetNotificationsForUser(username);
            if (notifications.Count == 0)
            {
                Console.WriteLine("Không có thông báo nào.");
            }
            else
            {
                notificationHandler.DisplayNotifications(notifications);
            }
        }

        public static void SaveReport(Report report)
        {
            List<Report> reports = FileHandler.LoadReports(reportFilePath);
            reports.Add(report);
            FileHandler.SaveReportsToFile(reportFilePath, reports);
        }

        public static LoginInfo Login(List<LoginInfo> loginInfos)
        {
            try
            {
                Console.WriteLine("Vui lòng đăng nhập:");
                Console.Write("Tên đăng nhập: ");
                string username = Console.ReadLine();
                Console.Write("Mật khẩu: ");
                string password = Console.ReadLine();

                foreach (LoginInfo info in loginInfos)
                {
                    if (info.Username == username && info.Password == password)
                    {
                        Console.WriteLine("Đăng nhập thành công!");
                        return info;
                    }
                }
                Console.WriteLine("Sai tên đăng nhập hoặc mật khẩu.");
                return null;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Lỗi NullReferenceException: " + ex.Message);
                return null;
            }
        }

        public static void AddStudent()
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

        public static void AddOrUpdateScore(string teacherSubject = "")
        {
            Console.WriteLine("Nhập mã học sinh:");
            string studentId = Console.ReadLine();

            Student foundStudent = null;
            foreach (Student s in students)
            {
                if (s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                {
                    foundStudent = s;
                    break;
                }
            }

            if (foundStudent != null)
            {
                Console.WriteLine("Nhập môn học:");
                string subject = Console.ReadLine();

                if (!string.IsNullOrEmpty(teacherSubject) && !subject.Equals(teacherSubject, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Bạn không được phép sửa điểm cho môn học này.");
                    return;
                }

                Console.WriteLine("Nhập điểm giữa kỳ:");
                double midtermScore = double.Parse(Console.ReadLine());
                Console.WriteLine("Nhập điểm cuối kỳ:");
                double finalScore = double.Parse(Console.ReadLine());

                if (foundStudent.ScoreExists(subject))
                {
                    foundStudent.UpdateScore(subject, midtermScore, finalScore);
                }
                else
                {
                    foundStudent.AddScore(new Score(subject, midtermScore, finalScore));
                    Console.WriteLine("Điểm môn học đã được thêm.");
                }
                FileHandler.SaveDataToFile(studentFilePath, students);
            }
            else
            {
                Console.WriteLine("Không tìm thấy học sinh.");
            }
        }

        private static void Notification_OnNotificationSent(Notification notification)
        {
            Console.WriteLine($"Thông báo từ {notification.Sender} tới {notification.Recipient}: {notification.Content} vào lúc {notification.Date}");
        }

        public static void SearchStudentAndDisplayScores()
        {
            students = FileHandler.LoadDataFromFile(studentFilePath);
            Console.WriteLine("Nhập mã học sinh:");
            string studentId = Console.ReadLine();

            Student foundStudent = null;
            foreach (Student s in students)
            {
                if (s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                {
                    foundStudent = s;
                    break;
                }
            }

            if (foundStudent != null)
            {
                foundStudent.DisplayInfo();
                foreach (Score score in foundStudent.Scores)
                {
                    score.DisplayScore();
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy học sinh.");
            }
        }

        public static void UpdateStudentInfo()
        {
            Console.WriteLine("Nhập mã học sinh:");
            string studentId = Console.ReadLine();

            Student foundStudent = null;
            foreach (Student s in students)
            {
                if (s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                {
                    foundStudent = s;
                    break;
                }
            }

            if (foundStudent != null)
            {
                Console.WriteLine("Nhập tên mới:");
                foundStudent.Name = Console.ReadLine();
                Console.WriteLine("Nhập tuổi mới:");
                foundStudent.Age = int.Parse(Console.ReadLine());
                Console.WriteLine("Nhập địa chỉ mới:");
                foundStudent.Address = Console.ReadLine();
                Console.WriteLine("Nhập lớp mới:");
                foundStudent.Class = Console.ReadLine();

                FileHandler.SaveDataToFile(studentFilePath, students);
                Console.WriteLine("Cập nhật thông tin học sinh thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy học sinh.");
            }
        }
    }
}
