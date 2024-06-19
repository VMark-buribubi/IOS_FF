using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Net.Http;

namespace FrontendMaui
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Register the HttpClient with the authenticated handler
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("http://localhost:7115/") });
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<CreateStudentViewModel>();
            builder.Services.AddTransient<AddNewStudentPage>();


            return builder.Build();
        }
    }
}
