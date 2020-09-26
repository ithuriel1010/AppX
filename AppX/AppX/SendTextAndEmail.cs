using Android.Telephony;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Essentials;
using static Android.Graphics.Pdf.PdfDocument;
using static System.Net.Mime.MediaTypeNames;

namespace AppX
{
    public class SendTextAndEmail
    {
        public string message;
        public string number;
        public string email;
        public SendTextAndEmail(String message, String number, String email)
        {
            this.message = message;
            this.number = number;
            this.email = email;
            Send(message, number);
            //Email(message, email);
        }

        public void Send(String message, String number)
        {
            try
            {
                SmsManager.Default.SendTextMessage(number, null, message, null, null);
            }
            catch (FeatureNotSupportedException ex)
            {
                //Application.Current.MainPage.Navigation.PopAsync();

                //Page p = new Page();

                //p.DisplayAlert("Failed", "Sms is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                //Application.Current.MainPage.Navigation.PopAsync();

                //Page p = new Page();

                //p.DisplayAlert("failed", ex.Message, "ok");
            }

        }

        public void Email(String message, String email)
        {
            //define mail
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("olineczkaw@gmail.com");
            mail.To.Add(email);
            mail.Subject = "your mail subject";
            mail.Body = message;

            //if you want to send an attachment just define filename

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(filename);
            //mail.Attachments.Add(attachment);

            //end email attachment part

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("olineczkaw@gmail.com", "Ale10Xandretta");
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                return true;
            };
            SmtpServer.Send(mail);
        }
    }
}
