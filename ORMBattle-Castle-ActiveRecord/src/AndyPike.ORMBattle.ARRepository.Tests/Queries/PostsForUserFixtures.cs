using System.Collections.Generic;
using AndyPike.ORMBattle.ARRepository.Models;
using AndyPike.ORMBattle.ARRepository.Persistence.Queries;
using AndyPike.ORMBattle.ARRepository.Tests.TestSupport;
using NUnit.Framework;

namespace AndyPike.ORMBattle.ARRepository.Tests.Queries
{
    [TestFixture]
    public class When_retrieving_posts_for_a_user : ActiveRecordTestBase
    {
        [Test]
        public void Should_only_return_posts_that_were_created_by_the_specified_user()
        {
            IList<Post> posts = postRepository.Find(new PostsForUser(andy));

            posts.Count.Should().Be.EqualTo(1);
            posts[0].Id.Should().Be.EqualTo(andysPost.Id);
        }
    }
}