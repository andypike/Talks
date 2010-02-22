using System;
using System.Collections.Generic;

namespace AndyPike.ORMBattle.ARRepository.Persistence
{
    public interface IRepository<T> where T : IEntity
    {
        T FindById(Guid id);

        IList<T> Find(IQueryCommand<T> queryCommand);

        void Save(T entity);
    }
}