namespace BookInventoryApp.Data.Models.Abstractions.Generic;

public abstract class MinimalConfig : Entity
{
    [StringLength(4)]
    public string Code { get; set; }

    [StringLength(20)]
    public string Name { get; set; }
}
