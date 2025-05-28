using BookInventoryApp.Data.Models.Abstractions.Generic;

namespace BookInventoryApp.Data.Models;

public class Person : Entity
{

    [StringLength(20)]
    public string Name { get; set; }

    [StringLength(20)]
    public string LastName { get; set; }

    [StringLength(200)]
    public string Address { get; set; }
}
