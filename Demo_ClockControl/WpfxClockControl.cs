using WpfxCustomControls;

namespace UserExtensions;

public static class WpfxClockControl
{
    // FQN: WpfxCustomControls.ClockControl

    // This static class provides the helper function:
    // UserExtensions.WpfxClockControl.ClockControlX(...)

    // Add the following line to the file `GlobalUsings.cs`:
    // global using static UserExtensions.WpfxClockControl;

    // `ClockControlX(...)` can then be used directly when building the `UIElement` tree using WPFX

    public static ClockControl ClockControlX(Action<ClockControl>? configure = null)
    {
        var element = new ClockControl();
        configure?.Invoke(element);
        return element;
    }
}
