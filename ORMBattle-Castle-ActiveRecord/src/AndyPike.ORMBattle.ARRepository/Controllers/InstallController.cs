using AndyPike.ORMBattle.ARRepository.Models;
using AndyPike.ORMBattle.ARRepository.Persistence;
using Castle.ActiveRecord;
using Castle.MonoRail.Framework;

namespace AndyPike.ORMBattle.ARRepository.Controllers
{
    [Layout("Default")]
    public class InstallController : SmartDispatcherController
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Post> postRepository;

        public InstallController(IRepository<User> userRepository, IRepository<Post> postRepository)
        {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
        }

        public void Index()
        {
            ActiveRecordStarter.CreateSchema();

            var andy = new User{ FirstName = "Andy", LastName = "Pike" };
            userRepository.Save(andy);

            var george = new User{ FirstName = "George", LastName = "Good" };
            userRepository.Save(george);

            var firstPost = new Post{ Title = "My First Post", Body = "This is the body of my post", CreatedBy = andy };
            postRepository.Save(firstPost);

            var secondPost = new Post { Title = "My Second Post", Body = "This is another one of my posts", CreatedBy = andy };
            postRepository.Save(secondPost);

            var thirdPost = new Post { Title = "My Third Post", Body = "Yet another post", CreatedBy = george };
            postRepository.Save(thirdPost);
        }
    }
}