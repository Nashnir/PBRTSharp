using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Points
{
    public readonly struct Point2f : IEquatable<Point2f>
    {
        public double X { get; }
        public double Y { get; }
        public Point2f(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double this[in int i] => i == 0 ? X : Y;

        // Operator overloads
        public static Point2f operator +(in Point2f p, in Vector2f v) => new Point2f(p.X + v.X, p.Y + v.Y);
        public static Point2f operator +(in Point2f p1, in Point2f p2) => new Point2f(p1.X + p2.X, p1.Y + p2.Y);
        public static Vector2f operator -(in Point2f p1, in Point2f p2) => new Vector2f(p1.X - p2.X, p1.Y - p2.Y);
        public static Point2f operator -(in Point2f p, in Vector2f v) => new Point2f(p.X - v.X, p.Y - v.Y);
        public static Point2f operator *(in double d, in Point2f p) => new Point2f(d * p.X, d * p.Y);
        public static bool operator ==(Point2f p1, Point2f p2) => p1.Equals(p2);
        public static bool operator !=(Point2f p1, Point2f p2) => !(p1 == p2);

        // Static methods
        public static Point2f ComponentMin(in Point2f p1, in Point2f p2) => new Point2f(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
        public static Point2f ComponentMax(in Point2f p1, in Point2f p2) => new Point2f(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Point2f && Equals((Point2f)obj);
        public bool Equals([AllowNull]Point2f other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => (int)((3 * X) + (5 * Y));

        // Public instance methods
        public double DistanceTo(in Point2f p) => (this - p).Length();
        public double DistanceSquaredTo(in Point2f p) => (this - p).LengthSquared();
        public Point2f Abs() => new Point2f(Math.Abs(X), Math.Abs(Y));
        public Point2f Floor() => new Point2f(Math.Floor(X), Math.Floor(Y));
        public Point2f Ceiling() => new Point2f(Math.Ceiling(X), Math.Ceiling(Y));
        public Point2f Lerp(double t, in Point2f p) => ((1.0d - t) * this) + (t * p);
        public Point2f Permute(in int X, in int Y) => new Point2f(this[X], this[Y]);
    }
}
