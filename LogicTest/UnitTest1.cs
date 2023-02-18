using ValueTester;

namespace LogicTest
{
    public class FirstTests
    {
        [Theory]
        [InlineData(1, 100)]
        [InlineData(99, 100)]
        [InlineData(55, 100)]
        [InlineData(25, 100)]
        public void CheckIfOverFlow_LessThanMax_ReturnTrue(int value, int max)
        {
            // Act
            var re = ValueChecker.CheckIfOverFlow(value, max);

            // Assert
            Assert.Equal(0, re);
        }

        [Theory]
        [InlineData(125, 100)]
        public void CheckIfOverFlow_GreaterThanMax_ReturnTrue(int value, int max)
        {
            // Act
            var re = ValueChecker.CheckIfOverFlow(value, max);

            // Assert
            Assert.Equal(25, re);
        }

    }
}