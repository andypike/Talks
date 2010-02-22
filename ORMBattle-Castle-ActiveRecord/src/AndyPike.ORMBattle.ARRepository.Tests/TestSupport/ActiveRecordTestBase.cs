using System.Reflection;
using AndyPike.ORMBattle.ARRepository.Models;
using AndyPike.ORMBattle.ARRepository.Persistence;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using NUnit.Framework;

namespace AndyPike.ORMBattle.ARRepository.Tests.TestSupport
{
    public abstract class ActiveRecordTestBase
    {
        protected SessionScope scope;
        protected IRepository<User> userRepository;
        protected IRepository<Post> postRepository;

        protected User andy;
        protected User george;
        protected User jonathan;
        protected Post andysPost;
        protected Post georgesPost;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            ActiveRecordStarter.ResetInitializationFlag();
            IConfigurationSource source = new XmlConfigurationSource("Activerecord.config");
            ActiveRecordStarter.Initialize(Assembly.GetAssembly(typeof(User)), source);
        }

        [SetUp]
        public virtual void SetUp()
        {
            ActiveRecordStarter.DropSchema();
            ActiveRecordStarter.CreateSchema();
            scope = new SessionScope();

            userRepository = new ActiveRecordRepository<User>();
            postRepository = new ActiveRecordRepository<Post>();

            PopulateUsers();
            PopulatePosts();

            Flush();
        }

        private void PopulateUsers()
        {
            andy = new User{ FirstName = "Andy", LastName = "Pike" };
            george = new User{ FirstName = "George", LastName = "Good" };
            jonathan = new User{ FirstName = "Jonathan", LastName = "Hill" };

            userRepository.Save(andy);
            userRepository.Save(george);
            userRepository.Save(jonathan);
        }

        private void PopulatePosts()
        {
            andysPost = new Post{ Title = "Andy created this post ", CreatedBy = andy };
            georgesPost = new Post{ Title = "George created this post ", CreatedBy = george };

            postRepository.Save(andysPost);
            postRepository.Save(georgesPost);
        }

        [TearDown]
        public virtual void TearDown()
        {
            scope.Dispose();
        }

        public void Flush()
        {
            scope.Flush();
            scope.Dispose();
            scope = new SessionScope();
        }
    }
}