namespace BookInventoryApp.Data.Services;
public class PersonService
{
    private readonly SQLiteAsyncConnection _connection;
    public PersonService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public Task<List<Person>> GetPersonsAsync() =>
        _connection.Table<Person>().ToListAsync();

    public Task<Person> GetPersonAsync(Guid id) =>
         _connection.Table<Person>().FirstOrDefaultAsync(b => b.Id == id);

    public Task<int> SavePersonAsync(Person Person) =>
         _connection.InsertOrReplaceAsync(Person);

    public Task<int> DeletePersonAsync(Person Person) =>
        _connection.DeleteAsync(Person);
}
