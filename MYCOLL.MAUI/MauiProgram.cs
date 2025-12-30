using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MYCOLL.RCL.Data.Interfaces;
using MYCOLL.RCL.Services;

namespace MYCOLL.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            // STEP 1: Register IHttpClientFactory with a named client
            builder.Services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri("https://dxbmlrv7-7077.uks1.devtunnels.ms");
                client.Timeout = TimeSpan.FromSeconds(120);
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            });

            // STEP 2: Register a default HttpClient that uses the factory
            builder.Services.AddScoped<HttpClient>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                return factory.CreateClient("api");
            });

            // Register RCL services
            builder.Services.AddScoped<ProdutoService>();
            builder.Services.AddScoped<CategoriaService>();
            builder.Services.AddScoped<SubCategoriaService>();
            builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();

            // Register Authentication Service
            builder.Services.AddScoped<AuthenticacaoService>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<AuthenticacaoService>());

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}