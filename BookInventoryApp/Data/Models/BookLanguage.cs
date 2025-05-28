namespace BookInventoryApp.Data.Models;
public class BookLanguage
{
    [Indexed(Name = "IX_BookLanguage_Composite", Order = 1, Unique = true)]
    public Guid BookId { get; set; }

    [Indexed(Name = "IX_BookLanguage_Composite", Order = 2, Unique = true)]
    public Guid LanguageId { get; set; }
}
