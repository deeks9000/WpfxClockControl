using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfxCustomControls;

namespace Demo_03_GlobalStyle;

public static class Program
{
    [STAThread]
    public static void Main()
    { 
        var app = new Application();                             

        app.Resources.Add(typeof(ClockControl), BuildClockStyle());  // KEY: Type

        var win = new MainWindow();

        app.Run(win);
    }

    private static Style BuildClockStyle()
    {
        var style = StyleX<ClockControl>(
            setters: [
                SetterX(Control.ForegroundProperty, Brushes.Green),
                SetterX(Control.FontWeightProperty, FontWeights.Bold),
                SetterX(TextBlock.FontFamilyProperty, new FontFamily("Courier New")),
                SetterX(TextBlock.FontSizeProperty, 48d),
            ]
        );

        return style;
    }
}
