using System;
using System.Diagnostics.CodeAnalysis;
using PBRTSharp.Core.Points;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Bounds
{
    public readonly struct Bounds3i : IEquatable<Bounds3i>
    {
        public Point3i Min { get; }
        public Point3i Max { get; }

        public Bounds3i(Point3i min, Point3i max)
        {
            Min = new Point3i(Math.Min(min.X, max.X), Math.Min(min.Y, max.Y), Math.Min(min.Z, max.Z));
            Max = new Point3i(Math.Max(min.X, max.X), Math.Min(min.Y, max.Y), Math.Min(min.Z, max.Z));
        }
        public Bounds3i(Point3i p)
        {
            Min = p;
            Max = p;
        }

        public static Bounds3i MaximumBounds => new Bounds3i(new Point3i(int.MinValue, int.MinValue, int.MinValue),
                                                             new Point3i(int.MaxValue, int.MaxValue, int.MaxValue));
        public Point3i this[in int i] => i == 0 ? Min : Max;

        public static bool operator ==(Bounds3i p1, Bounds3i p2) => p1.Equals(p2);
        public static bool operator !=(Bounds3i p1, Bounds3i p2) => !(p1 == p2);

        public Point3i Corner(in int corner)
        {
            return new Point3i(
                    this[corner & 1].X,
                    this[(corner & 2) != 0 ? 1 : 0].Y,
                    this[(corner & 4) != 0 ? 1 : 0].Z
                );
        }
        public Bounds3i Union(in Point3i p)
        {
            return new Bounds3i(
                    new Point3i(
                        Math.Min(Min.X, p.X),
                        Math.Min(Min.Y, p.Y),
                        Math.Min(Min.Z, p.Z)
                    ),
                    new Point3i(
                        Math.Max(Max.X, p.X),
                        Math.Max(Max.Y, p.Y),
                        Math.Max(Max.Z, p.Z)
                    )
                );
        }
        public Bounds3i Union(in Bounds3i other)
        {
            return new Bounds3i(
                    new Point3i(
                        Math.Min(Min.X, other.Min.X),
                        Math.Min(Min.Y, other.Min.Y),
                        Math.Min(Min.Z, other.Min.Z)
                    ),
                    new Point3i(
                        Math.Max(Max.X, other.Max.X),
                        Math.Max(Max.Y, other.Max.Y),
                        Math.Max(Max.Z, other.Max.Z)
                    )
                );
        }
        public Bounds3i Intersect(in Bounds3i other)
        {
            return new Bounds3i(
                    new Point3i(
                        Math.Max(Min.X, other.Min.X),
                        Math.Max(Min.Y, other.Min.Y),
                        Math.Max(Min.Z, other.Min.Z)
                    ),
                    new Point3i(
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
        public Bounds3i Expand(in int delta) => new Bounds3i(Min - new Vector3i(delta, delta, delta), Max + new Vector3i(delta, delta, delta));
        public Vector3i Diagonal() => Max - Min;
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
        public Point3i Lerp(in Point3i t)
        {
            return new Point3i(
                (int)ExtraMath.Lerp(t.X, Min.X, Max.X),
                (int)ExtraMath.Lerp(t.Y, Min.Y, Max.Y),
                (int)ExtraMath.Lerp(t.Z, Min.Z, Max.Z)
            );
        }
        public Vector3i Offset(in Point3i p)
        {
            var working = p - Min;
            return new Vector3i(
                    Max.X > Min.X ? working.X / (Max.X - Min.X) : working.X,
                    Max.Y > Min.Y ? working.Y / (Max.Y - Min.Y) : working.Y,
                    Max.Z > Min.Z ? working.Z / (Max.Z - Min.Z) : working.Z
            );
        }
        public (Point3f centre, double radius) BoundingSphere()
        {
            var centre = (Point3f)(Min + Max) / 2;
            return (
                centre,
                Contains(centre) ? centre.DistanceTo((Point3f)Max) : 0
                );
        }

        public override string ToString() => $"Bounds {Max.ToString()} to {Min.ToString()}";
        public override bool Equals(object? obj) => obj is Bounds3i && Equals((Bounds3i)obj);
        public bool Equals([AllowNull] Bounds3i other) => Max == other.Max && Min == other.Min;
        public override int GetHashCode() => (3 * Min.GetHashCode()) + (5 * Max.GetHashCode());
    }
}
