using PBRTSharp.Core.Vectors;
using Xunit;

namespace PBRTSharpTest.Core.Vectors
{
    public class Vector3fTests
    {
        private static readonly Vector3f _ZeroVector = new Vector3f(0, 0, 0);
        protected static ref readonly Vector3f ZeroVector => ref _ZeroVector;

        private static readonly Vector3f _XUnitVector = new Vector3f(1, 0, 0);
        protected static ref readonly Vector3f XUnitVector => ref _XUnitVector;

        private static readonly Vector3f _YUnitVector = new Vector3f(0, 1, 0);
        protected static ref readonly Vector3f YUnitVector => ref _YUnitVector;

        private static readonly Vector3f _ZUnitVector = new Vector3f(0, 0, 1);
        protected static ref readonly Vector3f ZUnitVector => ref _ZUnitVector;
    }

    public class Vector3fOperatorTests : Vector3fTests
    {
        [Theory]
        [MemberData(nameof(AddData))]
        public void TestAddOperator(Vector3f left, Vector3f right, Vector3f expected) => Assert.Equal(expected, left + right);
        public static TheoryData<Vector3f, Vector3f, Vector3f> AddData => new TheoryData<Vector3f, Vector3f, Vector3f>
        {
            { ZeroVector, ZeroVector, ZeroVector },

            { ZeroVector, XUnitVector, XUnitVector },
            { XUnitVector, ZeroVector, XUnitVector },

            { ZeroVector, YUnitVector, YUnitVector },
            { YUnitVector, ZeroVector, YUnitVector },

            { ZeroVector, ZUnitVector, ZUnitVector },
            { ZUnitVector, ZeroVector, ZUnitVector },

            { XUnitVector, YUnitVector, new Vector3f(1,1,0) },
            { XUnitVector, ZUnitVector, new Vector3f(1,0,1) },
            { YUnitVector, ZUnitVector, new Vector3f(0,1,1) },

            { XUnitVector, new Vector3f(-1,0,0), ZeroVector },
            { YUnitVector, new Vector3f(0,-1,0), ZeroVector },
            { ZUnitVector, new Vector3f(0,0,-1), ZeroVector },

        };

        [Theory]
        [MemberData(nameof(SubtractData))]
        public void TestSubtractOperator(Vector3f left, Vector3f right, Vector3f expected) => Assert.Equal(expected, left - right);
        public static TheoryData<Vector3f, Vector3f, Vector3f> SubtractData => new TheoryData<Vector3f, Vector3f, Vector3f>
        {
            { ZeroVector, ZeroVector, ZeroVector },

            { ZeroVector, XUnitVector, new Vector3f(-1,0,0) },
            { XUnitVector, ZeroVector, XUnitVector },

            { ZeroVector, YUnitVector,  new Vector3f(0,-1,0) },
            { YUnitVector, ZeroVector, YUnitVector },

            { ZeroVector, ZUnitVector,  new Vector3f(0,0,-1) },
            { ZUnitVector, ZeroVector, ZUnitVector },

            { XUnitVector, YUnitVector, new Vector3f(1,-1,0) },
            { XUnitVector, ZUnitVector, new Vector3f(1,0,-1) },
            { YUnitVector, ZUnitVector, new Vector3f(0,1,-1) },

            { XUnitVector, new Vector3f(-1,0,0), new Vector3f(2,0,0) },
            { YUnitVector, new Vector3f(0,-1,0), new Vector3f(0,2,0) },
            { ZUnitVector, new Vector3f(0,0,-1), new Vector3f(0,0,2) },

        };
    }
}
