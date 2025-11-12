using System;
using System.Collections.Generic;
using QuanLyDiemHocSinh.Data;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.Services
{
    public class AuthService
    {
        private readonly string _loginFilePath;

        public AuthService(string loginFilePath)
        {
            _loginFilePath = loginFilePath;
        }

        /// <summary>
        /// Xử lý đăng nhập, trả về LoginInfo nếu thành công, ngược lại trả về null
        /// </summary>
        public LoginInfo Login(List<LoginInfo> loginInfos)
        {
            Console.WriteLine("=== Đăng nhập hệ thống ===");
            Console.Write("Tên đăng nhập: ");
            string username = Console.ReadLine();

            Console.Write("Mật khẩu: ");
            string password = ReadPassword();

            // Tìm tài khoản phù hợp bằng vòng lặp (không dùng LINQ, Lambda)
            for (int i = 0; i < loginInfos.Count; i++)
            {
                LoginInfo info = loginInfos[i];
                if (info.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                    && info.Password == password)
                {
                    Console.WriteLine("✅ Đăng nhập thành công!");
                    return info;
                }
            }

            Console.WriteLine("❌ Sai tên đăng nhập hoặc mật khẩu.");
            return null;
        }

        /// <summary>
        /// Lưu danh sách tài khoản ra file
        /// </summary>
        public void SaveAccounts(List<LoginInfo> loginInfos)
        {
            FileHandler.SaveLoginData(_loginFilePath, loginInfos);
        }

        /// <summary>
        /// Hàm nhập mật khẩu ẩn ký tự (Console app)
        /// </summary>
        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }
}
