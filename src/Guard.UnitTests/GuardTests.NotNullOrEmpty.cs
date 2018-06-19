using System;
using NUnit.Framework;
using Shouldly;

namespace Guard.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(null, "paramName", "custom error message", "custom error message\r\nParameter name: paramName")]
        [TestCase(null, "paramName", null, "[paramName] cannot be Null or empty.\r\nParameter name: paramName")]
        [TestCase(null, "", null, "[parameter] cannot be Null or empty.\r\nParameter name: parameter")]
        [TestCase(null, " ", null, "[parameter] cannot be Null or empty.\r\nParameter name: parameter")]
        [TestCase(null, null, null, "[parameter] cannot be Null or empty.\r\nParameter name: parameter")]
        [TestCase("", "paramName", "custom error message", "custom error message\r\nParameter name: paramName")]
        [TestCase("", "paramName", null, "[paramName] cannot be Null or empty.\r\nParameter name: paramName")]
        [TestCase("", "", null, "[parameter] cannot be Null or empty.\r\nParameter name: parameter")]
        [TestCase("", " ", null, "[parameter] cannot be Null or empty.\r\nParameter name: parameter")]
        [TestCase("", null, null, "[parameter] cannot be Null or empty.\r\nParameter name: parameter")]
        public void NotNullOrEmpty_InvalidInputDefaultException_ThrowsException(
            string input,
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentException>(
                    () => Guard.NotNullOrEmpty(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNullOrEmpty_InvalidInputCustomException_ThrowsException()
        {
            string input = null;
            var expectedErrorMessage = "error message";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                    () => Guard.NotNullOrEmpty(input, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNullOrEmpty_InvalidInputNullCustomException_ThrowsException()
        {
            string input = null;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotNullOrEmpty(input, exception));
        }

        [Test]
        [TestCase("input")]
        [TestCase(" ")]
        public void NotNullOrEmpty_ValidInput_DoesNotThrowException(string input)
        {
            Should.NotThrow(() => Guard.NotNullOrEmpty(input, nameof(input), null));
        }
    }
}
