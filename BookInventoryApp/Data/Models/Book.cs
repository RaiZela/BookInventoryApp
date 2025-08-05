
namespace BookInventoryApp.Data.Models;

public class Book : Entity
{

    [StringLength(200)]
    public string Title { get; set; }
    public Status Status { get; set; } = Status.Unread;
    public BookType Type { get; set; } = BookType.Paperback;
    public bool IsRead { get; set; } = false;
    public int NrOfCopies { get; set; } = 1;
    public string Description { get; set; }
    public string CoverImg { get; set; }
    public string Adress { get; set; }
}

