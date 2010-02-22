using System.Collections.Generic;
using AndyPike.ORMBattle.ARRepository.Models;
using AndyPike.ORMBattle.ARRepository.Persistence.Queries;
using AndyPike.ORMBattle.ARRepository.Tests.TestSupport;
using NUnit.Framework;

namespace AndyPike.ORMBattle.ARRepository.Tests.Queries
{
    [TestFixture]
    public class When_retrieving_a_list_of_users : ActiveRecordTestBase
    {
        [Test]
        public void Should_return_them_in_the_correct_order()
        {
            IList<User> users = userRepository.Find(new UsersOrderedByName());

            users.Count.Should().Be.EqualTo(3);
            users[0].Id.Should().Be.EqualTo(george.Id);
            users[1].Id.Should().Be.EqualTo(jonathan.Id);
            users[2].Id.Should().Be.EqualTo(andy.Id);
        }
    }
}