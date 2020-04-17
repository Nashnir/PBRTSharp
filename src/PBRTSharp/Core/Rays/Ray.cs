using PBRTSharp.Core.Points;
using PBRTSharp.Core.Vectors;

namespace PBRTSharp.Core.Rays
{
    public class Ray
    {
        public Point3f Origin { get; }
        public Vector3f Direction { get; }
        public double TMax { get; set; }
        public double CastTime { get; }
        // public IMedium Medium { get; } // TODO: add media when I know how to do that

        public Ray(in Point3f origin, in Vector3f direction, in double tMax = double.PositiveInfinity, in double castTime = 0d)
        {
            Origin = origin;
            Direction = direction;
            TMax = tMax;
            CastTime = castTime;
        }

        public Point3f ValueAtParameter(in double t) => Origin + (t * Direction);
    }
}
