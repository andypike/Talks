using System.Linq;
using AndyPike.ORMBattle.ARRepository.Models;

namespace AndyPike.ORMBattle.ARRepository.Persistence.Queries
{
    public class PostsForUser : IQueryCommand<Post>
    {
        private readonly User user;

        public PostsForUser(User user)
        {
            this.user = user;
        }

        public IQueryable<Post> Execute(IQueryable<Post> queryable)
        {
            return queryable.Where(p => p.CreatedBy == user);
        }
    }
}