using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Shouldly;

namespace GuardNet.UnitTests
{
    [TestFixture]
    public partial class GuardTests
    {
        [Test]
        [TestCase("123", "123")]
        [TestCase("", "")]
        [TestCase("  ", " ")]
        [TestCase(" ", " ")]
        [TestCase("123", @"\d+")]
        [TestCase("12", @"\d{2}")]
        [TestCase("12asdasd", @"\.*")]
        [TestCase("my-us3r_n4m3", @"^[a-z0-9_-]{3,16}$")]
        [TestCase("john@doe.com", @"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$")]
        [TestCase("Aa", @"aa", RegexOptions.IgnoreCase)]
        public void NotIsMatch_ValidInput_DoesNotThrowException(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            Exception exception = new Exception();

            Should.NotThrow(() => Guard.NotIsMatch(input, pattern, exception, options));
        }

        [Test]
        [TestCase("123", "1234")]
        [TestCase("", " ")]
        [TestCase("  ", "   ")]
        [TestCase("123", @"\d{4}")]
        [TestCase("th1s1s-wayt00_l0ngt0beausername", @"^[a-z0-9_-]{3,16}$")]
        [TestCase("john@doe.something", @"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$")]
        [TestCase("Aa", @"aa")]
        public void NotIsMatch_InvalidInput_ThrowsException(string input, string pattern)
        {
            Exception exception = new Exception();

            Should.Throw<Exception>(() => Guard.NotIsMatch(input, pattern, exception));
        }

        [Test]
        public void NotIsMatch_InvalidInputNullException_ThrowsException()
        {
            string input = "";
            string pattern = "";
            Exception exception = null;

            Should.Throw<ArgumentNullException>(() => Guard.NotIsMatch(input, pattern, exception));
        }
    }
}
