using System;
using Xunit;

namespace CalculatorApp.Test
{
    public class MathHelperTest
    {
        /// <summary>
        /// Asserts the IsEven method return true on even integers
        /// and false on odd integers.
        /// </summary>
        [Fact]
        public void IsEvenTest()
        {
            // Arrange
            var calculator = new MathFormulas();
            int x = 1;
            int y = 2;

            // Act
            var xResults = calculator.IsEven(x);
            var yResults = calculator.IsEven(y);

            // Assert
            // X should be false.
            Assert.False(xResults);
            // Y should be true.
            Assert.True(yResults);
        }

        /// <summary>
        /// Asserts the difference between the second and first integer
        /// </summary>
        /// <param name="x">First integer, can be negative</param>
        /// <param name="y">Second integer, can be negative</param>
        /// <param name="expectedValue">The expected outcome of the computation</param>
        [Theory]
        [InlineData(1, 2, 1)]
        [InlineData(1, 3, 2)]
        public void DiffTest(int x, int y, int expectedValue)
        {
            // Arrange
            var calculator = new MathFormulas();

            // Act
            var result = calculator.Diff(x, y);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        /// <summary>
        /// Asserts that the integers are added.
        /// </summary>
        /// <param name="x">First integer, can be negative</param>
        /// <param name="y">Second integer, can be negative</param>
        /// <param name="expectedValue">The expected outcome of the computation</param>
        [Theory]
        [InlineData(3, 3, 6)]
        [InlineData(6, 6, 12)]
        public void AddTest(int x, int y, int expectedValue)
        {
            // Arrange 
            var calculator = new MathFormulas();

            // Act
            var result = calculator.Add(x, y);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        /// <summary>
        /// Asserts that all the integers in a collection are properly summed.
        /// </summary>
        /// <param name="values">List of integers to be summed</param>
        /// <param name="expectedValue">The expected outcome of the computation</param>
        [Theory]
        [InlineData(new int[3] { 1,2,3 }, 6)]
        [InlineData(new int[3] { 5, 2, 5 }, 12)]
        public void SumTest(int[] values, int expectedValue)
        {
            // Arrange
            var calculator = new MathFormulas();

            //Act
            var result = calculator.Sum(values);

            //Assert
            Assert.Equal(expectedValue, result);
        }

        /// <summary>
        /// Asserts that the average calculated from a list of integers divided by the lists length
        /// </summary>
        /// <param name="values">List of integers to be summed</param>
        /// <param name="expectedValue">The expected outcome of the computation</param>
        [Theory]
        [InlineData(new int[3] { 1, 2, 3 }, 2)]
        [InlineData(new int[3] { 5, 2, 5 }, 4)]
        public void AverageTest(int[] values, int expectedValue)
        {
            // Arrange
            var calculator = new MathFormulas();

            // Act
            var result = calculator.Average(values);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        /// <summary>
        /// Asserts that data retrieved via member data is properly added
        /// </summary>
        /// <param name="x">First Integer</param>
        /// <param name="y">Second Integer</param>
        /// <param name="expectedValue">The expected outcome of the computation</param>
        [Theory]
        [MemberData(nameof(MathFormulas.Data), MemberType = typeof(MathFormulas))]
        public void AddMemberDataTest(int x, int y, int expectedValue)
        {
            // Arrange
            var calculator = new MathFormulas();
            
            // Act
            var result = calculator.Add(x, y);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        /// <summary>
        /// Retrieves the enumerator for the specified Class of MathFormulas and sums up the first and second 
        /// integer to assert it is equal to the third.
        /// </summary>
        /// <param name="x">First integer from the enumerated set</param>
        /// <param name="y">Second integer from the enumerated set</param>
        /// <param name="expectedValue">The third integer from the enumerated set. Set to be the sum of the first two</param>
        [Theory]
        [ClassData(typeof(MathFormulas))]
        public void AddClassDataTest(int x, int y, int expectedValue)
        {
            // Arrange
            var calculator = new MathFormulas();

            // Act
            var result = calculator.Add(x, y);

            // Assert
            Assert.Equal(expectedValue, result);
        }
    }
}
