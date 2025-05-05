using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string html);
}

public class EmailService(IConfiguration config) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string html)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(config["Email:From"]));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = html };

        using var client = new SmtpClient();
        await client.ConnectAsync(config["Email:SmtpServer"],
            int.Parse(config["Email:Port"] ?? throw new InvalidOperationException()), true);
        await client.AuthenticateAsync(config["Email:Username"], config["Email:Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}