using System.Collections.ObjectModel;
using System.Globalization;

namespace Solution.DesktopApp.Converters;

public class TotalPriceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ObservableCollection<BillItemModel> items)
        {
            return items.Sum(i => i.TotalPrice);
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
