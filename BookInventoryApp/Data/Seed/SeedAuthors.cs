namespace BookInventoryApp.Data.Seed;

public static class SeedAuthors
{
    public static void SeedAuthorsAsync(SQLiteAsyncConnection connection)
    {
        var existing = connection.Table<Author>().ToListAsync().Result;
        if (existing.Any())
            return;

        AuthorSeedingData.SeedAlbanianWriters(connection);
        AuthorSeedingData.SeedFrenchWriters(connection);
        AuthorSeedingData.SeedBritishWriters(connection);
        AuthorSeedingData.SeedAmericanWriters(connection);
        AuthorSeedingData.SeedPersianWriters(connection);
        AuthorSeedingData.SeedRussianWriters(connection);
        AuthorSeedingData.SeedGermanWriters(connection);
        AuthorSeedingData.SeedItalianWriters(connection);
        AuthorSeedingData.SeedSpanishWriters(connection);
        AuthorSeedingData.SeedJapaneseWriters(connection);
        AuthorSeedingData.SeedArabicWriters(connection);
        AuthorSeedingData.SeedAfricanWriters(connection);
        AuthorSeedingData.SeedIndianWriters(connection);
        AuthorSeedingData.SeedTurkishWriters(connection);
        AuthorSeedingData.SeedLatinAmericanWriters(connection);
        AuthorSeedingData.SeedDutchWriters(connection);
        AuthorSeedingData.SeedScandinavianWriters(connection);
        AuthorSeedingData.SeedHungarianWriters(connection);
        AuthorSeedingData.SeedPolishWriters(connection);
        AuthorSeedingData.SeedChineseWriters(connection);
        AuthorSeedingData.SeedCanadianWriters(connection);
        AuthorSeedingData.SeedGreekWriters(connection);

    }
}
