using System;

namespace QuanLyDiemHocSinh.Models
{
    [Serializable]
    public class Notification
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public delegate void NotificationSentHandler(Notification notification);
        public event NotificationSentHandler OnNotificationSent;

        public Notification() { }

        public Notification(string sender, string recipient, string content)
        {
            Sender = sender;
            Recipient = recipient;
            Content = content;
            Date = DateTime.Now;
        }

        public void SendNotification()
        {
            OnNotificationSent?.Invoke(this);
        }
    }

    public class NotificationManager
    {
        public void RegisterNotification(Notification notification)
        {
            notification.OnNotificationSent += HandleNotification;
        }

        private void HandleNotification(Notification notification)
        {
            Console.WriteLine($"Notification from {notification.Sender} to {notification.Recipient}: {notification.Content} at {notification.Date}");
        }
    }
}
