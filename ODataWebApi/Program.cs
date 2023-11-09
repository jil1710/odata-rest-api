
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataWebApi.AppContextDb;
using ODataWebApi.Models;
using ODataWebApi.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ODataWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            builder.Services.AddDbContext<DemoContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();        

            builder.Services.AddControllers()
                            .AddOData(options => options
                                .Select()
                                .Filter()
                                .OrderBy()
                                .SetMaxTop(20)
                                .Count()
                                .Expand()
                                .AddRouteComponents("odata",GetEdmModel())
                            );


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

        static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<Employee>("EmployeeDept");
            return builder.GetEdmModel();
        }
    }
}