
using preAceleracionDisney.Entities;
using preAceleracionDisney.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace preAceleracionDisney.Services
{
    public class MailService : IMailService
    {
        private readonly ISendGridClient _sendGridClient;

        public MailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendEmail(User user)
        {
            try
            {
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("arpegius666@gmail.com", "Admin Disney"),
                    Subject = "Disney Challenge - Welcome New User",
                    PlainTextContent = $"¡Welcome {user.UserName} at Disney Challenge!",
                    HtmlContent = "<strong>and easy to do anywhere, even with C#</strong>",
                };
                msg.AddContent(MimeType.Text, "and easy to do anywhere, even with C#");
                msg.AddTo(new EmailAddress(user.Email, "Challenge User")); //Se agrega el campo email del usuario registrado
                var response = await _sendGridClient.SendEmailAsync(msg);
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);

            }
        }
    }
}
