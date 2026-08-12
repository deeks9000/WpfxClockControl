using Demo_ClockControl.Styles;
using System.Windows;

namespace Demo_ClockControl;

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
