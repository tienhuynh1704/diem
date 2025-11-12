using System;

namespace QuanLyDiemHocSinh.UI
{
    public static class Menu
    {
        public static void ShowAdminMenu()
        {
            Console.WriteLine("=== Hệ thống quản lý học sinh ===");
            Console.WriteLine("1. Thêm học sinh");
            Console.WriteLine("2. Thêm hoặc sửa điểm");
            Console.WriteLine("3. Sửa thông tin của học sinh");
            Console.WriteLine("4. Tìm kiếm và xem điểm của học sinh");
            Console.WriteLine("5. Danh sách học sinh");
            Console.WriteLine("6. Gửi thông báo");
            Console.WriteLine("7. Thoát");
        }

        public static void ShowTeacherMenu()
        {
            Console.WriteLine("=== Hệ thống quản lý học sinh - Giáo viên ===");
            Console.WriteLine("1. Thêm hoặc sửa điểm môn học");
            Console.WriteLine("2. Tổng kết môn học");
            Console.WriteLine("3. Gửi báo cáo tình hình học tập cho học sinh");
            Console.WriteLine("4. Xem thông báo");
            Console.WriteLine("5. Thoát");
        }

        public static void ShowStudentMenu()
        {
            Console.WriteLine("=== Hệ thống quản lý học sinh - Học sinh ===");
            Console.WriteLine("1. Xem điểm");
            Console.WriteLine("2. Xem báo cáo từ giáo viên");
            Console.WriteLine("3. Xem thông báo");
            Console.WriteLine("4. Thoát");
        }
    }
}
