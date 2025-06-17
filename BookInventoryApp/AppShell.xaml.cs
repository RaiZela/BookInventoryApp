using BookInventoryApp.Services;

namespace BookInventoryApp
{
    public partial class AppShell : Shell
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        private readonly ICategoriesService _categoriesService;
        private readonly ILanguagesService _languageService;
        public AppShell(IBookService bookService, ICategoriesService categoriesSewrvice, ILanguagesService languageService, IAuthorService authorService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _categoriesService = categoriesSewrvice;
            _languageService = languageService;
            InitializeComponent();
            Routing.RegisterRoute("BookList", typeof(BookList));

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Items.Add(new ShellContent
                {
                    Title = "Book List",
                    ContentTemplate = new DataTemplate(() => new BookListAndroid(_bookService, _authorService, _categoriesService, _languageService)),
                    Route = "BookList"
                });
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                Items.Add(new ShellContent
                {
                    Title = "Book List",
                    ContentTemplate = new DataTemplate(() => new BookList(_bookService, _authorService, _categoriesService, _languageService)),
                    Route = "BookList"
                });
            }
            else
            {
                Items.Add(new ShellContent
                {
                    Title = "Book List",
                    ContentTemplate = new DataTemplate(() => new BookListAndroid(_bookService, _authorService, _categoriesService, _languageService)),
                    Route = "BookList"
                });
            }


        }

    }
}
