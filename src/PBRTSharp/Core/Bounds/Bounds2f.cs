using System;
using System.Diagnostics.CodeAnalysis;
using PBRTSharp.Core.Points;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Bounds
{
    public readonly struct Bounds2f : IEquatable<Bounds2f>
    {
        public Point2f Min { get; }
        public Point2f Max { get; }

        public Bounds2f(Point2f min, Point2f max)
        {
            Min = new Point2f(Math.Min(min.X, max.X), Math.Min(min.Y, max.Y));
            Max = new Point2f(Math.Max(min.X, max.X), Math.Min(min.Y, max.Y));
        }
        public Bounds2f(Point2f p)
        {
            Min = p;
            Max = p;
        }

        public static Bounds2f MaximumBounds => new Bounds2f(new Point2f(double.MinValue, double.MinValue),
                                                             new Point2f(double.MaxValue, double.MaxValue));
        public Point2f this[in int i] => i == 0 ? Min : Max;

        public static bool operator ==(Bounds2f p1, Bounds2f p2) => p1.Equals(p2);
        public static bool operator !=(Bounds2f p1, Bounds2f p2) => !(p1 == p2);

        public Point2f Corner(in int corner)
        {
            return new Point2f(
                    this[corner & 1].X,
                    this[(corner & 2) != 0 ? 1 : 0].Y
                );
        }
        public Bounds2f Union(in Point2f p)
        {
            return new Bounds2f(
                    new Point2f(
                        Math.Min(Min.X, p.X),
                        Math.Min(Min.Y, p.Y)
                    ),
                    new Point2f(
                        Math.Max(Max.X, p.X),
                        Math.Max(Max.Y, p.Y)
                    )
                );
        }
        public Bounds2f Union(in Bounds2f other)
        {
            return new Bounds2f(
                    new Point2f(
                        Math.Min(Min.X, other.Min.X),
                        Math.Min(Min.Y, other.Min.Y)
                    ),
                    new Point2f(
                        Math.Max(Max.X, other.Max.X),
                        Math.Max(Max.Y, other.Max.Y)
                    )
                );
        }
        public Bounds2f Intersect(in Bounds2f other)
        {
            return new Bounds2f(
                    new Point2f(
                        Math.Max(Min.X, other.Min.X),
                        Math.Max(Min.Y, other.Min.Y)
                    ),
                    new Point2f(
                        Math.Min(Max.X, other.Max.X),
                        Math.Min(Max.Y, other.Max.Y)
                    )
                );
        }
        public bool Contains(in Point2f p) =>
                        p.X >= Min.X && p.X <= Max.X &&
                        p.Y >= Min.Y && p.Y <= Max.Y;
        public bool ContainsExclusive(in Point2f p) =>
                        p.X >= Min.X && p.X < Max.X &&
                        p.Y >= Min.Y && p.Y < Max.Y;
        public Bounds2f Expand(in double delta) => new Bounds2f(Min - new Vector2f(delta, delta), Max + new Vector2f(delta, delta));
        public Vector2f Diagonal() => Max - Min;
        public double Area()
        {
            var diag = Diagonal();
            return diag.X * diag.Y;
        }
        public double Volume()
        {
            var diag = Diagonal();
            return diag.X * diag.Y;
        }
        public int MaximumExtent()
        {
            var diag = Diagonal();
            return diag.X > diag.Y ? 0 : 1;
        }
        public Point2f Lerp(in Point2f t)
        {
            return new Point2f(
                ExtraMath.Lerp(t.X, Min.X, Max.X),
                ExtraMath.Lerp(t.Y, Min.Y, Max.Y)
            );
        }
        public Vector2f Offset(in Point2f p)
        {
            var working = p - Min;
            return new Vector2f(
                    Max.X > Min.X ? working.X / (Max.X - Min.X) : working.X,
                    Max.Y > Min.Y ? working.Y / (Max.Y - Min.Y) : working.Y
            );
        }

        public override string ToString() => $"Bounds {Max.ToString()} to {Min.ToString()}";
        public override bool Equals(object? obj) => obj is Bounds2f && Equals((Bounds2f)obj);
        public bool Equals([AllowNull] Bounds2f other) => Max == other.Max && Min == other.Min;
        public override int GetHashCode() => (3 * Min.GetHashCode()) + (5 * Max.GetHashCode());
    }
}
