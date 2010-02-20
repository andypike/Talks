using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using Castle.Components.Validator;

namespace AndyPike.ORMBattle.ARBaseClass.Models
{
    [ActiveRecord]
    public class Ticket : ActiveRecordValidationBase<Ticket>
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [ValidateNonEmpty]
        [Property(NotNull = true)]
        public string Summary { get; set; }

        [ValidateNonEmpty]
        [Property(NotNull = true, SqlType = "nvarchar(max)")]
        public string Body { get; set; }

        [Property(NotNull = true)]
        public TicketType Type { get; set; }

        [BelongsTo(NotNull = true)]
        public Project Project { get; set; }

        [Property]
        public DateTime CreatedAt { get; set; }

        [BelongsTo(NotNull = true)]
        public User AssignedTo { get; set; }

        public static IList<Ticket> FindBugsFor(User user)
        {
            var criteria = DetachedCriteria.For<Ticket>()
                            .Add(Restrictions.Eq("AssignedTo", user))
                            .Add(Restrictions.Eq("Type", TicketType.Bug));

            return FindAll(criteria);
        }
    }
}