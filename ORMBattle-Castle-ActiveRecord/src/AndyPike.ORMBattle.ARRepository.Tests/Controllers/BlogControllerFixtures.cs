using System.Collections.Generic;
using AndyPike.ORMBattle.ARRepository.Controllers;
using AndyPike.ORMBattle.ARRepository.Models;
using AndyPike.ORMBattle.ARRepository.Persistence;
using AndyPike.ORMBattle.ARRepository.Persistence.Queries;
using Castle.MonoRail.TestSupport;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino_Is = Rhino.Mocks.Constraints.Is;

namespace AndyPike.ORMBattle.ARRepository.Tests.Controllers
{
    [TestFixture]
    public class When_showing_the_blog_homepage : BlogControllerFixturesBase
    {
        [Test]
        public void Should_leverage_the_user_repository()
        {
            userRepository.Expect(r => r.Find(null))
                .IgnoreArguments()
                .Constraints(Rhino_Is.TypeOf(typeof(UsersOrderedByName)))
                .Return(users);

            controller.Index();

            userRepository.VerifyAllExpectations();
        }

        [Test]
        public void Should_populate_the_property_bag_with_the_users_list()
        {
            userRepository.Stub(r => r.Find(null)).IgnoreArguments().Return(users);

            controller.Index();

            controller.PropertyBag["users"].Should().Be.EqualTo(users);
        }
    }

    [TestFixture]
    public class When_showing_the_posts_for_a_user : BlogControllerFixturesBase
    {
        private readonly User user = new User();

        [Test]
        public void Should_leverage_the_post_repository()
        {
            postRepository.Expect(r => r.Find(null))
                .IgnoreArguments()
                .Constraints(Rhino_Is.TypeOf(typeof(PostsForUser)))
                .Return(posts);

            controller.PostsFor(user);

            postRepository.VerifyAllExpectations();
        }

        [Test]
        public void Should_populate_the_property_bag_with_the_posts_list()
        {
            postRepository.Stub(r => r.Find(null)).IgnoreArguments().Return(posts);

            controller.PostsFor(user);

            controller.PropertyBag["posts"].Should().Be.EqualTo(posts);
        }

        [Test]
        public void Should_populate_the_property_bag_with_the_specified_user()
        {
            controller.PostsFor(user);

            controller.PropertyBag["user"].Should().Be.EqualTo(user);
        }
    }

    public abstract class BlogControllerFixturesBase : BaseControllerTest
    {
        protected BlogController controller;
        protected IRepository<User> userRepository;
        protected IRepository<Post> postRepository;
        protected IList<User> users;
        protected IList<Post> posts;

        [SetUp]
        public void SetUp()
        {
            userRepository = MockRepository.GenerateMock<IRepository<User>>();
            postRepository = MockRepository.GenerateMock<IRepository<Post>>();

            controller = new BlogController(userRepository, postRepository);
            PrepareController(controller, "Blog");

            users = new[] { new User() };
            posts = new[] { new Post() };
        }
    }
}