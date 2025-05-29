using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class AddCategoryPopup : Popup
{
    ICategoriesService _service;
    public AddCategoryPopup(ICategoriesService service)
    {
        InitializeComponent();
        _service = service;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var category = new Category
        {
            Code = Code.Text,
            Name = Name.Text,
        };
        await _service.SaveCategoryAsync(category);
        Close();
    }
}