namespace BookInventoryApp.Services;

public interface ILanguagesService
{
    Task<List<LanguageDTO>> GetLanguagesAsync();
    Task<Language> GetLanguageAsync(Guid id);
    Task<int> SaveLanguageAsync(Language Language);
    Task<int> DeleteLanguageAsync(Guid id);
    Task<IEnumerable<LanguageDTO>> GetFilteredLanguagesAsync(string query);
    Task<IEnumerable<string>> GetBookLanguagesAsync(Guid bookId);
    IEnumerable<LanguageDTO> GetLanguagesById(List<Guid> ids);
}
public class LanguagesService : ILanguagesService
{
    private readonly SQLiteAsyncConnection _connection;
    public LanguagesService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }
    public async Task<List<LanguageDTO>> GetLanguagesAsync()
    {
        var languages = await _connection.Table<Language>()
         .ToListAsync();

        return languages.Select(c => new LanguageDTO
        {
            Id = c.Id,
            Name = c.Name,
            Code = c.Code
        }).ToList();
    }

    public Task<Language> GetLanguageAsync(Guid id) =>
         _connection.Table<Language>().FirstOrDefaultAsync(b => b.Id == id);

    public Task<int> SaveLanguageAsync(Language Language) =>
         _connection.InsertOrReplaceAsync(Language);

    public async Task<int> DeleteLanguageAsync(Guid id)
    {
        var language = await _connection.Table<Language>().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (language is not null)
            return await _connection.DeleteAsync(language);

        return -1;
    }

    public async Task<IEnumerable<LanguageDTO>> GetFilteredLanguagesAsync(string query)
    {
        var languages = await _connection.Table<Language>()
            .Where(c => c.Name.ToLower().Contains(query) || c.Code.ToLower().Contains(query))
            .ToListAsync();

        return languages.Select(c => new LanguageDTO
        {
            Id = c.Id,
            Name = c.Name,
            Code = c.Code
        }).ToList();
    }

    public async Task<IEnumerable<string>> GetBookLanguagesAsync(Guid bookId)
    {
        var languages = (from bookLanguage in await _connection.Table<BookLanguage>().Where(x => x.BookId == bookId).ToListAsync()
                         join language in await _connection.Table<Language>().ToListAsync()
                         on bookLanguage.LanguageId equals language.Id
                         select language.Name).ToList();

        if (languages == null)
            return new List<string>();

        return languages;
    }

    public IEnumerable<LanguageDTO> GetLanguagesById(List<Guid> ids)
    {
        var languages = _connection.Table<Language>()
     .Where(x => ids.Contains(x.Id)).ToListAsync();

        return languages.Result.Select(a => new LanguageDTO
        {
            Id = a.Id,
            Name = a.Name
        }).ToList();
    }
}
