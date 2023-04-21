using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.Extensions.Options;

namespace CSharp.Infrastructure.Providers;

public interface IMailProvider
{
    Task SendWelcomeMailAsync(
        string emailAddress,
        IDictionary<string, string> parameters,
        CancellationToken cancellationToken = default
        );
}
public class SmtpOptions
{
    public string Host { get; set; } = "";
    public int Port { get; set; }
    public bool UseSsl { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string FromEmail { get; set; } = "";
    public string FromDisplayName { get; set; } = "";
}
public static class MailTemplate
{
    public const string UserName = "username";
    public const string WelcomeMail_VerificationCode = "verification_code";
}
public class FakeMailProvider : IMailProvider
{
    public Task SendWelcomeMailAsync(string emailAddress, IDictionary<string, string> parameters, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new object());
    }
}
public class MailProvider : IMailProvider
{
    private readonly SmtpOptions options;

    public MailProvider(IOptions<SmtpOptions> smtpOptions)
    {
        this.options = smtpOptions.Value;
    }

    public async Task SendWelcomeMailAsync(string emailAddress, IDictionary<string, string> parameters, CancellationToken cancellationToken = default)
    {
        GetMailContent("welcome-mail", parameters, out var subject, out var body);
        await SendMailAsync(subject, body, new[] { emailAddress }, cancellationToken);
    }

    private void GetMailContent(string mailTypeAlias, IDictionary<string, string> parameters, out string subject, out string body)
    {
        GetMailContentFromSource(mailTypeAlias, out subject, out body);
        subject = Format(subject, parameters);
        body = Format(body, parameters);
    }

    private string Format(string content, IDictionary<string, string> parameters)
    {
        var matches = Regex.Matches(content, @"(\{[a-zA-Z_-]+\})");
        if (matches.Any() == false)
            return content;

        StringBuilder builder = new();
        int index = 0;
        foreach (Match match in matches)
        {
            var length = match.Index - index;
            builder.Append(content.Substring(index, length));
            if (parameters.TryGetValue(match.Value, out var value))
                builder.Append(value);
            index = match.Index + match.Length;
        }
        builder.Append(content.Substring(index, content.Length - index));
        return builder.ToString();
    }

    private void GetMailContentFromSource(string mailTypeAlias, out string subject, out string body)
    {
        // Here you can fetch your template either from a file, the database or a 3rd type of source
        throw new NotImplementedException();
    }

    private async Task SendMailAsync(string subject, string body, string[] recipients, CancellationToken cancellationToken)
    {
        var from = new MailAddress(options.FromEmail, options.FromDisplayName);

        using var client = new SmtpClient(options.Host, options.Port);
        client.Credentials = new NetworkCredential(options.Username, options.Password);
        client.EnableSsl = options.UseSsl;

        var message = new MailMessage
        {
            From = from,
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        foreach (var item in recipients)
            message.To.Add(item);

        await client.SendMailAsync(message, cancellationToken);
    }
}