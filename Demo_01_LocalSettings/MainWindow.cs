using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo_01_LocalSettings;

public class MainWindow : Window
{
    public MainWindow()
    {
        Title = "Demo 01 LocalSettings";
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
                            x.Foreground = Brushes.CornflowerBlue;
                            x.FontWeight = FontWeights.Bold;
                            x.FontFamily = new FontFamily("Courier New");
                            x.FontSize = 48;
                        }
                    )
                )
            ]
        );
    }
}
