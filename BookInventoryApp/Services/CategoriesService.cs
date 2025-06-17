namespace BookInventoryApp.Services;

public interface ICategoriesService
{
    Task<List<CategoryDTO>> GetCategoriesAsync();
    Task<Category> GetCategoryAsync(Guid id);
    Task<int> SaveCategoryAsync(Category category);
    Task<int> DeleteCategoryAsync(Guid id);
    Task<IEnumerable<CategoryDTO>> GetFilteredCategoriesAsync(string query);
    Task<IEnumerable<string>> GetBookCategoriesAsync(Guid bookId);
    IEnumerable<CategoryDTO> GetCategoriesById(List<Guid> ids);
}
public class CategoriesService : ICategoriesService
{
    private readonly SQLiteAsyncConnection _connection;
    public CategoriesService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }
    public async Task<List<CategoryDTO>> GetCategoriesAsync()
    {
        var categories = await _connection.Table<Category>().ToListAsync();

        return categories.Select(c => new CategoryDTO
        {
            Id = c.Id,
            Code = c.Code,
            Name = c.Name
        }).ToList();

    }

    public Task<Category> GetCategoryAsync(Guid id) =>
        _connection.Table<Category>().FirstOrDefaultAsync(c => c.Id == id);

    public Task<int> SaveCategoryAsync(Category category) =>
        _connection.InsertOrReplaceAsync(category);

    public async Task<int> DeleteCategoryAsync(Guid id)
    {
        var category = await _connection.Table<Category>().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (category is not null)
            return await _connection.DeleteAsync(category);

        return -1;
    }

    public async Task<IEnumerable<CategoryDTO>> GetFilteredCategoriesAsync(string query)
    {
        var categories = await _connection.Table<Category>()
            .Where(c => c.Name.ToLower().Contains(query) || c.Code.ToLower().Contains(query))
            .ToListAsync();

        return categories.Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name,
            Code = c.Code
        }).ToList();
    }

    public async Task<IEnumerable<string>> GetBookCategoriesAsync(Guid bookId)
    {
        var categories = (from bookCategory in await _connection.Table<BookCategory>().Where(x => x.BookId == bookId).ToListAsync()
                          join category in await _connection.Table<Category>().ToListAsync()
                          on bookCategory.CategoryId equals category.Id
                          select category.Name).ToList();

        if (categories == null)
            return new List<string>();

        return categories;
    }

    public IEnumerable<CategoryDTO> GetCategoriesById(List<Guid> ids)
    {
        var categories = _connection.Table<Category>()
         .Where(x => ids.Contains(x.Id)).ToListAsync();

        return categories.Result.Select(a => new CategoryDTO
        {
            Id = a.Id,
            Name = a.Name
        }).ToList();
    }
}
