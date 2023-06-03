using FirstMVC_SQL.Data;
using System.Data;
using System.Data.SqlClient;

namespace FirstMVC_SQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var dbConnectionString = builder.Configuration.GetConnectionString("conexionPredeterminada");
            builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

            //inyeccción mi propio dependencia
            builder.Services.AddScoped<IContactsRepository, ContactsRepository>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}