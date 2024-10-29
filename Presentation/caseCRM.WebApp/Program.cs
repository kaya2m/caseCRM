using caseCRM.Service.Configuration;
using Microsoft.AspNetCore.Builder;

namespace caseCRM.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddPostgreSqlConnection(builder.Configuration);

            builder.Services.AddJwtAuthentication(builder.Configuration)
                .AddAppAuthorization()
                .AddLoggingConfiguration();

            builder.Services.RegisterRepositories();
            builder.Services.RegisterServices();
            builder.Services.ConfigureSwagger();
            builder.Services.AddCors();
            builder.Services.AddSession();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/" || context.Request.Path == "/index.html")
                {
                    context.Response.Redirect("/auth/login");
                    return;
                }
                await next();
            });

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            app.Run();
        }
    }
}

