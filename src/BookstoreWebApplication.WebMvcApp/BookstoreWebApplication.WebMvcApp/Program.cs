using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookstoreWebApplication.WebMvcApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //1. cast - konfigurace Cookies
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    // odkaz na kontroler (jejich akce)
                    options.LoginPath = "/Auth/Login";
                    //options.LogoutPath = "/Auth/Logout";
                    //options.AccessDeniedPath = "/Auth/Denied";

                    options.Cookie.HttpOnly = true;
                    //v odkud musi byt cookie; jestli funguje napric domene/strance/...
                    options.Cookie.SameSite = SameSiteMode.Lax;
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // 2. cast - Zapnuti autentizace
            //  ! DULEZITE PORADI ! - nejdriv se autentizuje, pak autorizuje
            app.UseAuthentication(); // Kdo jsme
            app.UseAuthorization(); // Cim jsme (jake role mame)


            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
