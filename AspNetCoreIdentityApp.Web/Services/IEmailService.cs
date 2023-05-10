namespace AspNetCoreIdentityApp.Web.Services
{
    public interface IEmailService
    {
        Task SendResetEmail(string resetmailLink, string ToEmail);
    }
}
