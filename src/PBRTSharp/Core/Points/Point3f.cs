using System;
using System.Diagnostics.CodeAnalysis;
//using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Points
{
    public readonly struct Point3f : IEquatable<Point3f>
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public Point3f(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double this[in int i] => i == 0 ? X : i == 1 ? Y : Z;

        // Operator overloads
        public static Point3f operator +(in Point3f p, in Vector3f v) => new Point3f(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        public static Point3f operator +(in Point3f p1, in Point3f p2) => new Point3f(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        public static Vector3f operator -(in Point3f p1, in Point3f p2) => new Vector3f(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        public static Point3f operator *(in double d, in Point3f p) => new Point3f(d * p.X, d * p.Y, d * p.Z);

        // Static methods
        public static Point3f ComponentMin(in Point3f p1, in Point3f p2) => new Point3f(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Min(p1.Z, p2.Z));
        public static Point3f ComponentMax(in Point3f p1, in Point3f p2) => new Point3f(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y), Math.Max(p1.Z, p2.Z));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}, {Z.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Point3f && Equals((Point3f)obj);
        public bool Equals([AllowNull]Point3f other) => X == other.X && Y == other.Y && Z == other.Z;
        public override int GetHashCode() => (int)((3 * X) + (5 * Y) + (7 * Z));

        // Public instance methods
        public double DistanceTo(in Point3f p) => (this - p).Length();
        public double DistanceSquaredTo(in Point3f p) => (this - p).LengthSquared();
        public Point3f Abs() => new Point3f(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        public Point3f Floor() => new Point3f(Math.Floor(X), Math.Floor(Y), Math.Floor(Z));
        public Point3f Ceiling() => new Point3f(Math.Ceiling(X), Math.Ceiling(Y), Math.Ceiling(Z));
        public Point3f Lerp(double t, in Point3f p) => ((1.0d - t) * this) + (t * p);
        public Point3f Permute(in int X, in int Y, in int Z) => new Point3f(this[X], this[Y], this[Z]);
    }
}
