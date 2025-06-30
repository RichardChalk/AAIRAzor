using Azure.Core;
using Azure.AI.OpenAI;
using System.ClientModel;

namespace AAIRAzor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            // Lägg till DI för min Azure OpenAI service            
            builder.Services.AddSingleton(sp =>
                new AzureOpenAIClient(
                    new Uri("https://systementoropenaiinstance.openai.azure.com/"),
                    new ApiKeyCredential(Environment.GetEnvironmentVariable("AzureOpenAI-Key"))));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
