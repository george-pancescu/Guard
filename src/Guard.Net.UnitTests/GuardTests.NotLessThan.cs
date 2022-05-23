using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(-1, 0, "", null, "[parameter] cannot be less than 0.")]
        [TestCase(-1, 0, " ", null, "[parameter] cannot be less than 0.")]
        [TestCase(-1, 0, null, null, "[parameter] cannot be less than 0.")]
        public void NotLessThan_InvalidInputDefaultExceptionEmptyParamName_ThrowsException(
            int input,
            int threshold,
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
            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThan(input, threshold, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }
        [Test]
        [TestCase(-1, 0, "paramName", "custom error message", "custom error message")]
        [TestCase(-1, 0, "paramName", null, "[paramName] cannot be less than 0.")]
        public void NotLessThan_InvalidInputDefaultExceptionGivenParamName_ThrowsException(
            int input,
            int threshold,
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
            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThan(input, threshold, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }


        [Test]
        public void NotLessThan_InvalidInputDefaultException2_ThrowsException()
        {
            var input = "a";
            var threshold = "b";

            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThan(input, threshold, nameof(input)));
        }

        [Test]
        public void NotLessThan_InvalidInputDefaultException3_ThrowsException()
        {
            var input = false;
            var threshold = true;

            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThan(input, threshold, nameof(input)));
        }

        [Test]
        public void NotLessThan_InvalidInputCustomException_ThrowsException()
        {
            var input = -1;
            var threshold = 0;
            var expectedErrorMessage = "error message" + ParamNameMessageNet50;
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(() => Guard.NotLessThan(input, threshold, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotLessThan_InvalidInputNullCustomException_ThrowsException()
        {
            var input = -1;
            var threshold = 0;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotLessThan(input, threshold, exception));
        }

        [Test]
        [TestCase(null)]
        [TestCase("custom message")]
        public void NotLessThan_InvalidNullCustomException2_ThrowsException(string message)
        {
            var input = -1;
            var threshold = 0;

            if (message == null)
            {
                Should.Throw<InvalidOperationException>(() => Guard.NotLessThan<int, InvalidOperationException>(input, threshold));
            }
            else
            {
                Should
                    .Throw<InvalidOperationException>(() => Guard.NotLessThan<int, InvalidOperationException>(input, threshold, message))
                    .Message
                    .ShouldBe(message);
            }
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
