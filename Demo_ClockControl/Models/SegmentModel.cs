namespace Demo_ClockControl.Models;

public readonly record struct SegmentModel(
    ClockUnit Unit, 
    LedSegment Segment
);
