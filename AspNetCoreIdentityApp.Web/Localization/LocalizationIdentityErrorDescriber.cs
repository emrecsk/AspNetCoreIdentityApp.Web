using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Localization
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError() { Code = nameof(DuplicateUserName), Description = $"Bu kullanıcı adı ({userName}) zaten kullanımda." };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError() { Code = nameof(DuplicateEmail), Description = $"Bu e-posta adresi ({email}) zaten kullanımda." };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError() { Code = nameof(PasswordTooShort), Description = $"Şifre en az {length} karakter uzunluğunda olmalıdır." };
        }
    }
}
