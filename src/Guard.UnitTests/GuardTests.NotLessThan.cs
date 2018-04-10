using System;
using NUnit.Framework;
using Shouldly;

namespace Guard.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(-1, 0, "paramName", "custom error message", "custom error message")]
        [TestCase(-1, 0, "paramName", null, "[paramName] cannot be Null.")]
        [TestCase(-1, 0, "", null, "[parameter] cannot be Null.")]
        [TestCase(-1, 0, " ", null, "[parameter] cannot be Null.")]
        [TestCase(-1, 0, null, null, "[parameter] cannot be Null.")]
        public void NotLessThan_InvalidInputDefaultException_ThrowsException(
            int input,
            int threshold,
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentOutOfRangeException>(
                () => Guard.NotLessThan(input, threshold, paramName, errorMessage),
                expectedErrorMessage);
        }

        [Test]
        public void NotLessThan_InvalidInputCustomException_ThrowsException()
        {
            int input = -1;
            int threshold = 0;
            var expectedErrorMessage = "error message";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                () => Guard.NotLessThan(input, threshold, exception),
                expectedErrorMessage);
        }

        [Test]
        public void NotLessThan_InvalidInputNullCustomException_ThrowsException()
        {
            int input = -1;
            int threshold = 0;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotLessThan(input, threshold, exception));
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
