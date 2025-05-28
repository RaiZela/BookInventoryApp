namespace BookInventoryApp.Data.Models.Abstractions.Generic;

public abstract class Entity
{
    private readonly SQLiteAsyncConnection _connection;
    protected Entity(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }
    protected Entity()
    {

    }
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();
}
