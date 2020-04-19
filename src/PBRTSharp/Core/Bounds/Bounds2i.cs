using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using PBRTSharp.Core.Points;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Bounds
{
    public readonly struct Bounds2i : IEquatable<Bounds2i>, IEnumerable<Point2i>
    {
        public Point2i Min { get; }
        public Point2i Max { get; }

        public Bounds2i(Point2i min, Point2i max)
        {
            Min = new Point2i(Math.Min(min.X, max.X), Math.Min(min.Y, max.Y));
            Max = new Point2i(Math.Max(min.X, max.X), Math.Min(min.Y, max.Y));
        }
        public Bounds2i(Point2i p)
        {
            Min = p;
            Max = p;
        }

        public static Bounds2i MaximumBounds => new Bounds2i(new Point2i(int.MinValue, int.MinValue),
                                                             new Point2i(int.MaxValue, int.MaxValue));
        public Point2i this[int i] => i == 0 ? Min : Max;

        public static bool operator ==(in Bounds2i p1, in Bounds2i p2) => p1.Equals(p2);
        public static bool operator !=(in Bounds2i p1, in Bounds2i p2) => !(p1 == p2);

        public Point2i Corner(int corner)
        {
            return new Point2i(
                    this[corner & 1].X,
                    this[(corner & 2) != 0 ? 1 : 0].Y
                );
        }
        public Bounds2i Union(in Point2i p)
        {
            return new Bounds2i(
                    new Point2i(
                        Math.Min(Min.X, p.X),
                        Math.Min(Min.Y, p.Y)
                    ),
                    new Point2i(
                        Math.Max(Max.X, p.X),
                        Math.Max(Max.Y, p.Y)
                    )
                );
        }
        public Bounds2i Union(in Bounds2i other)
        {
            return new Bounds2i(
                    new Point2i(
                        Math.Min(Min.X, other.Min.X),
                        Math.Min(Min.Y, other.Min.Y)
                    ),
                    new Point2i(
                        Math.Max(Max.X, other.Max.X),
                        Math.Max(Max.Y, other.Max.Y)
                    )
                );
        }
        public Bounds2i Intersect(in Bounds2i other)
        {
            return new Bounds2i(
                    new Point2i(
                        Math.Max(Min.X, other.Min.X),
                        Math.Max(Min.Y, other.Min.Y)
                    ),
                    new Point2i(
                        Math.Min(Max.X, other.Max.X),
                        Math.Min(Max.Y, other.Max.Y)
                    )
                );
        }
        public bool Contains(in Point2i p) =>
                        p.X >= Min.X && p.X <= Max.X &&
                        p.Y >= Min.Y && p.Y <= Max.Y;
        public bool ContainsExclusive(in Point2i p) =>
                        p.X >= Min.X && p.X < Max.X &&
                        p.Y >= Min.Y && p.Y < Max.Y;
        public Bounds2i Expand(int delta) => new Bounds2i(Min - new Vector2i(delta, delta), Max + new Vector2i(delta, delta));
        public Vector2i Diagonal() => Max - Min;
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
        public Point2i Lerp(in Point2i t)
        {
            return new Point2i(
                (int)ExtraMath.Lerp(t.X, Min.X, Max.X),
                (int)ExtraMath.Lerp(t.Y, Min.Y, Max.Y)
            );
        }
        public Vector2i Offset(in Point2i p)
        {
            var working = p - Min;
            return new Vector2i(
                    Max.X > Min.X ? working.X / (Max.X - Min.X) : working.X,
                    Max.Y > Min.Y ? working.Y / (Max.Y - Min.Y) : working.Y
            );
        }

        public override string ToString() => $"Bounds {Max.ToString()} to {Min.ToString()}";
        public override bool Equals(object? obj) => obj is Bounds2i && Equals((Bounds2i)obj);
        public bool Equals([AllowNull] Bounds2i other) => Max == other.Max && Min == other.Min;
        public override int GetHashCode() => (3 * Min.GetHashCode()) + (5 * Max.GetHashCode());
        public IEnumerator<Point2i> GetEnumerator()
        {
            for (var j = 0; j < Max.Y; j++)
            {
                for (var i = 0; i < Max.X; i++)
                {
                    yield return new Point2i(i, j);
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
