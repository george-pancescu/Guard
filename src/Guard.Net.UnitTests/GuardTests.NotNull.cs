using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase("paramName", "custom error message", "custom error message\r\nParameter name: paramName")]
        [TestCase("paramName", null, "[paramName] cannot be Null.\r\nParameter name: paramName")]
        [TestCase("", null, "[parameter] cannot be Null.\r\nParameter name: parameter")]
        [TestCase(" ", null, "[parameter] cannot be Null.\r\nParameter name: parameter")]
        [TestCase(null, null, "[parameter] cannot be Null.\r\nParameter name: parameter")]
        public void NotNull_InvalidInputDefaultException_ThrowsException(
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            object input = null;

            Should.Throw<ArgumentNullException>(
                    () => Guard.NotNull(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNull_InvalidInputCustomException_ThrowsException()
        {
            object input = null;
            var expectedErrorMessage = "error message\r\nParameter name: parameter";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                    () => Guard.NotNull(input, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNull_InvalidNullCustomException_ThrowsException()
        {
            object input = null;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotNull(input, exception));
        }

        [Test]
        public void NotNull_ValidInput_DoesNotThrowException()
        {
            var input = new object();

            Should.NotThrow(() => Guard.NotNull(input, nameof(input), null));
        }
    }
}
