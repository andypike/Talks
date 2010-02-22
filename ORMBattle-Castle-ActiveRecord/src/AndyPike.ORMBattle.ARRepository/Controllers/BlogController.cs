using AndyPike.ORMBattle.ARRepository.Models;
using AndyPike.ORMBattle.ARRepository.Persistence;
using AndyPike.ORMBattle.ARRepository.Persistence.Queries;
using Castle.MonoRail.ActiveRecordSupport;
using Castle.MonoRail.Framework;

namespace AndyPike.ORMBattle.ARRepository.Controllers
{
    [Layout("Default")]
    public class BlogController : SmartDispatcherController
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Post> postRepository;

        public BlogController(IRepository<User> userRepository, IRepository<Post> postRepository)
        {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
        }

        public void Index()
        {
            PropertyBag["users"] = userRepository.Find(new UsersOrderedByName());
        }

        public void PostsFor([ARFetch]User user)
        {
            PropertyBag["user"] = user;
            PropertyBag["posts"] = postRepository.Find(new PostsForUser(user));
        }
    }
}