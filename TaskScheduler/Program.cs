using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TaskScheduler.Core.Interfaces.Base;
using TaskScheduler.Manager.Managers;
using TaskScheduler.Provider.Implementation;
using TaskScheduler.Manager.Interfaces;
using TaskScheduler.Core.Profiles;
using TaskScheduler.Code.Middleware;
using FluentValidation;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Code.Validators;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TaskScheduler.Code.Swagger;

namespace TaskScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            //db settings
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
           
            var connection = $"Server={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Pooling=true;MultipleActiveResultSets=true;Encrypt=false";
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ICommitProvider, CommitProvider>();

            builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IWorkTaskManager, WorkTaskManager>();
            builder.Services.AddScoped<IPriorityManager, PriorityManager>();

            builder.Services.AddScoped<IValidator<UserCreateUpdateModel>, UserValidator>();
            builder.Services.AddScoped<IValidator<WorkTaskCreateUpdateModel>, WorkTaskValidator>();
            builder.Services.AddScoped<IValidator<PriorityCreateUpdateModel>, PriorityValidator>();
            builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                var info = new OpenApiInfo
                {
                    Title = "TaskSchedulerApi",
                    Version = "v1"
                };
                option.SwaggerDoc("v1", info);
                option.SchemaFilter<EnumSchemaFilter>();
            });

            var app = builder.Build();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
