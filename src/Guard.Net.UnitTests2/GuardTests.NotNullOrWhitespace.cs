using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(null, "paramName", "custom error message", "custom error message" + ParamNameMessage)]
        [TestCase(null, "paramName", null, "[paramName] cannot be Null, empty or white-space." + ParamNameMessage)]
        [TestCase(null, "", null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
        [TestCase(null, " ", null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
        [TestCase(null, null, null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
        [TestCase("", "paramName", "custom error message", "custom error message" + ParamNameMessage)]
        [TestCase("", "paramName", null, "[paramName] cannot be Null, empty or white-space." + ParamNameMessage)]
        [TestCase("", "", null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
        [TestCase("", " ", null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
        [TestCase("", null, null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
        [TestCase(" ", "paramName", "custom error message", "custom error message" + ParamNameMessage)]
        [TestCase(" ", "paramName", null, "[paramName] cannot be Null, empty or white-space." + ParamNameMessage)]
        [TestCase(" ", "", null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
        [TestCase(" ", " ", null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
        [TestCase(" ", null, null, "[parameter] cannot be Null, empty or white-space." + ParameterMessage)]
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
            var expectedErrorMessage = "error message" + ParameterMessage;
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
            {
                Should.Throw<InvalidOperationException>(() => Guard.NotNullOrWhitespace<InvalidOperationException>(input));
            }
            else
            {
                Should
                    .Throw<InvalidOperationException>(() => Guard.NotNullOrWhitespace<InvalidOperationException>(input, message))
                    .Message
                    .ShouldBe(message);
            }
        }

        [Test]
        public void NotNullOrWhitespace_ValidInput_DoesNotThrowException()
        {
            var input = "input";

            Should.NotThrow(() => Guard.NotNullOrWhitespace(input, nameof(input), null));
        }
    }
}
