using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        const string ParamNameMessage = " (Parameter 'paramName')";
        const string ParameterMessage = " (Parameter 'parameter')";

        [Test]
        [TestCase(11, 10, "paramName", "custom error message", "custom error message" + ParamNameMessage)]
        [TestCase(11, 10, "paramName", null, "[paramName] cannot be greater than or equal to 10." + ParamNameMessage)]
        [TestCase(11, 10, "", null, "[parameter] cannot be greater than or equal to 10." + ParameterMessage)]
        [TestCase(11, 10, " ", null, "[parameter] cannot be greater than or equal to 10." + ParameterMessage)]
        [TestCase(11, 10, null, null, "[parameter] cannot be greater than or equal to 10." + ParameterMessage)]
        [TestCase(10, 10, null, null, "[parameter] cannot be greater than or equal to 10." + ParameterMessage)]
        public void NotGreaterThanOrEqualTo_InvalidInputDefaultException_ThrowsException(
            int input,
            int threshold,
            string paramName,
            string errorMessage,
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentOutOfRangeException>(
                    () => Guard.NotGreaterThanOrEqualTo(input, threshold, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        [TestCase(11, 10)]
        [TestCase(10, 10)]
        public void NotGreaterThanOrEqualTo_InvalidInputCustomException_ThrowsException(int input, int threshold)
        {
            var expectedErrorMessage = "error message" + ParameterMessage;
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                    () => Guard.NotGreaterThanOrEqualTo(input, threshold, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        [TestCase(11, 10)]
        [TestCase(10, 10)]
        public void NotGreaterThanOrEqualTo_InvalidInputNullCustomException_ThrowsException(int input, int threshold)
        {
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotGreaterThanOrEqualTo(input, threshold, exception));
        }

        [Test]
        [TestCase(11, 10, null)]
        [TestCase(10, 10, null)]
        [TestCase(11, 10, "custom message")]
        [TestCase(10, 10, "custom message")]
        public void NotGreaterThanOrEqualTo_InvalidNullCustomException2_ThrowsException(int input, int threshold, string message)
        {
            if (message == null)
            {
                Should.Throw<InvalidOperationException>(() => Guard.NotGreaterThanOrEqualTo<int, InvalidOperationException>(input, threshold));
            }
            else
            {
                Should
                    .Throw<InvalidOperationException>(() => Guard.NotGreaterThanOrEqualTo<int, InvalidOperationException>(input, threshold, message))
                    .Message
                    .ShouldBe(message);
            }
        }

        [Test]
        [TestCase(9, 10)]
        public void NotGreaterThanOrEqualTo_ValidInput_DoesNotThrowException(int input, int threshold)
        {
            Should.NotThrow(() => Guard.NotGreaterThanOrEqualTo(input, threshold, nameof(input), null));
        }
    }
}
