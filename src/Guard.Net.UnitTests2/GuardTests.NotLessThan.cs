using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase(-1, 0, "paramName", "custom error message", "custom error message" + ParamNameMessage)]
        [TestCase(-1, 0, "paramName", null, "[paramName] cannot be less than 0." + ParamNameMessage)]
        [TestCase(-1, 0, "", null, "[parameter] cannot be less than 0." + ParameterMessage)]
        [TestCase(-1, 0, " ", null, "[parameter] cannot be less than 0." + ParameterMessage)]
        [TestCase(-1, 0, null, null, "[parameter] cannot be less than 0." + ParameterMessage)]
        public void NotLessThan_InvalidInputDefaultException_ThrowsException(
            int input,
            int threshold,
            string paramName, 
            string errorMessage, 
            string expectedErrorMessage)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThan(input, threshold, paramName, errorMessage))
                .Message
                .ShouldBe(expectedErrorMessage);
        }

        [Test]
        public void NotLessThan_InvalidInputDefaultException2_ThrowsException()
        {
            string input = "a";
            string threshold = "b";
            
            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThan(input, threshold, nameof(input)));
        }

        [Test]
        public void NotLessThan_InvalidInputDefaultException3_ThrowsException()
        {
            bool input = false;
            bool threshold = true;

            Should.Throw<ArgumentOutOfRangeException>(() => Guard.NotLessThan(input, threshold, nameof(input)));
        }

        [Test]
        public void NotLessThan_InvalidInputCustomException_ThrowsException()
        {
            int input = -1;
            int threshold = 0;
            var expectedErrorMessage = "error message" + ParamNameMessage;
            var exception = new Exception(expectedErrorMessage);

            Should.Throw<Exception>(() => Guard.NotLessThan(input, threshold, exception))
                .Message
                .ShouldBe(expectedErrorMessage);
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
        [TestCase(null)]
        [TestCase("custom message")]
        public void NotLessThan_InvalidNullCustomException2_ThrowsException(string message)
        {
            int input = -1;
            int threshold = 0;

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
