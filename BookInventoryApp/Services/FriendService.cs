namespace BookInventoryApp.Services;

public interface IFriendService
{
    Task<List<FriendsDTO>> GetFriendsAsync();
    Task<Friend> GetFriendAsync(Guid id);
    Task<int> SaveNewFriendAsync(Friend Friend);
    Task<int> DeleteFriendAsync(Guid id);
    Task<int> UpdateFriendAsync(Friend Friend);
}
public class FriendService : IFriendService
{
    private readonly SQLiteAsyncConnection _connection;
    public FriendService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<FriendsDTO>> GetFriendsAsync()
    {
        var result = await _connection.Table<Friend>().ToListAsync();
        return result.Select(x => new FriendsDTO
        {
            Id = x.Id,
            Name = x.Name,
            LastName = x.LastName,
            Nickname = x.Nickname,
            Address = x.Address,
            LibraryID = x.LibraryID
        }).ToList();
    }

    public Task<Friend> GetFriendAsync(Guid id) =>
         _connection.Table<Friend>().FirstOrDefaultAsync(b => b.Id == id);

    public Task<int> SaveNewFriendAsync(Friend Friend) =>
         _connection.InsertOrReplaceAsync(Friend);

    public async Task<int> UpdateFriendAsync(Friend Friend) =>
     await _connection.UpdateAsync(Friend);

    public async Task<int> DeleteFriendAsync(Guid id)
    {
        var friend = await _connection.Table<Friend>().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (friend is not null)
            return await _connection.DeleteAsync(friend);

        return -1;
    }
}
