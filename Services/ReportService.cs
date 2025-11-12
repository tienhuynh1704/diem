using System;
using System.Collections.Generic;
using QuanLyDiemHocSinh.Data;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.Services
{
    public class ReportService
    {
        private readonly string reportFilePath;

        public ReportService(string reportFilePath)
        {
            this.reportFilePath = reportFilePath;
        }

        public void SendReport(string teacherName, string subject)
        {
            Console.WriteLine("Nhập mã học sinh:");
            string studentId = Console.ReadLine();

            Console.WriteLine("Nhập nội dung báo cáo:");
            string content = Console.ReadLine();

            Report report = new Report(studentId, teacherName, content, subject);

            List<Report> reports = FileHandler.LoadReports(reportFilePath);
            reports.Add(report);
            FileHandler.SaveReportsToFile(reportFilePath, reports);

            Console.WriteLine("Báo cáo đã được gửi thành công.");
        }

        public void ViewReports(string studentId)
        {
            List<Report> reports = FileHandler.LoadReports(reportFilePath);

            // ✅ Không dùng LINQ, tìm thủ công
            List<Report> studentReports = new List<Report>();
            for (int i = 0; i < reports.Count; i++)
            {
                if (reports[i].StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                {
                    studentReports.Add(reports[i]);
                }
            }

            if (studentReports.Count > 0)
            {
                Console.WriteLine("Các báo cáo từ giáo viên:");
                for (int i = 0; i < studentReports.Count; i++)
                {
                    Report report = studentReports[i];
                    Console.WriteLine(
                        "- Báo cáo từ " + report.TeacherName +
                        " vào " + report.Date.ToString("dd/MM/yyyy HH:mm") +
                        ": " + report.Content +
                        " (Môn học: " + report.Subject + ")"
                    );
                }
            }
            else
            {
                Console.WriteLine("Không có báo cáo nào từ giáo viên cho bạn.");
            }
        }
    }
}
