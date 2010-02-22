using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Linq;

namespace AndyPike.ORMBattle.ARRepository.Persistence
{
    public class ActiveRecordRepository<T> : IRepository<T> where T : class, IEntity
    {
        public T FindById(Guid id)
        {
            return ActiveRecordMediator<T>.FindByPrimaryKey(id);
        }

        public void Save(T entity)
        {
            ActiveRecordMediator<T>.Save(entity);
        }
        
        public IQueryable<T> AsQueryable()
        {
            return ActiveRecordLinq.AsQueryable<T>();
        }

        public IList<T> Find(IQueryCommand<T> queryCommand)
        {
            return queryCommand.Execute(AsQueryable()).ToList();
        }
    }
}