namespace BookInventoryApp.Data.Models;

public class BookCategory
{

    [Indexed(Name = "IX_BookCategory_Composite", Order = 1, Unique = true)]
    public Guid BookId { get; set; }

    [Indexed(Name = "IX_BookCategory_Composite", Order = 2, Unique = true)]
    public Guid CategoryId { get; set; }
}
