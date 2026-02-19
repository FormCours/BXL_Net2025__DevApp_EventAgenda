using Demo_WebAPI_EventAgenda.ApplicationCore.Interfaces.Utils;
using Demo_WebAPI_EventAgenda.Domain.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Demo_WebAPI_EventAgenda.Infrastructure.Mailer
{
    public class MailerUtil : IMailerUtil
    {
        private string _Host { get; set; }
        private int _Port { get; set; }
        private string _Username { get; set; }
        private string _Password { get; set; }
        private string _AppEmail { get; set; }
        private string _AppName { get; set; }

        public MailerUtil(string host, int port, string username, string password, string appEmail, string appName)
        {
            _Host = host;
            _Port = port;
            _Username = username;
            _Password = password;
            _AppEmail = appEmail;
            _AppName = appName;
        }

        public async Task<string> LoadMailTemplateAsync(string templateFileName)
        {
            if (string.IsNullOrWhiteSpace(templateFileName))
                throw new ArgumentException("templateFileName must be provided", nameof(templateFileName));

            var basePath = AppContext.BaseDirectory;
            var fullPath = Path.Combine(basePath, "Templates", templateFileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Template file not found: {fullPath}", fullPath);

            using var stream = File.OpenRead(fullPath);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private void SendMail(MimeMessage message)
        {
            using SmtpClient smtpClient = new SmtpClient();
            try
            {
                // - Connexion au serveur Smtp
                smtpClient.Connect(_Host, _Port, false);

                // - Authentification
                smtpClient.Authenticate(_Username, _Password);

                // - Envoi du mail 
                smtpClient.Send(message);
            }
            finally
            {
                smtpClient.Disconnect(true);
            }
        }

        public void SendWelcomeMessage(Member member)
        {
            MimeMessage message = new MimeMessage();

            // Config du message mail
            message.Subject = "Bienvenue sur Agenda Event !";
            message.From.Add(new MailboxAddress(_AppName, _AppEmail));
            message.To.Add(new MailboxAddress( member.Pseudo,  member.Email));

            // Génértion du Contenu (Simple)
            string msgHtml = LoadMailTemplateAsync("WelcomeTemplate.html").Result;
            msgHtml = msgHtml.Replace("{{UserName}}", member.Pseudo ?? member.Email);

            // Définition du body du mail
            BodyBuilder bodyBuilder = new BodyBuilder();

            bodyBuilder.TextBody = "Bienvenue.";
            bodyBuilder.HtmlBody = msgHtml;

            message.Body = bodyBuilder.ToMessageBody();

            // Envoi du mail
            SendMail(message);
        }
    }
}
