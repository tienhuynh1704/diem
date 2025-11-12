using System;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.Services
{
    public class TeacherService
    {
        public void SetSubject(LoginInfo loginInfo)
        {
            if (loginInfo == null)
            {
                Console.WriteLine("Thông tin đăng nhập không hợp lệ.");
                return;
            }

            if (string.IsNullOrEmpty(loginInfo.Subject))
            {
                Console.WriteLine("Vui lòng nhập môn học bạn giảng dạy:");
                string subject = Console.ReadLine();
                loginInfo.Subject = subject;
            }
        }
    }
}
