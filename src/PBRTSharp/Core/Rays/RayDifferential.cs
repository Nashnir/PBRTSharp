using System;
using PBRTSharp.Core.Points;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Rays
{
    public class RayDifferential : Ray
    {
        public bool HasDifferentials { get; set; }
        public Point3f RxOrigin { get; set; }
        public Point3f RyOrigin { get; set; }
        public Vector3f RxDirection { get; set; }
        public Vector3f RyDirection { get; set; }

        public RayDifferential(in Point3f origin, in Vector3f direction, double tMax = double.PositiveInfinity, double castTime = 0d) : base(origin, direction, tMax, castTime)
        {
            HasDifferentials = false;
        }
        public RayDifferential(in Ray r) : this(r?.Origin ?? throw new ArgumentNullException(nameof(r)), r.Direction, r.TMax, r.CastTime) { }

        public void ScaleDifferentials(double s)
        {
            RxOrigin = Origin + (s * (RxOrigin - Origin));
            RyOrigin = Origin + (s * (RyOrigin - Origin));
            RxDirection = Direction + (s * (RxDirection - Direction));
            RyDirection = Direction + (s * (RyDirection - Direction));
        }
    }
}
