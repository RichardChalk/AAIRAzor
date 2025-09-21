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

            // L�gg till DI f�r min Azure OpenAI service            

            // 'Singleton' �r r�tt h�r:
            // AzureOpenAIClient �r 'tr�ds�ker*' och gjorda f�r att �teranv�ndas,
            // vilket sparar resurser och �teranv�nder HTTP-anslutningar
            // ist�llet f�r att skapa nya hela tiden.

            // *Tr�ds�ker betyder att flera tr�dar kan anv�nda samma instans samtidigt utan problem.
            // tex: Om tv� anv�ndare samtidigt skickar f�rfr�gningar till din
            // webapp och b�da anv�nder samma AzureOpenAIClient,
            // s� kommer den att hantera b�da parallellt utan problem.
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
