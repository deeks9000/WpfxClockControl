using Demo_04_ClockControls.Styles;
using System.Windows;

namespace Demo_04_ClockControls;

public static class Program
{
    [STAThread]
    public static void Main()
    { 
        var app = new Application();

        var dict = new ResourceDictionary();
        dict.Add(WeekdayClockStyle.Name, WeekdayClockStyle.Build());
        dict.Add(LedClockStyle.Name, LedClockStyle.Build());

        app.Resources = dict;

        var win = new MainWindow();

        app.Run(win);
    }
}
