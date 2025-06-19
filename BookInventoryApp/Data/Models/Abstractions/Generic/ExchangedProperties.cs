namespace BookInventoryApp.Data.Models.Abstractions.Generic;

public abstract class ExchangedProperties
{

    public DateTime? ReturnedOn { get; set; } = null;
    public bool IsReturned { get; set; } = false;
    public Guid FriendId { get; set; }
    public DateTime On { get; set; }
}
