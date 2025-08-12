using System.Globalization;

namespace BookInventoryApp.NewFolder;

public class Base64ToImageSource : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string base64Image = (string)value;
        return base64Image.GetImageSourceFromBase64String();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}


public static class GetImageSourceFromBase64StringClass
{
    public static ImageSource GetImageSourceFromBase64String(this string base64)
    {
        if (base64 == null)
        {
            return null;
        }

        byte[] Base64Stream = Convert.FromBase64String(base64);
        return ImageSource.FromStream(() => new MemoryStream(Base64Stream));
    }
}

