using System.Text;
using SalesManagement.Domain.Services;

namespace SalesManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Registering the CodePagesEncodingProvider ensures that the application can handle various CSV file encodings correctly.
            //The given file uses the encoding Windows-1252.
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<ISalesManagementService, SalesManagementService>();
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
