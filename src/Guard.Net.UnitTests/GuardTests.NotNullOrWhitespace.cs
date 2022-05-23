using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(null, "", null, "[parameter] cannot be Null, empty or white-space.")]
        [TestCase(null, " ", null, "[parameter] cannot be Null, empty or white-space.")]
        [TestCase(null, null, null, "[parameter] cannot be Null, empty or white-space.")]
        [TestCase("", "", null, "[parameter] cannot be Null, empty or white-space.")]
        [TestCase("", " ", null, "[parameter] cannot be Null, empty or white-space.")]
        [TestCase("", null, null, "[parameter] cannot be Null, empty or white-space.")]
        [TestCase(" ", "", null, "[parameter] cannot be Null, empty or white-space.")]
        [TestCase(" ", " ", null, "[parameter] cannot be Null, empty or white-space.")]
        [TestCase(" ", null, null, "[parameter] cannot be Null, empty or white-space.")]
        public void NotNullOrWhitespace_InvalidInputDefaultExceptionEmptyParamName_ThrowsException(
            string input,
            string paramName,
            string errorMessage,
            string expectedErrorMessageBase)
        {
            string expectedErrorMessage = null;
#if NET5_0_OR_GREATER
            expectedErrorMessage = expectedErrorMessageBase + ParameterMessageNet50;
#else
            expectedErrorMessage = expectedErrorMessageBase + Environment.NewLine + ParameterMessageNet4X;
#endif
            Should.Throw<ArgumentException>(
                    () => Guard.NotNullOrWhitespace(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        [TestCase(null, "paramName", "custom error message", "custom error message")]
        [TestCase(null, "paramName", null, "[paramName] cannot be Null, empty or white-space.")]
        [TestCase("", "paramName", "custom error message", "custom error message")]
        [TestCase("", "paramName", null, "[paramName] cannot be Null, empty or white-space.")]
        [TestCase(" ", "paramName", "custom error message", "custom error message")]
        [TestCase(" ", "paramName", null, "[paramName] cannot be Null, empty or white-space.")]
        public void NotNullOrWhitespace_InvalidInputDefaultExceptionGivenParamName_ThrowsException(
            string input,
            string paramName,
            string errorMessage,
            string expectedErrorMessageBase)
        {
            string expectedErrorMessage = null;
#if NET5_0_OR_GREATER
            expectedErrorMessage = expectedErrorMessageBase + ParamNameMessageNet50;
#else
            expectedErrorMessage = expectedErrorMessageBase + Environment.NewLine + ParamNameMessageNet4X;
#endif
            Should.Throw<ArgumentException>(
                    () => Guard.NotNullOrWhitespace(input, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotNullOrWhitespace_InvalidInputCustomException_ThrowsException()
        {
            string input = null;
            var expectedErrorMessage = "error message" + ParameterMessageNet50;
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
        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(null, "custom message")]
        [TestCase("", "custom message")]
        public void NotNullOrWhitespace_InvalidNullCustomException2_ThrowsException(string input, string message)
        {
            if (message == null)
                Should.Throw<InvalidOperationException>(() =>
                    Guard.NotNullOrWhitespace<InvalidOperationException>(input));
            else
                Should
                    .Throw<InvalidOperationException>(() =>
                        Guard.NotNullOrWhitespace<InvalidOperationException>(input, message))
                    .Message
                    .ShouldBe(message);
        }

        [Test]
        public void NotNullOrWhitespace_ValidInput_DoesNotThrowException()
        {
            var input = "input";

            Should.NotThrow(() => Guard.NotNullOrWhitespace(input, nameof(input), null));
        }
    }
}