using System;
using NUnit.Framework;
using Shouldly;

namespace Guard.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(11, 10, "paramName", "custom error message", "custom error message")]
        [TestCase(11, 10, "paramName", null, "[paramName] cannot be Null.")]
        [TestCase(11, 10, "", null, "[parameter] cannot be Null.")]
        [TestCase(11, 10, " ", null, "[parameter] cannot be Null.")]
        [TestCase(11, 10, null, null, "[parameter] cannot be Null.")]
        public void NotGreaterThan_InvalidInputDefaultException_ThrowsException(
            int input,
            int threshold,
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentException>(
                () => Guard.NotGreaterThan(input, threshold, paramName, errorMessage),
                expectedErrorMessage);
        }

        [Test]
        public void NotGreaterThan_InvalidInputCustomException_ThrowsException()
        {
            int input = 11;
            int threshold = 10;
            var expectedErrorMessage = "error message";
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(
                () => Guard.NotGreaterThan(input, threshold, exception),
                expectedErrorMessage);
        }

        [Test]
        public void NotGreaterThan_InvalidInputNullCustomException_ThrowsException()
        {
            int input = 11;
            int threshold = 10;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotGreaterThan(input, threshold, exception));
        }

        [Test]
        [TestCase(9, 10)]
        [TestCase(10, 10)]
        public void NotGreaterThan_ValidInput_DoesNotThrowException(int input, int threshold)
        {
            Should.NotThrow(() => Guard.NotGreaterThan(input, threshold, nameof(input), null));
        }
    }
}
