using Demo_04_ClockControls.Models;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Demo_04_ClockControls.Converters;

public class LedClockFillConverter : IValueConverter
{
    public static readonly SolidColorBrush SegmentOffBrush = new SolidColorBrush(Color.FromRgb(32, 32, 32));

    public static readonly SolidColorBrush SecondsSegmentOnBrush = Brushes.Red;

    public static readonly SolidColorBrush MinutesSegmentOnBrush = Brushes.LawnGreen;

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

        if (parameter is not SegmentModel segmentModel)
            return SegmentOffBrush;

        int digit = segmentModel.Unit switch {
            ClockUnit.OneSeconds => dateTime.Second % 10,
            ClockUnit.TenSeconds => dateTime.Second / 10,
            ClockUnit.OneMinutes => dateTime.Minute % 10,
            ClockUnit.TenMinutes => dateTime.Minute / 10,
            ClockUnit.OneHours => dateTime.Hour % 10,
            ClockUnit.TenHours => dateTime.Hour / 10,
            _ => throw new ArgumentOutOfRangeException()
        };

        bool isSegmentOn = Decoder[digit].Contains(segmentModel.Segment.ToString());

        if (!isSegmentOn)
            return SegmentOffBrush;

        bool isSeconds = (segmentModel.Unit == ClockUnit.OneSeconds || segmentModel.Unit == ClockUnit.TenSeconds);

        return isSeconds
            ? SecondsSegmentOnBrush
            : MinutesSegmentOnBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
