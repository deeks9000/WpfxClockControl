# WPFX ClockControl

WPF custom control `ClockControl` built with WPFX **entirely in C#**.

No `Generic.xaml` file is required for the default style.

## Custom control: `ClockControl`

Minimalist custom control with:

- A single DependencyProperty / CLR property: `Timestamp` (`DateTime`)
- A RoutedEvent / CLR event: `TimestampChanged`

### ControlTemplate

The default template visual tree contains a single `TextBlock` that displays the template-bound `Timestamp` value as a date/time string.

### How to use

The control can be used in its default state! However, the `ControlTemplate` can be fully customised to suit your own applications. The demo app provides two example templates:

- A 7-segment LED clock
- A weekday/date clock

### Customising the `ControlTemplate`

- Provide a user-defined visual tree, `ControlTemplate`, and `Style`
- Bind elements in the visual tree to the `Timestamp` property using `RelativeSourceMode.TemplatedParent`
- Use associated `Converter` objects where required to transform `DateTime` into the target DependencyProperty type, *e.g.* `TextProperty`, `FillProperty`, `VisibilityProperty` etc.

### Example demo app

The demo app uses the NuGet package `UserExtensions.Wpfx` to create and display a WPF `Window` containing three `ClockControl` instances, each using a different `ControlTemplate`:

- **Row 0:** Built-in default template rendering `Timestamp` as `"dd/MM/yyyy HH:mm:ss"`
- **Row 1:** A 7-segment LED template and style built programmatically (`LedClockStyle` / `LedClockConverter`)
- **Row 2:** A template that formats the date/time and displays the weekday (`WeekdayClockStyle` / `WeekdayConverter`)

![WPF demo app showing a custom ClockControl built using WPFX](https://raw.githubusercontent.com/deeks9000/WpfxClockControl/main/Images/Demo_ClockControl.png)


