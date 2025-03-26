using System.Text;
using SalesManagement.Domain.Services;
using SalesManagement.UI.Components;
using SalesManagement.UI.Services;


namespace SalesManagement.UI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Registering the CodePagesEncodingProvider ensures that the application can handle various CSV file encodings correctly.
        //The given file uses the encoding Windows-1252.
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddTransient<ISalesManagementService, SalesManagementService>();
        builder.Services.AddTransient<ISalesViewService, SalesViewService>();
        // Register HttpClient
        // builder.Services.AddHttpClient<SalesViewService>(client =>
        // {
        //     client.BaseAddress = new Uri("http://localhost:5165");
        // });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}