using System;

namespace PBRTSharp.Core.Matrices
{
    // TODO: I don't think this will perform well. Test it later.
    public readonly struct Matrix4x4
    {
        private readonly double[][] m;

        public Matrix4x4(double[][] mat)
        {
            m = mat;
        }
        public Matrix4x4(double t00, double t01, double t02, double t03,
                         double t10, double t11, double t12, double t13,
                         double t20, double t21, double t22, double t23,
                         double t30, double t31, double t32, double t33)
        {
            m = new[]
            {
                new []{ t00, t01,t02,t03},
                new []{ t10, t11,t12,t13},
                new []{ t20, t21,t22,t23},
                new []{ t30, t31,t32,t33}
            };
        }

        public Matrix4x4 Transpose()
        {
            return new Matrix4x4(
                    m[0][0], m[1][0], m[2][0], m[3][0],
                    m[0][1], m[1][1], m[2][1], m[3][1],
                    m[0][2], m[1][2], m[2][2], m[3][2],
                    m[0][3], m[1][3], m[2][3], m[3][3]
            );
        }

        // TODO: check performance
        public Matrix4x4 Multiply(in Matrix4x4 other)
        {
            var newArray = new double[4][];
            for (var i = 0; i < 4; i++)
            {
                newArray[i] = new double[4];
                for (var j = 0; j < 4; j++)
                {
                    newArray[i][j] = (m[i][0] * other.m[0][j]) +
                                     (m[i][1] * other.m[1][j]) +
                                     (m[i][2] * other.m[2][j]) +
                                     (m[i][3] * other.m[3][j]);
                }
            }
            return new Matrix4x4(newArray);
        }

        private static readonly ValueTuple<int, int>[] OrderOfRowOperations =
        {
            (0,0),
            (0,1),
            (0,2),
            (0,3),

            (1,1),
            (1,2),
            (1,3),

            (2,2),
            (2,3),

            (3,3),
            (3,2),
            (3,1),
            (3,0),

            (2,1),
            (2,0),

            (1,0)
        };

        public Matrix4x4 Inverse() => throw new NotImplementedException();
    }
}
