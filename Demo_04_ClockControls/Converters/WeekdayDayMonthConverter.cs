using System.Globalization;
using System.Windows.Data;

namespace Demo_04_ClockControls.Converters;

[ValueConversion(typeof(DateTime), typeof(string))]
public class WeekdayDayMonthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
            return date.ToString("dddd d MMMM", culture);

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
