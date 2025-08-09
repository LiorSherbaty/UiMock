

namespace UiTesterDemo.Converters
{
    // You need to add this simple converter to your project as well.
    // It's used to invert the IsLoading boolean for the button's IsEnabled property.
    public class NotConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
