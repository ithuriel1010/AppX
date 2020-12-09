using Android.Telephony;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Graphics.Pdf.PdfDocument;
using static System.Net.Mime.MediaTypeNames;

namespace AppX
{
    public class SendTextAndEmail
    {
        public string message;
        public string number;
        public string email;
        public SendTextAndEmail()
        {
        }

        public bool Send(String message, String number)
        {
            bool sent;
            try
            {
                SmsManager.Default.SendTextMessage(number, null, message, null, null);
                sent = true;
            }
            catch (Exception ex)
            {
                SendNotification("Brak dostępu do wiadomości!", "Zezwól na wysyłanie wiadomości aby aplikacja działała lepiej", "LocalizationAlert");       //If app can't send text it displays an error notification
                sent = false;
            }
            return sent;
        }
        private void SendNotification(string title, string message, string action)
        {
            DependencyService.Get<INotification>().CreateNotification(title, message, action);
        }
        public void Email(String message, String email)     //Is not used in the end but it did work
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("test@gmail.com");
            mail.To.Add(email);
            mail.Subject = "your mail subject";
            mail.Body = message;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("test@gmail.com", "xxxxx");
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                return true;
            };
            SmtpServer.Send(mail);
        }
    }
}
