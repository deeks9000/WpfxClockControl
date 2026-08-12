namespace Demo_ClockControl.Converters;

public enum ClockDigit
{
    UnitSeconds = 0,
    TenSeconds,
    UnitMinutes,
    TenMinutes,
    UnitHours,
    TenHours
}

public enum LedSegment
{
    A = 0,
    B,
    C,
    D,
    E,
    F,
    G
}

public readonly record struct LedClockSegment(
    ClockDigit Digit, 
    LedSegment Segment
);
