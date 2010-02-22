using System.Linq;
using AndyPike.ORMBattle.ARRepository.Models;

namespace AndyPike.ORMBattle.ARRepository.Persistence.Queries
{
    public class UsersOrderedByName : IQueryCommand<User>
    {
        public IQueryable<User> Execute(IQueryable<User> queryable)
        {
            return queryable.OrderBy(u => u.LastName);
        }
    }
}