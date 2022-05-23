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
            var input = 11;
            var @value = 10;
            var exception = new Exception();

            Should.Throw<Exception>(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        [TestCase(11, "11")]
        [TestCase(null, "11")]
        [TestCase("11", null)]        
        public void NotEqualTo_InvalidInput2_ThrowsException(object input, object @value)
        {
            var exception = new Exception();

            Should.Throw<Exception>(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        public void NotEqualTo_ValidInput_DoesNotThrowException()
        {
            var input = 10;
            var @value = 10;
            var exception = new Exception();

            Should.NotThrow(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        public void NotEqualTo_ValidInput2_DoesNotThrowException()
        {
            var input = "11";
            var @value = "11";
            var exception = new Exception();

            Should.NotThrow(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        [TestCase(null, null)]
        [TestCase(11, 11)]
        [TestCase("11", "11")]
        public void NotEqualTo_ValidInput3_DoesNotThrowException(object input, object @value)
        {
            var exception = new Exception();

            Should.NotThrow(() => Guard.NotEqualTo(input, @value, exception));
        }

        [Test]
        public void NotEqualTo_InvalidInputNullCustomException_ThrowsException()
        {
            var input = 11;
            var @value = 10;
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotEqualTo(input, @value, exception));
        }
    }
}
