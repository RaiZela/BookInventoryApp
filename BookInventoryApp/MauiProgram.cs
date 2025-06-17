using BookInventoryApp.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BookInventoryApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit(options => options.SetShouldEnableSnackbarOnWindows(true))
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("VictorMono-Bold.ttf", "VictorMono-Bold");
                fonts.AddFont("VictorMono-BoldItalic.ttf", "VictorMono-BoldItalic");
                fonts.AddFont("VictorMono-ExtraLight.ttf", "VictorMono-ExtraLight");
                fonts.AddFont("VictorMono-ExtraLightItalic.ttf", "VictorMono-ExtraLightItalic");
                fonts.AddFont("VictorMono-Italic.ttf", "VictorMono-Italic");
                fonts.AddFont("VictorMono-Light.ttf", "VictorMono-Light");
                fonts.AddFont("VictorMono-LightItalic.ttf", "VictorMono-LightItalic");
                fonts.AddFont("VictorMono-Medium.ttf", "VictorMono-Medium");
                fonts.AddFont("VictorMono-MediumItalic.ttf", "VictorMono-MediumItalic");
                fonts.AddFont("VictorMono-Regular.ttf", "VictorMono-Regular");
                fonts.AddFont("VictorMono-SemiBold.ttf", "VictorMono-SemiBold");
                fonts.AddFont("VictorMono-SemiBoldItalic.ttf", "VictorMono-SemiBoldItalic");
                fonts.AddFont("VictorMono-Thin.ttf", "VictorMono-Thin");
                fonts.AddFont("VictorMono-ThinItalic.ttf", "VictorMono-ThinItalic");
            });

        builder.Services.AddSingleton<AppShell>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton(s =>
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "books.db3");
            var connection = new SQLiteAsyncConnection(dbPath);
            new BookDatabase(dbPath, connection);
            return connection;
        });

        builder.Services.AddTransient<IAuthorService, AuthorService>();
        builder.Services.AddTransient<IBookService, BookService>();
        builder.Services.AddTransient<ICategoriesService, CategoriesService>();
        builder.Services.AddTransient<ILanguagesService, LanguagesService>();

        return builder.Build();
    }
}
