using System.Linq;
using PBRTSharp.Core;
using Xunit;

namespace PBRTSharpTest.Core
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
        [Theory]
        [MemberData(nameof(AddData))]
        public void TestAddMethod(Vector3f left, Vector3f right, Vector3f expected) => Assert.Equal(expected, Vector3f.Add(in left, in right));
        [Theory]
        [MemberData(nameof(AddCompareData))]
        public void TestAddMethodEqualsAddOperator(Vector3f left, Vector3f right) => Assert.Equal(left + right, Vector3f.Add(in left, in right));
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
        public static TheoryData<Vector3f, Vector3f> AddCompareData
        {
            get {
                var data = new TheoryData<Vector3f, Vector3f>();
                foreach (var datum in AddData.Select(td => new[] { td[0], td[1] }))
                {
                    data.Add((Vector3f)datum[0], (Vector3f)datum[1]);
                }
                return data;
            }
        }

        [Theory]
        [MemberData(nameof(SubtractData))]
        public void TestSubtractOperator(Vector3f left, Vector3f right, Vector3f expected) => Assert.Equal(expected, left - right);
        [Theory]
        [MemberData(nameof(SubtractData))]
        public void TestSubtractMethod(Vector3f left, Vector3f right, Vector3f expected) => Assert.Equal(expected, Vector3f.Subtract(in left, in right));
        [Theory]
        [MemberData(nameof(SubtractCompareData))]
        public void TestSubtractMethodEqualsSubtractOperator(Vector3f left, Vector3f right) => Assert.Equal(left - right, Vector3f.Subtract(in left, in right));
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
        public static TheoryData<Vector3f, Vector3f> SubtractCompareData
        {
            get {
                var data = new TheoryData<Vector3f, Vector3f>();
                foreach (var datum in SubtractData.Select(td => new[] { td[0], td[1] }))
                {
                    data.Add((Vector3f)datum[0], (Vector3f)datum[1]);
                }
                return data;
            }
        }
    }
}
