using System;
using System.Collections.Generic;
using QuanLyDiemHocSinh.Data;
using QuanLyDiemHocSinh.Models;

namespace QuanLyDiemHocSinh.Services
{
    public class NotificationService
    {
        private readonly string notificationFilePath;
        private List<Notification> notifications;

        public NotificationService(string notificationFilePath)
        {
            this.notificationFilePath = notificationFilePath;
            notifications = FileHandler.LoadNotifications(notificationFilePath);
        }

        public void SendNotification(string sender, string recipient, string content)
        {
            Notification notification = new Notification(sender, recipient, content);
            notifications.Add(notification);

            notification.OnNotificationSent += Notification_OnNotificationSent;
            notification.SendNotification();

            FileHandler.SaveNotifications(notificationFilePath, notifications);
        }

        private void Notification_OnNotificationSent(Notification notification)
        {
            Console.WriteLine("Thông báo từ " + notification.Sender);
        }

        public List<Notification> GetNotificationsForUser(string username)
        {
            List<Notification> userNotifications = new List<Notification>();

            for (int i = 0; i < notifications.Count; i++)
            {
                if (notifications[i].Recipient.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    userNotifications.Add(notifications[i]);
                }
            }

            return userNotifications;
        }

        public void DisplayNotifications(string username)
        {
            List<Notification> list = GetNotificationsForUser(username);

            if (list.Count == 0)
            {
                Console.WriteLine("Không có thông báo nào.");
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Notification n = list[i];
                Console.WriteLine("Từ: " + n.Sender + ", Ngày: " + n.Date.ToString("dd/MM/yyyy HH:mm") + ", Nội dung: " + n.Content);
            }
        }
    }
}
