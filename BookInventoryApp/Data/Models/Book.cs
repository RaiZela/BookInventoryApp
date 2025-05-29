
namespace BookInventoryApp.Data.Models;

public class Book : Entity
{

    [StringLength(200)]
    public string Title { get; set; }
    public int YearPublished { get; set; }
    public bool IsRead { get; set; } = false;
    public bool IsBorrowed { get; set; } = false;
    public bool IsLended { get; set; } = false;
    public bool IsOwned { get; set; } = true;
    public Status Status { get; set; } = Status.Unread;
    public BookType Type { get; set; } = BookType.Paperback;
}

