using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace WpfxCustomControls;

public class ClockControl : Control
{
    private readonly DispatcherTimer _timer = new DispatcherTimer();

    public static Style DefaultStyle { get; }

    //------------------------------------------------------------------------------
    // Static Constructor

    static ClockControl()
    {
        DefaultStyle = BuildDefaultStyle();

        StyleProperty.OverrideMetadata(typeof(ClockControl), new FrameworkPropertyMetadata(DefaultStyle, OnStyleChanged));
    }

    private static Style BuildDefaultStyle()
    {
        var visualTree = FrameworkElementFactoryX<TextBlock>(
            name: "PART_Root",
            setters: [
                SetterX(TextBlock.TextProperty, BindingX(b => {
                    b.Path = PropertyPathX(nameof(ClockControl.Timestamp));
                    b.RelativeSource = RelativeSourceX(RelativeSourceMode.TemplatedParent);
                    b.StringFormat = "dd/MM/yyyy HH:mm:ss";
                }))
            ]
        );

        var controlTemplate = ControlTemplateX<ClockControl>(visualTree);

        var style = StyleX<ClockControl>(
            setters: [
                SetterX(TextBlock.FontSizeProperty, 24d),
                SetterX(Control.TemplateProperty, controlTemplate),
            ]
        );

        return style;
    }

    private static void OnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is not Style style) return;

        //------------------------------------------------------------------------
        // EXPLICIT BasedOn. This is either:
        // [1] A user supplied a Style with an explicit BasedOn
        // [2] The DefaultStyle, transparently inserted into a user supplied Style without BasedOn (the 2nd pass of this method)

        if (style.BasedOn is not null) return;

        //------------------------------------------------------------------------
        // User supplied Style WITHOUT BasedOn:
        // Preserve the user's entire style, then transparently establish our DefaultStyle as its base

        Style correctedStyle = CloneStyle(style);
        correctedStyle.BasedOn = DefaultStyle;

        d.SetValue(StyleProperty, correctedStyle);
    }

    private static Style CloneStyle(Style source)
    {
        var clone = new Style(source.TargetType, source.BasedOn);

        foreach (var setter in source.Setters)
            clone.Setters.Add(setter);

        foreach (var trigger in source.Triggers)
            clone.Triggers.Add(trigger);

        foreach (var key in source.Resources.Keys)
            clone.Resources[key] = source.Resources[key];

        return clone;
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
        _timer.Interval = TimeSpan.Zero;

        _timer.Start();
    }

    private void ClockControl_Unloaded(object sender, RoutedEventArgs e)
    {
        _timer.Stop();
    }

    //------------------------------------------------------------------------------
    // Properties

    // Dependency Property
    public static readonly DependencyProperty TimestampProperty = DependencyProperty.Register(
        nameof(Timestamp),
        typeof(DateTime),
        typeof(ClockControl),
        new PropertyMetadata(DateTime.Now, TimestampPropertyChangedCallback)
    );

    // CLR Property
    public DateTime Timestamp
    {
        get => (DateTime)GetValue(TimestampProperty);
        set => SetValue(TimestampProperty, value);
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
