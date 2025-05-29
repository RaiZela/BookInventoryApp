namespace BookInventoryApp.Data;

public static class SeedCategories
{
    public static void SeedCategoriesAsync(SQLiteAsyncConnection connection)
    {
        var existing = connection.Table<Category>().ToListAsync().Result;
        if (existing.Any())
            return;

        var categories = new List<Category>
    {
        // High-level categories
        new Category { Id = Guid.NewGuid(), Code = "FIC", Name = "Fiction" },
        new Category { Id = Guid.NewGuid(), Code = "NF", Name = "Non-Fiction" },
        new Category { Id = Guid.NewGuid(), Code = "SCI", Name = "Science" },
        new Category { Id = Guid.NewGuid(), Code = "HIS", Name = "History" },
        new Category { Id = Guid.NewGuid(), Code = "TECH", Name = "Technology" },
        new Category { Id = Guid.NewGuid(), Code = "ART", Name = "Art & Design" },
        new Category { Id = Guid.NewGuid(), Code = "PHIL", Name = "Philosophy" },
        new Category { Id = Guid.NewGuid(), Code = "PSY", Name = "Psychology" },
        new Category { Id = Guid.NewGuid(), Code = "REL", Name = "Religion" },

        // Genres (treated as categories)
        new Category { Id = Guid.NewGuid(), Code = "FAN", Name = "Fantasy" },
        new Category { Id = Guid.NewGuid(), Code = "SCI-FI", Name = "Science Fiction" },
        new Category { Id = Guid.NewGuid(), Code = "ROM", Name = "Romance" },
        new Category { Id = Guid.NewGuid(), Code = "THR", Name = "Thriller" },
        new Category { Id = Guid.NewGuid(), Code = "MYS", Name = "Mystery" },
        new Category { Id = Guid.NewGuid(), Code = "HOR", Name = "Horror" },
        new Category { Id = Guid.NewGuid(), Code = "HIST-FIC", Name = "Historical Fiction" },
        new Category { Id = Guid.NewGuid(), Code = "DYST", Name = "Dystopian" },
        new Category { Id = Guid.NewGuid(), Code = "ADV", Name = "Adventure" },
        new Category { Id = Guid.NewGuid(), Code = "YA", Name = "Young Adult" },
        new Category { Id = Guid.NewGuid(), Code = "POE", Name = "Poetry" }
    };

        connection.InsertAllAsync(categories).Wait();
    }

}
