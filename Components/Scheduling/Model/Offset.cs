namespace TimetablePlanning.Components.Scheduling;

    public struct Offset
    {
        public Offset(int x, int y) { X = x; Y = y; IsInvalid = false; }
        public int X { get; }
        public int Y { get; }
        public bool IsInvalid { get; private set; }

        public static Offset Invalid => new(0, 0) { IsInvalid = true };
        public static Offset operator +(Offset a, Offset b) => a.IsInvalid ? b : b.IsInvalid ? a : new(a.X + b.X, a.Y + b.Y);
        public static Offset operator -(Offset a, Offset b) => a.IsInvalid ? b : b.IsInvalid ? a : new(a.X - b.X, a.Y - b.Y);
    }
