using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace AndyPike.Presentations.IntroToTDD.Tests
{
    [TestFixture]
    public class When_reversing_a_string
    {
        private IReverser<string> reverser;
        private ILogger logger;
        private MockRepository mockery;

        [SetUp]
        public void SetUp()
        {
            mockery = new MockRepository();
            logger = mockery.DynamicMock<ILogger>();

            reverser = new StringReverser(logger);
        }

        [Test]
        public void Should_return_the_reverse_of_a_valid_string()
        {
            string result = reverser.Reverse("Hello DevEvening!");

            Assert.That(result, Is.EqualTo("!gninevEveD olleH"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_throw_an_exception_if_a_null_is_passed_in()
        {
            string result = reverser.Reverse(null);
        }

        [Test]
        public void Should_leverage_the_logger_when_reversing_a_string()
        {
            using (mockery.Record())
            {
                Expect.Call(logger.Info("'12345' was reversed.")).Return(true);
            }

            using (mockery.Playback())
            {
                reverser.Reverse("12345");
            }
        }
    }
}