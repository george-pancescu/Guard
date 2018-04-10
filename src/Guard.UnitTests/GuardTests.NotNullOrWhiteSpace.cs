using System;
using NUnit.Framework;
using Shouldly;

namespace Guard.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(null, "paramName", "custom error message", "custom error message")]
        [TestCase(null, "paramName", null, "[paramName] cannot be Null.")]
        [TestCase(null, "", null, "[parameter] cannot be Null.")]
        [TestCase(null, " ", null, "[parameter] cannot be Null.")]
        [TestCase(null, null, null, "[parameter] cannot be Null.")]
        [TestCase("", "paramName", "custom error message", "custom error message")]
        [TestCase("", "paramName", null, "[paramName] cannot be Null.")]
        [TestCase("", "", null, "[parameter] cannot be Null.")]
        [TestCase("", " ", null, "[parameter] cannot be Null.")]
        [TestCase("", null, null, "[parameter] cannot be Null.")]
        [TestCase(" ", "paramName", "custom error message", "custom error message")]
        [TestCase(" ", "paramName", null, "[paramName] cannot be Null.")]
        [TestCase(" ", "", null, "[parameter] cannot be Null.")]
        [TestCase(" ", " ", null, "[parameter] cannot be Null.")]
        [TestCase(" ", null, null, "[parameter] cannot be Null.")]
        public void NotNullOrWhitespace_InvalidInputDefaultException_ThrowsException(
            string input,
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentException>(
                () => Guard.NotNullOrWhitespace(input, paramName, errorMessage),
                expectedErrorMessage);
        }

        [Test]
        public void NotNullOrWhitespace_InvalidInputCustomException_ThrowsException()
        {
            string input = null;
            var expectedErrorMessage = "error message";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                () => Guard.NotNullOrWhitespace(input, exception),
                expectedErrorMessage);
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
