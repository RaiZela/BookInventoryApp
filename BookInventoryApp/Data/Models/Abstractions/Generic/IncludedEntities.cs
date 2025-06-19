using System.Reflection;


namespace BookInventoryApp.Data.Models.Abstractions.Generic;

public static class IncludedEntities
{
    //This class imitates the "Include" Linq of Entity Framework Core.
    //Using EF Core in such a small project would be overkill. But this specific functionality is needed.
    //Even though, this can be seen more as an experiment on the side of this programmer.


    //This method will return a query that includes the destination entity based on the source entity.
    //The query will be built based on the properties of the source entity and the destination entity.
    //The query will be built using reflection to get the properties of the source entity and the destination entity.
    //The query will be built using the name of the property in the source entity that contains the name of the destination entity.
    //The query will be built using the name of the property in the destination entity that contains the name of the source entity.
    //The query will be built using the name of the property in the source entity that contains the name of the destination entity.
    //The query will be built using the name of the property in the destination entity that contains the name of the source entity.
    public static async Task<IQueryable> Include<S, D>(this SQLiteAsyncConnection connection)
        where S : class, new() //SOURCE ENTITY
        where D : class, new() //DESTINATION ENTITY
    {
        var destinationName = typeof(D).Name;

        //check if the tables exist in the database
        var exists = await TablesExistAsync(connection, typeof(S).Name, destinationName);
        if (exists != null)
            throw new ArgumentException(exists);


        var sourceProperties = typeof(S).GetProperties();
        var propertyNames = sourceProperties.Select(x => x.Name).ToList();

        PropertyInfo? property = null;
        if (propertyNames.Any(x => x.Contains(destinationName)))
            property = sourceProperties.FirstOrDefault(x => x.Name.Contains(destinationName));

        if (property == null)
            throw new ArgumentException($"The source entity {typeof(S).Name} does not contain a property that includes the destination entity {destinationName}.");





        return null;
    }

    public static async Task<string> TablesExistAsync(this SQLiteAsyncConnection connection, string table1, string table2)
    {
        var query = @"
            SELECT name FROM sqlite_master 
            WHERE type = 'table' 
              AND name IN (?, ?);";

        var result = await connection.QueryAsync<TableNameResult>(query, table1, table2);
        var found = result.Select(r => r.Name).ToHashSet();

        if (!found.Contains(table1) && !found.Contains(table1))
        {
            return $"Tables {table1} && {table2} don't exist";
        }
        else if (!found.Contains(table1))
        {
            return $"Table {table1} doesn't exist";
        }
        else if (!found.Contains(table2))
        {
            return $"Table {table2} doesn't exist";
        }

        return null;
    }


    private class TableNameResult
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
