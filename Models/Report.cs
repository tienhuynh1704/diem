using System;

namespace QuanLyDiemHocSinh.Models
{
    [Serializable]
    public class Report
    {
        public string StudentId { get; set; }
        public string TeacherName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }

        // Định nghĩa EventArgs để tuân thủ chuẩn .NET
        public class ReportEventArgs : EventArgs
        {
            public Report Report { get; private set; }

            public ReportEventArgs(Report report)
            {
                Report = report;
            }
        }

        // Event handler theo chuẩn
        public event EventHandler<ReportEventArgs> ReportCreated;

        public Report() { }

        public Report(string studentId, string teacherName, string content, string subject)
        {
            StudentId = studentId;
            TeacherName = teacherName;
            Content = content;
            Date = DateTime.Now;
            Subject = subject;
            OnReportCreated();
        }

        protected virtual void OnReportCreated()
        {
            if (ReportCreated != null)
            {
                ReportCreated(this, new ReportEventArgs(this));
            }
        }

        public void AssignReport()
        {
            OnReportCreated();
        }
    }
}
