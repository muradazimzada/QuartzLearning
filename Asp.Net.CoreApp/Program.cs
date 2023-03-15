
using Quartz;
using System.Configuration;
using Npgsql.EntityFrameworkCore;
using Asp.Net.CoreApp.DbContext.ConsoleApp.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Asp.Net.CoreApp.Jobs;

namespace Asp.Net.CoreApp
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BloggingContext>();
            //services.AddIdentity<User>()
            //                .AddEntityFrameworkStores<ApplicationDbContext>()
            //                .AddDefaultTokenProviders();
            // Add services to the container.
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("DemoJob");
                var dumbJobKey = new JobKey("DumbJobKey");
                //q.AddJob<DemoJob>(opts => opts.WithIdentity(jobKey));
                //q.AddTrigger(opts => opts
                //    .ForJob(jobKey)
                //    .WithIdentity("DemoJob-trigger")
                //    .WithCronSchedule("0/5 * * * * ?"));

                q.AddJob<DumbJob>(opts => opts.WithIdentity(dumbJobKey));

                q.AddTrigger(opts => opts.ForJob(dumbJobKey).WithIdentity("myJob", "group1") // name "myJob", group "group1"
                     .UsingJobData("jobSays", "Hello World!")
                     .UsingJobData("myFloatValue", 3.5f).WithIdentity("DemoJob-trigger")
                    .WithCronSchedule("15 * * * * ?")
                     );
            }
            ); ;

            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}