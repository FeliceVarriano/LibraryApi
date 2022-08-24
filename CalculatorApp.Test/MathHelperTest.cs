using System;
using Xunit;

namespace CalculatorApp.Test
{
    public class MathHelperTest
    {
        [Fact]
        public void IsEvenTest()
        {
            var calculator = new MathFormulas();
            int x = 1;
            int y = 2;

            var xResults = calculator.IsEven(x);
            var yResults = calculator.IsEven(y);

            // X should be false.
            Assert.False(xResults);
            // Y should be true.
            Assert.True(yResults);
        }

        [Theory]
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 2)]
        public void DiffTest(int x, int y, int expectedValue)
        {
            var calculator = new MathFormulas();
            var result = calculator.Diff(x, y);

            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(3, 3, 6)]
        [InlineData(6, 6, 12)]
        public void AddTest(int x, int y, int expectedValue)
        {
            var calculator = new MathFormulas();
            var result = calculator.Add(x, y);

            Assert.Equal(expectedValue, result);
        }


        [Theory]
        [InlineData(new int[3] { 1,2,3 }, 6)]
        [InlineData(new int[3] { 5, 2, 5 }, 12)]
        public void SumTest(int[] values, int expectedValue)
        {
            var calculator = new MathFormulas();
            var result = calculator.Sum(values);

            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(new int[3] { 1, 2, 3 }, 2)]
        [InlineData(new int[3] { 5, 2, 5 }, 4)]
        public void AverageTest(int[] values, int expectedValue)
        {
            var calculator = new MathFormulas();
            var result = calculator.Average(values);

            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [MemberData(nameof(MathFormulas.Data), MemberType = typeof(MathFormulas))]
        public void Add_MemberData_Test(int x, int y, int expectedValue)
        {
            var calculator = new MathFormulas();
            var result = calculator.Add(x, y);

            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [ClassData(typeof(MathFormulas))]
        public void Add_ClassData_Test(int x, int y, int expectedValue)
        {
            var calculator = new MathFormulas();
            var result = calculator.Add(x, y);

            Assert.Equal(expectedValue, result);
        }
    }
}
