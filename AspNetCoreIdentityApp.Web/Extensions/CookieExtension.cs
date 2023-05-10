namespace AspNetCoreIdentityApp.Web.Extensions
{
    public static class CookieExtension
    {
        public static void AddCookieConfig(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                var cookiebuilder = new CookieBuilder();
                cookiebuilder.Name = "AspNetCoreIdentityApp";
                options.LoginPath = new PathString("/Home/SignIn");
                options.LogoutPath = new PathString("/Member/LogOut");
                options.Cookie = cookiebuilder;
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                options.SlidingExpiration = true; //If the user is active, then the expiration time will be extended.
            });
        }
    }
}
