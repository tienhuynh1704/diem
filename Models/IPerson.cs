using System;

namespace StudentManagement.Services
{
    // Interface mô tả thông tin chung của một người
    public interface IPerson
    {
        // Thuộc tính chung
        int Id { get; set; }
        string FullName { get; set; }
        string Gender { get; set; }
        DateTime DateOfBirth { get; set; }

        // Hành vi chung
        void DisplayInfo(); // Hiển thị thông tin
    }
}
