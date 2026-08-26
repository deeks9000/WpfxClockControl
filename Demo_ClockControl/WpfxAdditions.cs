using WpfxCustomControls;

namespace UserExtensions;

public static class WpfxAdditions
{
    public static ClockControl ClockControlX(Action<ClockControl>? configure = null)
    {
        var element = new ClockControl();
        configure?.Invoke(element);
        return element;
    }   
}
