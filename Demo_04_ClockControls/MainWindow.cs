using Demo_04_ClockControls.Styles;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_04_ClockControls;

public class MainWindow : Window
{
    public MainWindow()
    {
        Title = "Demo 04 ClockControls";
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
                            x.Foreground = Brushes.Magenta;
                            x.FontWeight = FontWeights.Bold;
                            x.FontFamily = new FontFamily("Courier New");
                            x.FontSize = 48;
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
                            x.Style = TryFindResource(LedClockStyle.Name) as Style;
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
                            x.Style = TryFindResource(WeekdayClockStyle.Name) as Style;
                        }
                    )
                ),
            ]
        );
    }
}
