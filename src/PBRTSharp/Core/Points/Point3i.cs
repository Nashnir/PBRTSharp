using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Points
{
    public readonly struct Point3i : IEquatable<Point3i>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public Point3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public int this[in int i] => i == 0 ? X : i == 1 ? Y : Z;

        // Operator overloads
        public static Point3i operator +(in Point3i p, in Vector3f v) => new Point3i((int)(p.X + v.X), (int)(p.Y + v.Y), (int)(p.Z + v.Z));
        public static Point3i operator +(in Point3i p1, in Point3i p2) => new Point3i(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        public static Vector3f operator -(in Point3i p1, in Point3i p2) => new Vector3f(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        public static Point3i operator -(in Point3i p, in Vector3i v) => new Point3i(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        public static Point3i operator *(in double d, in Point3i p) => new Point3i((int)(d * p.X), (int)(d * p.Y), (int)(d * p.Z));
        public static explicit operator Point3f(in Point3i p) => new Point3f(p.X, p.Y, p.Z);
        public static bool operator ==(Point3i p1, Point3i p2) => p1.Equals(p2);
        public static bool operator !=(Point3i p1, Point3i p2) => !(p1 == p2);

        // Static methods
        public static Point3i ComponentMin(in Point3i p1, in Point3i p2) => new Point3i(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Min(p1.Z, p2.Z));
        public static Point3i ComponentMax(in Point3i p1, in Point3i p2) => new Point3i(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y), Math.Max(p1.Z, p2.Z));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}, {Z.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Point3i && Equals((Point3i)obj);
        public bool Equals([AllowNull]Point3i other) => X == other.X && Y == other.Y && Z == other.Z;
        public override int GetHashCode() => (3 * X) + (5 * Y) + (7 * Z);

        // Public instance methods
        public double DistanceTo(in Point3i p) => (this - p).Length();
        public double DistanceSquaredTo(in Point3i p) => (this - p).LengthSquared();
        public Point3i Abs() => new Point3i(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        public Point3i Floor() => new Point3i(X, Y, Z);
        public Point3i Ceiling() => new Point3i(X, Y, Z);
        public Point3i Lerp(double t, in Point3i p) => ((1.0d - t) * this) + (t * p);
        public Point3i Permute(in int X, in int Y, in int Z) => new Point3i(this[X], this[Y], this[Z]);
    }
}
