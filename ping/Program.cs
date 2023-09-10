using System;
using System.Net.NetworkInformation;
using System.Net.Mail;

namespace NetworkMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            // IP-адрес или хост для мониторинга
            string target = "google.com";

            // Опции отправки письма
            string emailFrom = "selestor379@mail.ru"; // Адрес отправителя
            string emailTo = "nikitaefimenko496@mail.ru"; // Адрес получателя
            string emailSubject = "Network Monitoring Alert"; // Тема письма
            string emailBody = "The target network resource is unavailable."; // Текст письма

            // Бесконечный цикл мониторинга
            while (true)
            {
                bool isPingSuccessful = PingHost(target);

                // Если ping неуспешный, отправляем уведомление по почте
                if (!isPingSuccessful)
                {
                    SendEmail(emailFrom, emailTo, emailSubject, emailBody);
                }

                // Ожидаем некоторое время перед повторной проверкой
                System.Threading.Thread.Sleep(5000); // В данном примере проверка будет выполняться каждые 5 секунд
            }
        }

        // Функция для выполнения ping-запроса к заданному хосту
        static bool PingHost(string target)
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(target);

                if (reply != null && reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Пинг успешен");
                    return true;
                }
                else
                {
                    Console.WriteLine("Пинг провальный");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
                return false;
            }
        }

        // Функция для отправки уведомления по почте
        static void SendEmail(string emailFrom, string emailTo, string emailSubject, string emailBody)
        {
            try
            {
                MailMessage message = new MailMessage(emailFrom, emailTo, emailSubject, emailBody);
                SmtpClient client = new SmtpClient("smtp.mail.ru"); // Добавьте адрес SMTP-сервера

                // Задайте настройки SMTP-аутентификации при необходимости
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("nikitaefimenko496@mail.ru", "Efim1598745623."); // Добавьте учетные данные

                client.Send(message);
                Console.WriteLine("Отправлено уведомление по электронной почте");
            }
            catch (Exception ex)
            {
                Console.WriteLine("При отправке электронного письма произошла ошибка: " + ex.Message);
            }
        }
    }
}