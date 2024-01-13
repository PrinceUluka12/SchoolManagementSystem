using Microsoft.EntityFrameworkCore;
using School.Data;

namespace School.Extensions
{
    public class DatabaseManagement
    {
        public static void MigrationIntilization(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
