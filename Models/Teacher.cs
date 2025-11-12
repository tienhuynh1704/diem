using System;
using System.Collections.Generic;

namespace QuanLyDiemHocSinh.Models
{
    [Serializable]
    public class Teacher : Person
    {
        public List<string> Subjects { get; set; }

        public Teacher()
        {
            Subjects = new List<string>();
        }

        public Teacher(string name, int age, string address, string subject)
            : base(name, age, address)
        {
            Subjects = new List<string> { subject };
        }

        public void AddSubject(string subject)
        {
            if (!Subjects.Contains(subject))
            {
                Subjects.Add(subject);
            }
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Môn giảng dạy: {string.Join(", ", Subjects)}");
        }
    }
}
