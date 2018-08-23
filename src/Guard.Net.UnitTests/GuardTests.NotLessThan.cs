using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(-1, 0, "paramName", "custom error message", "custom error message\r\nParameter name: paramName")]
        [TestCase(-1, 0, "paramName", null, "[paramName] is out of range.\r\nParameter name: paramName")]
        [TestCase(-1, 0, "", null, "[parameter] is out of range.\r\nParameter name: parameter")]
        [TestCase(-1, 0, " ", null, "[parameter] is out of range.\r\nParameter name: parameter")]
        [TestCase(-1, 0, null, null, "[parameter] is out of range.\r\nParameter name: parameter")]
        public void NotLessThan_InvalidInputDefaultException_ThrowsException(
            int input,
            int threshold,
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentOutOfRangeException>(
                    () => Guard.NotLessThan(input, threshold, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotLessThan_InvalidInputCustomException_ThrowsException()
        {
            int input = -1;
            int threshold = 0;
            var expectedErrorMessage = "error message\r\nParameter name: parameter";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                    () => Guard.NotLessThan(input, threshold, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotLessThan_InvalidInputNullCustomException_ThrowsException()
        {
            int input = -1;
            int threshold = 0;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotLessThan(input, threshold, exception));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        public void NotLessThan_ValidInput_DoesNotThrowException(int input, int threshold)
        {
            Should.NotThrow(() => Guard.NotLessThan(input, threshold, nameof(input), null));
        }
    }
}
