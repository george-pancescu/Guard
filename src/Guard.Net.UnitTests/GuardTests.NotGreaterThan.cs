using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(11, 10, "paramName", "custom error message", "custom error message\r\nParameter name: paramName")]
        [TestCase(11, 10, "paramName", null, "[paramName] cannot be greater than 10.\r\nParameter name: paramName")]
        [TestCase(11, 10, "", null, "[parameter] cannot be greater than 10.\r\nParameter name: parameter")]
        [TestCase(11, 10, " ", null, "[parameter] cannot be greater than 10.\r\nParameter name: parameter")]
        [TestCase(11, 10, null, null, "[parameter] cannot be greater than 10.\r\nParameter name: parameter")]
        public void NotGreaterThan_InvalidInputDefaultException_ThrowsException(
            int input,
            int threshold,
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentOutOfRangeException>(
                    () => Guard.NotGreaterThan(input, threshold, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotGreaterThan_InvalidInputCustomException_ThrowsException()
        {
            int input = 11;
            int threshold = 10;
            var expectedErrorMessage = "error message\r\nParameter name: parameter";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                    () => Guard.NotGreaterThan(input, threshold, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotGreaterThan_InvalidInputNullCustomException_ThrowsException()
        {
            int input = 11;
            int threshold = 10;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotGreaterThan(input, threshold, exception));
        }

        [Test]
        [TestCase(null)]
        [TestCase("custom message")]
        public void NotGreaterThan_InvalidNullCustomException2_ThrowsException(string message)
        {
            int input = 11;
            int threshold = 10;

            if (message == null)
            {
                Should.Throw<InvalidOperationException>(() => Guard.NotGreaterThan<int, InvalidOperationException>(input, threshold));
            }
            else
            {
                Should
                    .Throw<InvalidOperationException>(() => Guard.NotGreaterThan<int, InvalidOperationException>(input, threshold, message))
                    .Message
                    .ShouldBe(message);
            }
        }

        [Test]
        [TestCase(9, 10)]
        [TestCase(10, 10)]
        public void NotGreaterThan_ValidInput_DoesNotThrowException(int input, int threshold)
        {
            Should.NotThrow(() => Guard.NotGreaterThan(input, threshold, nameof(input), null));
        }
    }
}
