namespace QuanLyDiemHocSinh.Models
{
    public class LoginInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }   // "admin", "teacher", "student"
        public string Subject { get; set; } // dùng cho teacher

        public LoginInfo() { }

        public LoginInfo(string username, string password, string role, string subject = "")
        {
            Username = username;
            Password = password;
            Role = role;
            Subject = subject;
        }
    }
}
