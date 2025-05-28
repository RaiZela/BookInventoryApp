namespace BookInventoryApp.Data.Services;

public interface ILanguagesService
{
    Task<List<Language>> GetLanguageAsync();
    Task<Language> GetLanguageAsync(Guid id);
    Task<int> SaveLanguageAsync(Language Language);
    Task<int> DeleteLanguageAsync(Language Language);
}
public class LanguagesService : ILanguagesService
{
    private readonly SQLiteAsyncConnection _connection;
    public LanguagesService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }
    public Task<List<Language>> GetLanguageAsync() =>
         _connection.Table<Language>().ToListAsync();

    public Task<Language> GetLanguageAsync(Guid id) =>
         _connection.Table<Language>().FirstOrDefaultAsync(b => b.Id == id);

    public Task<int> SaveLanguageAsync(Language Language) =>
         _connection.InsertOrReplaceAsync(Language);

    public Task<int> DeleteLanguageAsync(Language Language) =>
        _connection.DeleteAsync(Language);
}
