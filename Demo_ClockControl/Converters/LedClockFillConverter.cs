using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Demo_ClockControl.Converters;

public class LedClockFillConverter : IValueConverter
{
    public static readonly SolidColorBrush SegmentOffBrush = new SolidColorBrush(Color.FromRgb(40, 40, 40));

    // 5 x 9 seven-segment layout:
    //
    //  AAA
    // F   B
    // F   B
    // F   B
    //  GGG
    // E   C
    // E   C
    // E   C
    //  DDD

    private static readonly string[] Decoder = [
        "ABCDEF",   // 0
        "BC",       // 1
        "ABDEG",    // 2
        "ABCDG",    // 3
        "BCFG",     // 4
        "ACDFG",    // 5
        "ACDEFG",   // 6
        "ABC",      // 7
        "ABCDEFG",  // 8
        "ABCDFG"    // 9
    ];

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not DateTime dateTime)
            return SegmentOffBrush;  

        if (parameter is not LedClockSegment ledSegment)
            return SegmentOffBrush; 

        int digit = ledSegment.Digit switch {
            ClockDigit.UnitSeconds => dateTime.Second % 10,
            ClockDigit.TenSeconds => dateTime.Second / 10,
            ClockDigit.UnitMinutes => dateTime.Minute % 10,
            ClockDigit.TenMinutes => dateTime.Minute / 10,
            ClockDigit.UnitHours => dateTime.Hour % 10,
            ClockDigit.TenHours => dateTime.Hour / 10,
            _ => throw new ArgumentOutOfRangeException()
        };

        bool isSegmentOn = Decoder[digit].Contains(ledSegment.Segment.ToString());

        Brush segmentOnBrush = (ledSegment.Digit == ClockDigit.UnitSeconds || ledSegment.Digit == ClockDigit.TenSeconds)
            ? Brushes.Red
            : Brushes.LawnGreen;

        return isSegmentOn
            ? segmentOnBrush
            : SegmentOffBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
