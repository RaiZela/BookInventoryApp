using BookInventoryApp.Data.Seed;

namespace BookInventoryApp;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AppShell _appShell;

    public App(IServiceProvider serviceProvider, AppShell appShell)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _appShell = appShell;

        var connection = _serviceProvider.GetRequiredService<SQLiteAsyncConnection>();

        //connection.DeleteAllAsync<Book>().Wait();
        //connection.DeleteAllAsync<Author>().Wait();
        //connection.DeleteAllAsync<Category>().Wait();
        //connection.DeleteAllAsync<Friend>().Wait();
        //connection.DeleteAllAsync<Language>().Wait();

        if (connection.Table<Author>().CountAsync().Result == 0)
            SeedAuthors.SeedAuthorsAsync(connection);
        if (connection.Table<Category>().CountAsync().Result == 0)
            SeedCategories.SeedCategoriesAsync(connection);
        if (connection.Table<Language>().CountAsync().Result == 0)
            SeedLanguages.SeedLanguagesAsync(connection);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(_appShell);
    }
}
