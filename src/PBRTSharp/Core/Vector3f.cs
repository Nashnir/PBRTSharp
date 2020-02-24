using System;
using System.Diagnostics;
using System.Globalization;

namespace PBRTSharp.Core
{
    public readonly struct Vector3f : IEquatable<Vector3f>
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public Vector3f(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Debug.Assert(!HasNaNs());
        }

        public double this[in int i]
        {
            get {
                Debug.Assert(i >= 0 && i <= 2);
                return i == 0 ? X : i == 1 ? Y : Z;
            }
        }

        // Operator overloads
        public static Vector3f operator +(in Vector3f v1, in Vector3f v2) => new Vector3f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Vector3f operator -(in Vector3f v1, in Vector3f v2) => new Vector3f(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        public static Vector3f operator *(in Vector3f v, in double d) => new Vector3f(d * v.X, d * v.Y, d * v.Z);
        public static Vector3f operator *(in double d, in Vector3f v) => v * d;
        public static Vector3f operator /(in Vector3f v, in double d)
        {
            Debug.Assert(d != 0);
            var recip = 1.0d / d;
            return v * recip;
        }
        public static Vector3f operator -(in Vector3f v) => new Vector3f(-v.X, -v.Y, -v.Z);
        public static bool operator ==(Vector3f v1, Vector3f v2) => v1.Equals(v2);
        public static bool operator !=(Vector3f v1, Vector3f v2) => !(v1 == v2);

        // Named operator overloads
        public static Vector3f Add(in Vector3f left, in Vector3f right) => left + right;
        public static Vector3f Subtract(in Vector3f left, in Vector3f right) => left - right;
        public static Vector3f Multiply(in Vector3f left, in double right) => left * right;
        public static Vector3f Multiply(in double left, in Vector3f right) => left * right;
        public static Vector3f Negate(in Vector3f item) => -item;
        public static Vector3f Divide(in Vector3f left, in double right) => left / right;

        // Static methods
        public static Vector3f ComponentMin(in Vector3f v1, in Vector3f v2) => new Vector3f(Math.Min(v1.X, v2.X), Math.Min(v1.Y, v2.Y), Math.Min(v1.Z, v2.Z));
        public static Vector3f ComponentMax(in Vector3f v1, in Vector3f v2) => new Vector3f(Math.Max(v1.X, v2.X), Math.Max(v1.Y, v2.Y), Math.Max(v1.Z, v2.Z));

        // Overrides from System.Object
        public override string ToString() => $"[{X.ToString("G17", CultureInfo.InvariantCulture)}, {Y.ToString("G17", CultureInfo.InvariantCulture)}, {Z.ToString("G17", CultureInfo.InvariantCulture)}]";
        public override bool Equals(object? obj) => obj is Vector3f && Equals((Vector3f)obj);
        public bool Equals(Vector3f other) => X == other.X && Y == other.Y && Z == other.Z;
        public override int GetHashCode() => (int)((3 * X) + (5 * Y) + (7 * Z));

        // Public instance methods
        public Vector3f Abs() => new Vector3f(Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
        public double Dot(in Vector3f other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);
        public double AbsDot(in Vector3f other) => Math.Abs(Dot(other));
        public Vector3f Cross(in Vector3f other)
        {
            return new Vector3f(
                (Y * other.Z) - (Z * other.Y),
                (Z * other.X) - (X * other.Z),
                (X * other.Y) - (Y * other.X));
        }
        public double LengthSquared() => (X * X) + (Y * Y) + (Z * Z);
        public double Length() => Math.Sqrt(LengthSquared());
        public Vector3f Normalize() => this / Length();
        public double MinComponent() => Math.Min(X, Math.Min(Y, Z));
        public double MaxComponent() => Math.Max(X, Math.Max(Y, Z));
        public double MaxDimension() => X > Y ? (X > Z ? 0 : 2) : (Y > Z ? 1 : 2);
        public Vector3f Permute(in int X, in int Y, in int Z) => new Vector3f(this[X], this[Y], this[Z]);
        public void CreateCoordinateSystem(out Vector3f v2, out Vector3f v3)
        {
            v2 = Math.Abs(X) > Math.Abs(Y)
                ? new Vector3f(-Z, 0, X) / Math.Sqrt((X * X) + (Z * Z))
                : new Vector3f(0, Z, -Y) / Math.Sqrt((Y * Y) + (Z * Z));
            v3 = Cross(v2);
        }

        // Private instance methods
        private bool HasNaNs() => double.IsNaN(X) || double.IsNaN(Y) || double.IsNaN(Z);
    }
}
