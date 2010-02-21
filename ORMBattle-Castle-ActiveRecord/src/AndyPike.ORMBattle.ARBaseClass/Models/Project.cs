using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace AndyPike.ORMBattle.ARBaseClass.Models
{
    [ActiveRecord]
    public class Project : ActiveRecordValidationBase<Project>
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property]
        public string Name { get; set; }

        [HasMany]
        public IList<Ticket> Tickets { get; set; }

        public static IList<Project> FindAllOrderedByName()
        {
            return FindAll(new Order("Name", true));
        }
    }
}