using System.Linq;

namespace AndyPike.ORMBattle.ARRepository.Persistence
{
    public interface IQueryCommand<T>
    {
        IQueryable<T> Execute(IQueryable<T> queryable);
    }
}