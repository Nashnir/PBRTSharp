using System;
using System.Diagnostics.CodeAnalysis;
using PBRTSharp.Core.Points;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Bounds
{
    public readonly struct Bounds3f : IEquatable<Bounds3f>
    {
        public Point3f Min { get; }
        public Point3f Max { get; }

        public Bounds3f(Point3f min, Point3f max)
        {
            Min = new Point3f(Math.Min(min.X, max.X), Math.Min(min.Y, max.Y), Math.Min(min.Z, max.Z));
            Max = new Point3f(Math.Max(min.X, max.X), Math.Min(min.Y, max.Y), Math.Min(min.Z, max.Z));
        }
        public Bounds3f(Point3f p)
        {
            Min = p;
            Max = p;
        }

        public static Bounds3f MaximumBounds => new Bounds3f(new Point3f(double.MinValue, double.MinValue, double.MinValue),
                                                             new Point3f(double.MaxValue, double.MaxValue, double.MaxValue));
        public Point3f this[int i] => i == 0 ? Min : Max;

        public static bool operator ==(in Bounds3f p1, in Bounds3f p2) => p1.Equals(p2);
        public static bool operator !=(in Bounds3f p1, in Bounds3f p2) => !(p1 == p2);

        public Point3f Corner(int corner)
        {
            return new Point3f(
                    this[corner & 1].X,
                    this[(corner & 2) != 0 ? 1 : 0].Y,
                    this[(corner & 4) != 0 ? 1 : 0].Z
                );
        }
        public Bounds3f Union(in Point3f p)
        {
            return new Bounds3f(
                    new Point3f(
                        Math.Min(Min.X, p.X),
                        Math.Min(Min.Y, p.Y),
                        Math.Min(Min.Z, p.Z)
                    ),
                    new Point3f(
                        Math.Max(Max.X, p.X),
                        Math.Max(Max.Y, p.Y),
                        Math.Max(Max.Z, p.Z)
                    )
                );
        }
        public Bounds3f Union(in Bounds3f other)
        {
            return new Bounds3f(
                    new Point3f(
                        Math.Min(Min.X, other.Min.X),
                        Math.Min(Min.Y, other.Min.Y),
                        Math.Min(Min.Z, other.Min.Z)
                    ),
                    new Point3f(
                        Math.Max(Max.X, other.Max.X),
                        Math.Max(Max.Y, other.Max.Y),
                        Math.Max(Max.Z, other.Max.Z)
                    )
                );
        }
        public Bounds3f Intersect(in Bounds3f other)
        {
            return new Bounds3f(
                    new Point3f(
                        Math.Max(Min.X, other.Min.X),
                        Math.Max(Min.Y, other.Min.Y),
                        Math.Max(Min.Z, other.Min.Z)
                    ),
                    new Point3f(
                        Math.Min(Max.X, other.Max.X),
                        Math.Min(Max.Y, other.Max.Y),
                        Math.Min(Max.Z, other.Max.Z)
                    )
                );
        }
        public bool Contains(in Point3f p) =>
    p.X >= Min.X && p.X <= Max.X &&
    p.Y >= Min.Y && p.Y <= Max.Y &&
    p.Z >= Min.Z && p.Z <= Max.Z;
        public bool ContainsExclusive(in Point3f p) =>
   p.X >= Min.X && p.X < Max.X &&
   p.Y >= Min.Y && p.Y < Max.Y &&
   p.Z >= Min.Z && p.Z < Max.Z;
        public Bounds3f Expand(double delta) => new Bounds3f(Min - new Vector3f(delta, delta, delta), Max + new Vector3f(delta, delta, delta));
        public Vector3f Diagonal() => Max - Min;
        public double SurfaceArea()
        {
            var diag = Diagonal();
            return 2 * ((diag.X * diag.Y) + (diag.X * diag.Z) + (diag.Y * diag.Z));
        }
        public double Volume()
        {
            var diag = Diagonal();
            return diag.X * diag.Y * diag.Z;
        }
        public int MaximumExtent()
        {
            var diag = Diagonal();
            return diag.X > diag.Y && diag.X > diag.Z ? 0 : diag.Y > diag.Z ? 1 : 2;
        }
        public Point3f Lerp(in Point3f t)
        {
            return new Point3f(
                ExtraMath.Lerp(t.X, Min.X, Max.X),
                ExtraMath.Lerp(t.Y, Min.Y, Max.Y),
                ExtraMath.Lerp(t.Z, Min.Z, Max.Z)
            );
        }
        public Vector3f Offset(in Point3f p)
        {
            var working = p - Min;
            return new Vector3f(
                    Max.X > Min.X ? working.X / (Max.X - Min.X) : working.X,
                    Max.Y > Min.Y ? working.Y / (Max.Y - Min.Y) : working.Y,
                    Max.Z > Min.Z ? working.Z / (Max.Z - Min.Z) : working.Z
            );
        }
        public (Point3f centre, double radius) BoundingSphere()
        {
            var centre = (Min + Max) / 2;
            return (
                centre,
                Contains(centre) ? centre.DistanceTo(Max) : 0
                );
        }

        public override string ToString() => $"Bounds {Max.ToString()} to {Min.ToString()}";
        public override bool Equals(object? obj) => obj is Bounds3f && Equals((Bounds3f)obj);
        public bool Equals([AllowNull] Bounds3f other) => Max == other.Max && Min == other.Min;
        public override int GetHashCode() => (3 * Min.GetHashCode()) + (5 * Max.GetHashCode());
    }
}
