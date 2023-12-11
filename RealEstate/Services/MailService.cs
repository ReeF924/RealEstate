using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RealEstate.Models.DatabaseModels;
namespace RealEstate.Services
{
    public static class MailService
    {
        public static void SendMail(User user, string password)
        {
            var smtpClient = new SmtpClient("LocalHost", 25);
            smtpClient.EnableSsl = false;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("realestate@mail.cz");
            mailMessage.To.Add(user.Email);
            mailMessage.Subject = "Forgot Password Realestate";
            mailMessage.Body = MailService.GetMessageBody(password);

            smtpClient.Send(mailMessage);

        }
        private static string GetMessageBody(string password)
        {
            return $"Your new password is: {password}";
        }

    }
}
