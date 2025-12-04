using System.Globalization;

namespace Solution.DesktopApp.Converters;

public class BoolToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue && parameter is string text)
        {
            var texts = text.Split('|');
            if (texts.Length == 2)
            {
                return boolValue ? texts[0] : texts[1];
            }
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
