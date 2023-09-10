using System;
using System.Net.NetworkInformation;
using System.Net.Mail;

namespace NetworkMonitor
{
    class Program
    {
        static void Main(string[] args)
        {

            string target = "google.com";


            string emailFrom = "selestor379@mail.ru"; // Адрес отправителя
            string emailTo = "nikitaefimenko496@mail.ru"; // Адрес получателя
            string emailSubject = "Алерт о пинге"; // Тема письма
            string emailBody = "Что-то не работает короче."; // Текст письма


            while (true)
            {
                bool isPingSuccessful = PingHost(target);

                if (!isPingSuccessful)
                {
                    SendEmail(emailFrom, emailTo, emailSubject, emailBody);
                }

                
                System.Threading.Thread.Sleep(5000); 
            }
        }


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


        static void SendEmail(string emailFrom, string emailTo, string emailSubject, string emailBody)
        {
            try
            {
                MailMessage message = new MailMessage(emailFrom, emailTo, emailSubject, emailBody);
                SmtpClient client = new SmtpClient("smtp.mail.ru"); 


                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("nikitaefimenko496@mail.ru", "Efim1598745623."); 

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