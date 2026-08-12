using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace WpfxCustomControls;

public class ClockControl : Control
{
    private readonly DispatcherTimer _timer = new DispatcherTimer();

    //------------------------------------------------------------------------------
    // Static Constructor

    static ClockControl()
    {     
        var style = BuildDefaultStyle();

        StyleProperty.OverrideMetadata(typeof(ClockControl), new FrameworkPropertyMetadata(style));
    }

    private static Style BuildDefaultStyle()
    {
        var visualTree = FrameworkElementFactoryX<TextBlock>(
            name: "PART_Root",
            setters: [
                SetterX(TextBlock.FontSizeProperty, 24d),
                SetterX(TextBlock.TextProperty, BindingX(b => {
                    b.Path = new PropertyPath(nameof(ClockControl.Timestamp));
                    b.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
                    b.StringFormat = "dd/MM/yyyy HH:mm:ss";
                }))
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

    //------------------------------------------------------------------------------
    // Instance Constructor

    public ClockControl()
    {
        // NOTE: No explicit unsubscribes required
        _timer.Tick += Timer_Tick;
        Loaded += ClockControl_Loaded;
        Unloaded += ClockControl_Unloaded;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        Timestamp = DateTime.Now;

        _timer.Interval = TimeSpan.FromMilliseconds(1000 - Timestamp.Millisecond);
    }
    
    private void ClockControl_Loaded(object sender, RoutedEventArgs e)
    {
        Timestamp = DateTime.Now;

        _timer.Interval = TimeSpan.FromMilliseconds(1000 - Timestamp.Millisecond);
        _timer.Start();
    }

    private void ClockControl_Unloaded(object sender, RoutedEventArgs e)
    {
        _timer.Stop();
    }

    //------------------------------------------------------------------------------
    // Properties

    // DependencyProperty
    public static readonly DependencyProperty TimestampProperty = DependencyProperty.Register(
        nameof(Timestamp),
        typeof(DateTime),
        typeof(ClockControl),
        new PropertyMetadata(DateTime.Now, TimestampPropertyChangedCallback)
    );

    // CLR Property
    public DateTime Timestamp
    {
        get
        {
            return (DateTime)GetValue(TimestampProperty);
        }

        set
        {
            SetValue(TimestampProperty, value);
        }
    }

    //------------------------------------------------------------------------------
    // Events

    // PropertyChangedCallback
    private static void TimestampPropertyChangedCallback(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
    {
        ClockControl ctrl = (ClockControl)depObj;

        DateTime oldValue = (DateTime)e.OldValue;
        DateTime newValue = (DateTime)e.NewValue;

        RoutedPropertyChangedEventArgs<DateTime> args = new RoutedPropertyChangedEventArgs<DateTime>(oldValue, newValue);
        args.RoutedEvent = ClockControl.TimestampChangedEvent;

        ctrl.RaiseEvent(args);
    }

    // RoutedEvent
    public static readonly RoutedEvent TimestampChangedEvent = EventManager.RegisterRoutedEvent(
        nameof(TimestampChanged),
        RoutingStrategy.Bubble,
        typeof(RoutedPropertyChangedEventHandler<DateTime>),
        typeof(ClockControl)
    );

    // CLR Event
    public event RoutedPropertyChangedEventHandler<DateTime> TimestampChanged
    {
        add
        {
            AddHandler(TimestampChangedEvent, value); 
        }

        remove
        {
            RemoveHandler(TimestampChangedEvent, value);
        }
    }
}
