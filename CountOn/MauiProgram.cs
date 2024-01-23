using DBManager.Context;
using DBManager.Service;
using Service.Repository;

namespace CountOn;

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

		builder.Services.AddSingleton<IDbContext, DbContext>();
		builder.Services.AddScoped(typeof(IDbService<>), typeof(DbService<>));
		builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		builder.Services.AddScoped<IBillRepository, BillRepository>();

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        return builder.Build();
	}
}
