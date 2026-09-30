using Demo.Services;
using Microsoft.Extensions.Logging;

namespace Demo;

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

	// Системный сервис MAUI
	builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
	
	// Ваш созданный сервис сети
	builder.Services.AddSingleton<INetworkService, NetworkService>();
	
	// ViewModel и Страница
	builder.Services.AddTransient<MainViewModel>();
			builder.Services.AddTransient<MainPage>();

		return builder.Build();
	}
}
