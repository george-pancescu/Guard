using System;
using NUnit.Framework;
using Shouldly;

namespace Guard.Net.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(null, "paramName", "custom error message", "custom error message\r\nParameter name: paramName")]
        [TestCase(null, "paramName", null, "[paramName] cannot be Null, empty or white-space.\r\nParameter name: paramName")]
        [TestCase(null, "", null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        [TestCase(null, " ", null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        [TestCase(null, null, null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        [TestCase("", "paramName", "custom error message", "custom error message\r\nParameter name: paramName")]
        [TestCase("", "paramName", null, "[paramName] cannot be Null, empty or white-space.\r\nParameter name: paramName")]
        [TestCase("", "", null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        [TestCase("", " ", null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        [TestCase("", null, null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        [TestCase(" ", "paramName", "custom error message", "custom error message\r\nParameter name: paramName")]
        [TestCase(" ", "paramName", null, "[paramName] cannot be Null, empty or white-space.\r\nParameter name: paramName")]
        [TestCase(" ", "", null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        [TestCase(" ", " ", null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        [TestCase(" ", null, null, "[parameter] cannot be Null, empty or white-space.\r\nParameter name: parameter")]
        public void NotNullOrWhitespace_InvalidInputDefaultException_ThrowsException(
            string input,
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentException>(
                    () => Guard.NotNullOrWhitespace(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNullOrWhitespace_InvalidInputCustomException_ThrowsException()
        {
            string input = null;
            var expectedErrorMessage = "error message\r\nParameter name: parameter";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                    () => Guard.NotNullOrWhitespace(input, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNullOrWhitespace_InvalidInputNullCustomException_ThrowsException()
        {
            string input = null;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotNullOrWhitespace(input, exception));
        }

        [Test]
        public void NotNullOrWhitespace_ValidInput_DoesNotThrowException()
        {
            var input = "input";

            Should.NotThrow(() => Guard.NotNullOrWhitespace(input, nameof(input), null));
        }
    }
}
