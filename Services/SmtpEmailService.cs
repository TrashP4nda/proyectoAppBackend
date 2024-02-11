namespace proyectoApi.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


public class SmtpEmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;

    public SmtpEmailService()
    {
        _smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("dmartin3005@gmail.com", "glsh yxak gyed qfwr"),
            EnableSsl = true,
        };
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress("dmartin3005@gmail.com"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(toEmail);

        await _smtpClient.SendMailAsync(mailMessage);
    }
}