namespace BookInventoryApp.Services;

public interface ICategoriesService
{
    Task<List<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryAsync(Guid id);
    Task<int> SaveCategoryAsync(Category category);
    Task<int> DeleteCategoryAsync(Category category);
}
public class CategoriesService : ICategoriesService
{
    private readonly SQLiteAsyncConnection _connection;
    public CategoriesService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }
    public Task<List<Category>> GetCategoriesAsync() =>
        _connection.Table<Category>().ToListAsync();

    public Task<Category> GetCategoryAsync(Guid id) =>
        _connection.Table<Category>().FirstOrDefaultAsync(c => c.Id == id);

    public Task<int> SaveCategoryAsync(Category category) =>
        _connection.InsertOrReplaceAsync(category);

    public Task<int> DeleteCategoryAsync(Category category) =>
        _connection.DeleteAsync(category);
}
