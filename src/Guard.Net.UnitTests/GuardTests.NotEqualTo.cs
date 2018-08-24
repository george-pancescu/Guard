using System;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        public void NotEqualTo_InvalidInput_ThrowsException()
        {
            int input = 11;
            int @value = 10;
            Exception exception = new Exception();

            Should.Throw<Exception>(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        [TestCase(11, "11")]
        [TestCase(null, "11")]
        [TestCase("11", null)]        
        public void NotEqualTo_InvalidInput2_ThrowsException(object input, object @value)
        {
            Exception exception = new Exception();

            Should.Throw<Exception>(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        public void NotEqualTo_ValidInput_DoesNotThrowException()
        {
            int input = 10;
            int @value = 10;
            Exception exception = new Exception();

            Should.NotThrow(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        public void NotEqualTo_ValidInput2_DoesNotThrowException()
        {
            string input = "11";
            string @value = "11";
            Exception exception = new Exception();

            Should.NotThrow(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        [TestCase(null, null)]
        [TestCase(11, 11)]
        [TestCase("11", "11")]
        public void NotEqualTo_ValidInput3_DoesNotThrowException(object input, object @value)
        {
            Exception exception = new Exception();

            Should.NotThrow(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        public void NotEqualTo_InvalidInputNullCustomException_ThrowsException()
        {
            int input = 11;
            int @value = 10;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotEqualTo(input, @value, exception));
        }
    }
}
