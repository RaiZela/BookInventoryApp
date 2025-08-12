namespace BookInventoryApp.Services;

public interface IFriendService
{
    Task<List<FriendsDTO>> GetFriendsAsync();
    Task<FriendsDTO> GetFriendAsync(Guid id);
    Task<int> SaveNewFriendAsync(FriendsDTO Friend);
    Task<int> DeleteFriendAsync(Guid id);
    Task<int> UpdateFriendAsync(FriendsDTO Friend);
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
            Address = x.Address
        }).ToList();
    }

    public async Task<FriendsDTO> GetFriendAsync(Guid id)
    {
        var friend = await _connection.Table<Friend>().Where(x => x.Id == id).FirstOrDefaultAsync();
        return new FriendsDTO
        {

            Id = friend.Id,
            Name = friend.Name,
            LastName = friend.LastName,
            Nickname = friend.Nickname,
            Address = friend.Address
        };
    }

    public Task<int> SaveNewFriendAsync(FriendsDTO Friend) =>
         _connection.InsertOrReplaceAsync(new Friend
         {
             Id = Friend.Id,
             Name = Friend.Name,
             LastName = Friend.LastName,
             Nickname = Friend.Nickname,
             Address = Friend.Address,
             Image = Friend.Image
         });

    public async Task<int> UpdateFriendAsync(FriendsDTO Friend)
    {
        var update = await _connection.UpdateAsync(new Friend
        {
            Id = Friend.Id,
            Name = Friend.Name,
            LastName = Friend.LastName,
            Nickname = Friend.Nickname,
            Address = Friend.Address,
            Image = Friend.Image
        });
        return update;
    }

    public async Task<int> DeleteFriendAsync(Guid id)
    {
        var friend = await _connection.Table<Friend>().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (friend is not null)
            return await _connection.DeleteAsync(friend);

        return -1;
    }
}
