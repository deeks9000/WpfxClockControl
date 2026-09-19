using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Demo_04_ClockControls.Converters;
using WpfxCustomControls;

namespace Demo_04_ClockControls.Styles;

public static class WeekdayClockStyle
{
    public static string Name = nameof(WeekdayClockStyle);

    public static Style Build()
    {
        var visualTree = FrameworkElementFactoryX<StackPanel>(
            name: "PART_Root",
            setters: [
                SetterX(Panel.BackgroundProperty, TemplateBindingX(Panel.BackgroundProperty))
            ],
            children: [
                FrameworkElementFactoryX<TextBlock>(
                    name: "PART_Weekday",
                    setters: [
                        SetterX(TextBlock.TextProperty, BindingX(b => {
                            b.Path = PropertyPathX(nameof(ClockControl.Timestamp));
                            b.RelativeSource = RelativeSourceX(RelativeSourceMode.TemplatedParent);
                            b.Converter = new WeekdayDayMonthConverter();
                            //b.Converter = new WeekdayConverter();
                        }))
                    ]
                )
            ]
        );

        var controlTemplate = ControlTemplateX<ClockControl>(visualTree);

        var style = StyleX<ClockControl>(
            setters: [
                SetterX(FrameworkElement.MarginProperty, ThicknessX(10)),
                SetterX(TextBlock.ForegroundProperty, Brushes.Purple),
                SetterX(TextBlock.FontFamilyProperty, new FontFamily("Courier New")),
                SetterX(TextBlock.FontStyleProperty, FontStyles.Italic),
                SetterX(TextBlock.FontWeightProperty, FontWeights.SemiBold),
                SetterX(TextBlock.FontSizeProperty, 48d),
                SetterX(Control.TemplateProperty, controlTemplate),
            ]
        );

        return style;
    }
}
