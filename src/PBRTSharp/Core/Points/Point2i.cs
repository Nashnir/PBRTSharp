using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Points
{
    public readonly struct Point2i : IEquatable<Point2i>
    {
        public double X { get; }
        public double Y { get; }
        public Point2i(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double this[in int i] => i == 0 ? X : Y;

        // Operator overloads
        public static Point2i operator +(in Point2i p, in Vector2f v) => new Point2i(p.X + v.X, p.Y + v.Y);
        public static Point2i operator +(in Point2i p1, in Point2i p2) => new Point2i(p1.X + p2.X, p1.Y + p2.Y);
        public static Vector2f operator -(in Point2i p1, in Point2i p2) => new Vector2f(p1.X - p2.X, p1.Y - p2.Y);
        public static Point2i operator -(in Point2i p, in Vector2i v) => new Point2i(p.X - v.X, p.Y - v.Y);
        public static Point2i operator *(in double d, in Point2i p) => new Point2i(d * p.X, d * p.Y);
        public static bool operator ==(Point2i p1, Point2i p2) => p1.Equals(p2);
        public static bool operator !=(Point2i p1, Point2i p2) => !(p1 == p2);
        public static explicit operator Point2f(in Point2i p) => new Point2f(p.X, p.Y);

        // Static methods
        public static Point2i ComponentMin(in Point2i p1, in Point2i p2) => new Point2i(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
        public static Point2i ComponentMax(in Point2i p1, in Point2i p2) => new Point2i(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Point2i && Equals((Point2i)obj);
        public bool Equals([AllowNull]Point2i other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => (int)((3 * X) + (5 * Y));

        // Public instance methods
        public double DistanceTo(in Point2i p) => (this - p).Length();
        public double DistanceSquaredTo(in Point2i p) => (this - p).LengthSquared();
        public Point2i Abs() => new Point2i(Math.Abs(X), Math.Abs(Y));
        public Point2i Floor() => new Point2i(X, Y);
        public Point2i Ceiling() => new Point2i(X, Y);
        public Point2i Lerp(double t, in Point2i p) => ((1.0d - t) * this) + (t * p);
        public Point2i Permute(in int X, in int Y) => new Point2i(this[X], this[Y]);
    }
}
