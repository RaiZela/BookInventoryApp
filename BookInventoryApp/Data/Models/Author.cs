using BookInventoryApp.Data.Models.Abstractions.Generic;

namespace BookInventoryApp.Data.Models;

public class Author : Entity
{

    [StringLength(20)]
    public string Name { get; set; }

    [StringLength(20)]
    public string? MiddleName { get; set; }

    [StringLength(20)]
    public string LastName { get; set; }

    public Guid NationalityId { get; set; }
}
