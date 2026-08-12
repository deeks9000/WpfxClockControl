using System.Globalization;
using System.Windows.Data;

namespace Demo_ClockControl.Converters;

[ValueConversion(typeof(DateTime), typeof(string))]
public class WeekdayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateTime date = (DateTime)value;
        return date.DayOfWeek.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
