using Demo_ClockControl.Styles;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfxCustomControls;

namespace Demo_ClockControl;

public class MainWindow : Window
{
    public MainWindow()
    {
        Title = "Demo ClockControl";
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = 800;
        Height = 500;
        Content = Build();
    }

    private UIElement Build()
    {
        return GridX(
            configure: x => {
                x.AddRow();
                x.AddRow();
                x.AddRow();
            },
            children: [
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 0);
                        x.Background = Brushes.White;
                    },
                    child: ClockControlX(
                        configure: x => {
                            x.HorizontalAlignment = HorizontalAlignment.Center;
                            x.VerticalAlignment = VerticalAlignment.Center;

                            //x.Foreground = Brushes.Magenta;
                            //x.FontWeight = FontWeights.Bold;
                            //x.FontFamily = new FontFamily("Courier New");
                            //x.FontSize = 42;

                            var style = StyleX<ClockControl>(
                                basedOn: ClockControl.DefaultStyle,
                                setters: [
                                    SetterX(Control.ForegroundProperty, Brushes.Magenta),
                                    SetterX(Control.FontWeightProperty, FontWeights.Bold),
                                    SetterX(TextBlock.FontFamilyProperty, new FontFamily("Courier New")),
                                    SetterX(TextBlock.FontSizeProperty, 42d),
                                ]
                            );

                            x.Style = style;
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 1);
                        x.Background = Brushes.Black;
                    },
                    child: ClockControlX(
                        configure: x => {
                            x.HorizontalAlignment = HorizontalAlignment.Center;
                            x.VerticalAlignment = VerticalAlignment.Center;
                            x.Style = Application.Current.TryFindResource(LedClockStyle.Name) as Style;
                        }
                    )
                ),
                BorderX(
                    configure: x => {
                        Grid.SetRow(x, 2);
                        x.Background = Brushes.LightBlue;
                    },
                    child: ClockControlX(
                        configure: x => {
                            x.HorizontalAlignment = HorizontalAlignment.Center;
                            x.VerticalAlignment = VerticalAlignment.Center;
                            x.Style = Application.Current.TryFindResource(WeekdayClockStyle.Name) as Style;
                        }
                    )
                ),
            ]
        );
    }
}
