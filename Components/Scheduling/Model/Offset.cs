namespace TimetablePlanning.Components.Scheduling;

    public struct Offset(int x, int y)
{
    public int X { get; } = x; public int Y { get; } = y; public bool IsInvalid { get; private set; } = false;
    public static Offset Invalid => new(0, 0) { IsInvalid = true };
        public static Offset operator +(Offset a, Offset b) => a.IsInvalid ? b : b.IsInvalid ? a : new(a.X + b.X, a.Y + b.Y);
        public static Offset operator -(Offset a, Offset b) => a.IsInvalid ? b : b.IsInvalid ? a : new(a.X - b.X, a.Y - b.Y);
    }
