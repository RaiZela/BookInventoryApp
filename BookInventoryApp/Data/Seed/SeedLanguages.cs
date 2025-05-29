namespace BookInventoryApp.Data.Seed;

public static class SeedLanguages
{
    public static void SeedLanguagesAsync(SQLiteAsyncConnection connection)
    {
        var existing = connection.Table<Language>().ToListAsync().Result;
        if (existing.Any())
            return;

        var languages = new List<Language>
{
    new Language { Id = Guid.NewGuid(), Code = "sq", Name = "Albanian" },
    new Language { Id = Guid.NewGuid(), Code = "en", Name = "English" },
    new Language { Id = Guid.NewGuid(), Code = "fr", Name = "French" },
    new Language { Id = Guid.NewGuid(), Code = "ru", Name = "Russian" },
    new Language { Id = Guid.NewGuid(), Code = "de", Name = "German" },
    new Language { Id = Guid.NewGuid(), Code = "it", Name = "Italian" },
    new Language { Id = Guid.NewGuid(), Code = "es", Name = "Spanish" },
    new Language { Id = Guid.NewGuid(), Code = "pt", Name = "Portuguese" },
    new Language { Id = Guid.NewGuid(), Code = "tr", Name = "Turkish" },
    new Language { Id = Guid.NewGuid(), Code = "zh", Name = "Chinese" },
    new Language { Id = Guid.NewGuid(), Code = "ja", Name = "Japanese" },
    new Language { Id = Guid.NewGuid(), Code = "ko", Name = "Korean" },
    new Language { Id = Guid.NewGuid(), Code = "ar", Name = "Arabic" },
    new Language { Id = Guid.NewGuid(), Code = "hi", Name = "Hindi" },
    new Language { Id = Guid.NewGuid(), Code = "fa", Name = "Persian" },
    new Language { Id = Guid.NewGuid(), Code = "el", Name = "Greek" },
    new Language { Id = Guid.NewGuid(), Code = "pl", Name = "Polish" },
    new Language { Id = Guid.NewGuid(), Code = "cs", Name = "Czech" },
    new Language { Id = Guid.NewGuid(), Code = "ro", Name = "Romanian" },
    new Language { Id = Guid.NewGuid(), Code = "ur", Name = "Urdu" }
};

        connection.InsertAllAsync(languages).Wait();
    }
}
