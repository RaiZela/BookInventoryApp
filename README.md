# 📚 BookInventory App

A cross-platform book management application built with .NET MAUI.
Allows users to add, search, categorize, lend, and borrow books, with full tracking of current and historical transactions.

## Features

- Add & Edit Books

- Title, Author(s), Category, Language, and ISBN.

- TODO: Optional cover image upload.

- Search & Filter

- Search books by title, author, category, or language.

- TODO: Filter by availability or lending status.

- Book Lending & Borrowing

- Lend books to friends and track borrowing history.

- TODO:Manage due dates and current borrowers.

- Historical Tracking

- Keep a record of all lending and borrowing transactions.

- Offline Storage

- Uses SQLite (without EF Core) for local persistence.

- Responsive UI

- Built with MAUI layouts and custom popups for a smooth cross-platform experience.

## Screenshots

WORK IN PROGRESS

## Getting Started
### Prerequisites

- .NET 7 SDK or later

- Visual Studio 2022 (or later) with .NET MAUI workload installed

- Android/iOS simulator or a physical device

## Installation

- Clone the repository:

- cd BookInventoryApp


- Restore dependencies:
  
```csharp
dotnet restore
```

- Run the app:

```csharp
dotnet build
dotnet run
```

## Database Setup

- The app uses SQLite. The database file is automatically created at first run.

- Tables include: Books, Users, Friends

Project Structure
BookInventoryApp/
│
├─ App.xaml.cs           # App entry point
├─ MainPage.xaml         # Main UI page
├─ Models/               # Data models (Book, User, LendingHistory)
├─ Views/                # UI pages & popups
├─ ViewModels/           # MVVM logic
├─ Services/             # SQLite service, Lending service, etc.
├─ Resources/            # Images, fonts, styles
└─ Database/             # SQLite setup & migrations

## Usage

- Add a Book: Tap “Add Book” → fill in details → save.

- Lend a Book: Select a book → tap “Lend” → choose borrower → save.

- Search & Filter: Use the search bar or filter options to find specific books.

- View History: Tap a book → view lending history.

## Contributing

- Contributions are welcome! Please fork the repo and submit a pull request.

- Feature requests: Open an issue

- Bug reports: Open an issue with detailed steps

## License

MIT License © [Your Name]
