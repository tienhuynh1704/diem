using System;
using System.Runtime.Serialization;

namespace QuanLyDiemHocSinh.Models
{
    public interface IPerson
    {
        string Name { get; set; }
        int Age { get; set; }
        string Address { get; set; }
        void DisplayInfo();
    }

    [Serializable]
    public abstract class Person : ISerializable, IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public Person() { }

        public Person(string name, int age, string address)
        {
            Name = name;
            Age = age;
            Address = address;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                info.AddValue("Name", Name);
                info.AddValue("Age", Age);
                info.AddValue("Address", Address);
            }
            catch (SerializationException ex)
            {
                Console.WriteLine("Lỗi trong quá trình tuần tự hóa: " + ex.Message);
            }
        }

        protected Person(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            Age = info.GetInt32("Age");
            Address = info.GetString("Address");
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Tên: {Name}, Tuổi: {Age}, Địa chỉ: {Address}");
        }
    }
}
