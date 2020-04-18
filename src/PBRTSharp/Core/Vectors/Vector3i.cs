using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace PBRTSharp.Core.Vectors
{
    public readonly struct Vector3i : IEquatable<Vector3i>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public Vector3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int this[in int i] => i == 0 ? X : i == 1 ? Y : Z;

        // Operator overloads
        public static Vector3i operator +(in Vector3i v1, in Vector3i v2) => new Vector3i(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Vector3i operator -(in Vector3i v1, in Vector3i v2) => new Vector3i(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        public static Vector3i operator *(in double d, in Vector3i v) => new Vector3i((int)(d * v.X), (int)(d * v.Y), (int)(d * v.Z));
        public static Vector3i operator /(in Vector3i v, in double d) => 1.0d / d * v;
        public static Vector3i operator -(in Vector3i v) => new Vector3i(-v.X, -v.Y, -v.Z);
        public static bool operator ==(Vector3i v1, Vector3i v2) => v1.Equals(v2);
        public static bool operator !=(Vector3i v1, Vector3i v2) => !(v1 == v2);

        // Static methods
        public static Vector3i ComponentMin(in Vector3i v1, in Vector3i v2) => new Vector3i(Math.Min(v1.X, v2.X), Math.Min(v1.Y, v2.Y), Math.Min(v1.Z, v2.Z));
        public static Vector3i ComponentMax(in Vector3i v1, in Vector3i v2) => new Vector3i(Math.Max(v1.X, v2.X), Math.Max(v1.Y, v2.Y), Math.Max(v1.Z, v2.Z));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}, {Z.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Vector3i && Equals((Vector3i)obj);
        public bool Equals([AllowNull]Vector3i other) => X == other.X && Y == other.Y && Z == other.Z;
        public override int GetHashCode() => (3 * X) + (5 * Y) + (7 * Z);

        // Public instance methods
        public Vector3i Abs() => new Vector3i(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        public double Dot(in Vector3i other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);
        public double AbsDot(in Vector3i other) => Math.Abs(Dot(other));
        public Vector3i Cross(in Vector3i other)
        {
            return new Vector3i(
                (Y * other.Z) - (Z * other.Y),
                (Z * other.X) - (X * other.Z),
                (X * other.Y) - (Y * other.X));
        }
        public double LengthSquared() => (X * X) + (Y * Y) + (Z * Z);
        public double Length() => Math.Sqrt(LengthSquared());
        public Vector3i Normalize() => this / Length();
        public double MinComponent() => Math.Min(X, Math.Min(Y, Z));
        public double MaxComponent() => Math.Max(X, Math.Max(Y, Z));
        public double MaxDimension() => X > Y ? (X > Z ? 0 : 2) : (Y > Z ? 1 : 2);
        public Vector3i Permute(in int X, in int Y, in int Z) => new Vector3i(this[X], this[Y], this[Z]);
    }
}
