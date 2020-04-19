using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace PBRTSharp.Core.Vectors
{
    public readonly struct Vector2i : IEquatable<Vector2i>
    {
        public int X { get; }
        public int Y { get; }
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int this[int i] => i == 0 ? X : Y;

        // Operator overloads
        public static Vector2i operator +(in Vector2i v1, in Vector2i v2) => new Vector2i(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2i operator -(in Vector2i v1, in Vector2i v2) => new Vector2i(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2i operator *(double d, in Vector2i v) => new Vector2i((int)(d * v.X), (int)(d * v.Y));
        public static Vector2i operator /(in Vector2i v, double d) => 1.0d / d * v;
        public static Vector2i operator -(in Vector2i v) => new Vector2i(-v.X, -v.Y);
        public static explicit operator Vector2f(in Vector2i p) => new Vector2f(p.X, p.Y);
        public static bool operator ==(Vector2i v1, Vector2i v2) => v1.Equals(v2);
        public static bool operator !=(Vector2i v1, Vector2i v2) => !(v1 == v2);

        // Static methods
        public static Vector2i ComponentMin(in Vector2i v1, in Vector2i v2) => new Vector2i(Math.Min(v1.X, v2.X), Math.Min(v1.Y, v2.Y));
        public static Vector2i ComponentMax(in Vector2i v1, in Vector2i v2) => new Vector2i(Math.Max(v1.X, v2.X), Math.Max(v1.Y, v2.Y));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Vector2i && Equals((Vector2i)obj);
        public bool Equals([AllowNull]Vector2i other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => (int)((3 * X) + (5 * Y));

        // Public instance methods
        public Vector2i Abs() => new Vector2i(Math.Abs(X), Math.Abs(Y));
        public double Dot(in Vector2i other) => (X * other.X) + (Y * other.Y);
        public double AbsDot(in Vector2i other) => Math.Abs(Dot(other));
        public double LengthSquared() => (X * X) + (Y * Y);
        public double Length() => Math.Sqrt(LengthSquared());
        public Vector2i Normalize() => this / Length();
        public double MinComponent() => Math.Min(X, Y);
        public double MaxComponent() => Math.Max(X, Y);
        public double MaxDimension() => X > Y ? 0 : 1;
        public Vector2i Permute(int X, int Y) => new Vector2i(this[X], this[Y]);
    }
}
