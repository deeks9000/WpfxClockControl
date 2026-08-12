using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Demo_ClockControl.Converters;
using WpfxCustomControls;

namespace Demo_ClockControl.Styles;

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
                    name: "PART_Timestamp",
                    setters: [
                        SetterX(FrameworkElement.MarginProperty, ThicknessX(10)),
                        SetterX(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                        SetterX(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center),
                        SetterX(TextBlock.ForegroundProperty, Brushes.DodgerBlue),
                        SetterX(TextBlock.FontFamilyProperty, new FontFamily("Courier New")),
                        SetterX(TextBlock.FontSizeProperty, 42d),
                        SetterX(TextBlock.TextProperty, BindingX(b => {
                            b.Path = new PropertyPath(nameof(ClockControl.Timestamp));
                            b.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
                            b.StringFormat = "dd/MM/yyyy HH:mm:ss";
                        }))
                    ]
                ),
                FrameworkElementFactoryX<TextBlock>(
                    name: "PART_Weekday",
                    setters: [
                        SetterX(FrameworkElement.MarginProperty, ThicknessX(10)),
                        SetterX(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                        SetterX(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center),
                        SetterX(TextBlock.ForegroundProperty, Brushes.Purple),
                        SetterX(TextBlock.FontFamilyProperty, new FontFamily("Courier New")),
                        SetterX(TextBlock.FontStyleProperty, FontStyles.Italic),
                        SetterX(TextBlock.FontWeightProperty, FontWeights.SemiBold),
                        SetterX(TextBlock.FontSizeProperty, 36d),
                        SetterX(TextBlock.TextProperty, BindingX(b => {
                            b.Path = new PropertyPath(nameof(ClockControl.Timestamp));
                            b.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
                            b.Converter = new WeekdayConverter();
                        }))
                    ]
                )
            ]
        );

        var template = ControlTemplateX<ClockControl>(visualTree);

        var style = StyleX<ClockControl>(
            setters: [
                SetterX(Control.TemplateProperty, template),
            ]
        );

        return style;
    }
}
