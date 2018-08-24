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
        [TestCase(-1, 0, "paramName", null, "[paramName] cannot be less than or equal to 0.\r\nParameter name: paramName")]
        [TestCase(-1, 0, "", null, "[parameter] cannot be less than or equal to 0.\r\nParameter name: parameter")]
        [TestCase(-1, 0, " ", null, "[parameter] cannot be less than or equal to 0.\r\nParameter name: parameter")]
        [TestCase(-1, 0, null, null, "[parameter] cannot be less than or equal to 0.\r\nParameter name: parameter")]
        [TestCase(0, 0, null, null, "[parameter] cannot be less than or equal to 0.\r\nParameter name: parameter")]
        public void NotLessThanOrEqualTo_InvalidInputDefaultException_ThrowsException(
            int input,
            int threshold,
            string paramName,
            string errorMessage,
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThanOrEqualTo(input, threshold, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        [TestCase("a", "b")]
        [TestCase("a", "a")]
        public void NotLessThanOrEqualTo_InvalidInputDefaultException2_ThrowsException(string input, string threshold)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThanOrEqualTo(input, threshold, nameof(input)));
        }

        [Test]
        [TestCase(-1, 0)]
        [TestCase(0, 0)]
        public void NotLessThanOrEqualTo_InvalidInputNullCustomException_ThrowsException(int input, int threshold)
        {
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotLessThanOrEqualTo(input, threshold, exception));
        }

        [Test]
        [TestCase(-1, 0, null)]
        [TestCase(0, 0, "custom message")]
        public void NotLessThanOrEqualTo_InvalidNullCustomException2_ThrowsException(int input, int threshold, string message)
        {
            if (message == null)
            {
                Should.Throw<InvalidOperationException>(() => Guard.NotLessThanOrEqualTo<int, InvalidOperationException>(input, threshold));
            }
            else
            {
                Should
                    .Throw<InvalidOperationException>(() => Guard.NotLessThanOrEqualTo<int, InvalidOperationException>(input, threshold, message))
                    .Message
                    .ShouldBe(message);
            }
        }

        [Test]
        [TestCase(2, 0)]
        public void NotLessThanOrEqualTo_ValidInput_DoesNotThrowException(int input, int threshold)
        {
            Should.NotThrow(() => Guard.NotLessThanOrEqualTo(input, threshold, nameof(input), null));
        }
    }
}
