namespace BookInventoryApp.Data.Models;

public class Library
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Address { get; set; }
    public Guid FriendId { get; set; } = Guid.Empty; // Default to empty Guid if not set
}
