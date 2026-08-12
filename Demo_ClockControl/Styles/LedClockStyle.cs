using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Demo_ClockControl.Converters;
using WpfxCustomControls;

namespace Demo_ClockControl.Styles;

public static class LedClockStyle
{
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

    private const int PixelSize = 10;

    // Each seven-segment digit occupies 5 x 9 logical pixels
    private const int DigitWidth = 5;
    private const int DigitHeight = 9;

    // The colon occupies 1 logical pixel
    private const int ColonWidth = 1;

    // There are 7 one-pixel gaps:
    // 5 + 1 + 5 + 1 + :1 + 1 + 5 + 1 + 5 + 1 + :1 + 1 + 5 + 1 + 5
    private const int GapCount = 7;

    private const int DigitCount = 6;
    private const int ColonCount = 2;

    private const int DisplayWidthPixels = (DigitCount * DigitWidth) + (ColonCount * ColonWidth) + GapCount;
    private const int DisplayHeightPixels = DigitHeight;

    public static string Name = nameof(LedClockStyle);

    public static Style Build()
    {
        var visualTree = FrameworkElementFactoryX<Canvas>(
            name: "PART_Root",
            setters: [
                SetterX(StackPanel.BackgroundProperty, Brushes.Black),
                SetterX(Canvas.WidthProperty, (double)(DisplayWidthPixels * PixelSize)),
                SetterX(Canvas.HeightProperty, (double)(DisplayHeightPixels * PixelSize)),
            ],
            children: BuildClockDisplay()
        );

        var template = ControlTemplateX<ClockControl>(visualTree);

        var style = StyleX<ClockControl>(
            setters: [
                SetterX(Control.TemplateProperty, template),
            ]
        );               

        return style;
    }

    private static FrameworkElementFactory[] BuildClockDisplay()
    {
        var children = new List<FrameworkElementFactory>();

        // ---- HOURS ----
        children.AddRange(BuildDigit(ClockDigit.TenHours, x: 0));
        children.AddRange(BuildDigit(ClockDigit.UnitHours, x: 60));

        // ---- HOURS COLON ----
        children.Add(BuildColon(x: 120));

        // ---- MINUTES ----
        children.AddRange(BuildDigit(ClockDigit.TenMinutes, x: 140));
        children.AddRange(BuildDigit(ClockDigit.UnitMinutes, x: 200));

        // --- SECONDS COLON ----
        children.Add(BuildColon(x: 260, isSeconds: true));

        // ---- SECONDS ----
        children.AddRange(BuildDigit(ClockDigit.TenSeconds, x: 280));
        children.AddRange(BuildDigit(ClockDigit.UnitSeconds, x: 340));

        return children.ToArray();
    }

    private static FrameworkElementFactory[] BuildDigit(ClockDigit digit, int x)
    {
        return [
            // [A] Top
            BuildSegment(digit, LedSegment.A, x + PixelSize, 0, width: 3 * PixelSize, height: PixelSize),

            // [B] Upper Right
            BuildSegment(digit, LedSegment.B, x + 4 * PixelSize, PixelSize, width: PixelSize, height: 3 * PixelSize),

            // [C] Lower Right
            BuildSegment(digit, LedSegment.C, x + 4 * PixelSize, 5 * PixelSize, width: PixelSize, height: 3 * PixelSize),

            // [D] Bottom
            BuildSegment(digit, LedSegment.D, x + PixelSize, 8 * PixelSize, width: 3 * PixelSize, height: PixelSize),

            // [E] Lower Left
            BuildSegment(digit, LedSegment.E, x, 5 * PixelSize, width: PixelSize, height: 3 * PixelSize),

            // [F] Upper Left
            BuildSegment(digit, LedSegment.F, x, PixelSize, width: PixelSize, height: 3 * PixelSize),

            // [G] Middle
            BuildSegment(digit, LedSegment.G, x + PixelSize, 4 * PixelSize, width: 3 * PixelSize, height: PixelSize),
        ];
    }

    private static FrameworkElementFactory BuildSegment(ClockDigit digit, LedSegment segment, int x, int y, int width, int height)
    {
        return FrameworkElementFactoryX<Rectangle>(
            setters: [
                SetterX(Canvas.LeftProperty, (double)x),
                SetterX(Canvas.TopProperty, (double)y),
                SetterX(Rectangle.WidthProperty, (double)width),
                SetterX(Rectangle.HeightProperty, (double)height),
                SetterX(Rectangle.SnapsToDevicePixelsProperty, true),
                SetterX(Rectangle.FillProperty, BindingX(b => {
                    b.Path = new PropertyPath(nameof(ClockControl.Timestamp));
                    b.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
                    b.Converter = new LedClockFillConverter();
                    b.ConverterParameter = new LedClockSegment(digit, segment);
                }))
            ]
        );
    }

    private static FrameworkElementFactory BuildColon(int x, bool isSeconds = false)
    {
        return FrameworkElementFactoryX<Canvas>(
            children: [
                BuildColonPixel(x, y: 2 * PixelSize, isSeconds),
                BuildColonPixel(x, y: 6 * PixelSize, isSeconds),
            ]
        );
    }
     
    private static FrameworkElementFactory BuildColonPixel(int x, int y, bool isSeconds = false)
    {
        return FrameworkElementFactoryX<Rectangle>(
            setters: [
                SetterX(Canvas.LeftProperty, (double)x),
                SetterX(Canvas.TopProperty, (double)y),
                SetterX(Rectangle.WidthProperty, (double)PixelSize),
                SetterX(Rectangle.HeightProperty, (double)PixelSize),
                SetterX(Rectangle.SnapsToDevicePixelsProperty, true),
                SetterX(Rectangle.FillProperty, isSeconds ? Brushes.Red : Brushes.LawnGreen),
            ]
        );
    }
}
