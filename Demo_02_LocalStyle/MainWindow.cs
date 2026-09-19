using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfxCustomControls;

namespace Demo_02_LocalStyle;

public class MainWindow : Window
{
    public MainWindow()
    {
        Title = "Demo 02 LocalStyle";
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
                            x.Style = BuildLocalStyle();
                        }
                    )
                )
            ]
        );
    }

    private Style BuildLocalStyle()
    {
        var style = StyleX<ClockControl>(
            setters: [
                SetterX(Control.ForegroundProperty, Brushes.DarkOrange),
                SetterX(Control.FontWeightProperty, FontWeights.Bold),
                SetterX(TextBlock.FontFamilyProperty, new FontFamily("Courier New")),
                SetterX(TextBlock.FontSizeProperty, 48d),
            ]
        );

        return style;
    }
}
