using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Normals
{
    public readonly struct Normal3f : IEquatable<Normal3f>
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public Normal3f(in double x, in double y, in double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Normal3f(in Vector3f v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public double this[in int i] => i == 0 ? X : i == 1 ? Y : Z;

        // Operator overloads
        public static Normal3f operator +(in Normal3f v1, in Normal3f v2) => new Normal3f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Normal3f operator -(in Normal3f v1, in Normal3f v2) => new Normal3f(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        public static Normal3f operator *(in double d, in Normal3f v) => new Normal3f(d * v.X, d * v.Y, d * v.Z);
        public static Normal3f operator /(in Normal3f v, in double d) => 1.0d / d * v;
        public static Normal3f operator -(in Normal3f v) => new Normal3f(-v.X, -v.Y, -v.Z);
        public static bool operator ==(Normal3f v1, Normal3f v2) => v1.Equals(v2);
        public static bool operator !=(Normal3f v1, Normal3f v2) => !(v1 == v2);

        // Static methods
        public static Normal3f ComponentMin(in Normal3f v1, in Normal3f v2) => new Normal3f(Math.Min(v1.X, v2.X), Math.Min(v1.Y, v2.Y), Math.Min(v1.Z, v2.Z));
        public static Normal3f ComponentMax(in Normal3f v1, in Normal3f v2) => new Normal3f(Math.Max(v1.X, v2.X), Math.Max(v1.Y, v2.Y), Math.Max(v1.Z, v2.Z));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}, {Z.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Normal3f && Equals((Normal3f)obj);
        public bool Equals([AllowNull]Normal3f other) => X == other.X && Y == other.Y && Z == other.Z;
        public override int GetHashCode() => (int)((3 * X) + (5 * Y) + (7 * Z));

        // Public instance methods
        public Normal3f Abs() => new Normal3f(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        public double Dot(in Normal3f other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);
        public double Dot(in Vector3f other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);
        public double AbsDot(in Normal3f other) => Math.Abs(Dot(other));
        public double LengthSquared() => (X * X) + (Y * Y) + (Z * Z);
        public double Length() => Math.Sqrt(LengthSquared());
        public Normal3f Normalize() => this / Length();
        public double MinComponent() => Math.Min(X, Math.Min(Y, Z));
        public double MaxComponent() => Math.Max(X, Math.Max(Y, Z));
        public double MaxDimension() => X > Y ? (X > Z ? 0 : 2) : (Y > Z ? 1 : 2);
        public Normal3f Permute(in int X, in int Y, in int Z) => new Normal3f(this[X], this[Y], this[Z]);
        public Normal3f FlipToSameHemisphereAs(in Vector3f v) => Dot(v) < 0f ? -this : this;
    }
}
