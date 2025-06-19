namespace BookInventoryApp.Data.Messages;

public static class Messages
{
    public static class Errors
    {
        public const string NotFound = "The requested item was not found.";
        public const string InvalidInput = "The input provided is invalid.";
        public const string DatabaseError = "An error occurred while accessing the database.";
        public const string UnauthorizedAccess = "You do not have permission to access this resource.";

    }
    public static class Success
    {
        public const string ItemAdded = "Item successfully added.";
        public const string ItemUpdated = "Item successfully updated.";
        public const string ItemDeleted = "Item successfully deleted.";
    }
}
