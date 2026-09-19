namespace Demo_04_ClockControls.Models;

public readonly record struct SegmentModel(
    ClockUnit Unit,
    LedSegment Segment
);
