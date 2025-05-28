namespace BookInventoryApp.Data.Models.Abstractions.Generic;

public abstract class ExchangedProperties
{
    public Guid BookId { get; set; }
    public DateTime? ReturnedOn { get; set; } = null;
    public bool IsReturned { get; set; } = false;
    public Guid PersonId { get; set; }
    public DateTime On { get; set; }
}
