using Client.MVVM.Model;
using Client.MVVM.View;
using Client.MVVM.View.Town;
using Client.MVVM.ViewModel;
using Client.MVVM.ViewModel.Town;
using Client.Services;
using Client.Services.IServices;
using Microsoft.Extensions.Logging;

namespace Client
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

            builder.Services.AddTransient<NewPage1>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            builder.Services.AddTransient<TownPage>();
            builder.Services.AddTransient<TownViewModel>();

            builder.Services.AddTransient<GladePage>();
            builder.Services.AddTransient<GladeViewModel>();

            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<RegistrationViewModel>();

            builder.Services.AddHttpClient<IAuthService, AuthService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            builder.Services.AddSingleton<UserStore>();
            builder.Services.AddTransient<Router>();
            builder.Services.AddTransient<TravelService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
