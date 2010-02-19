using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Linq;
using NHibernate.Criterion;

namespace AndyPike.ORMBattle.ARBaseClass.Models
{
    [ActiveRecord]
    public class Ticket : ActiveRecordLinqBase<Ticket>
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Version]
        public int Version { get; set; }

        [Property(NotNull = true)]
        public string Summary { get; set; }

        [Property(NotNull = true, SqlType = "nvarchar(max)")]
        public string Body { get; set; }

        [Property(NotNull = true)]
        public TicketType Type { get; set; }

        [BelongsTo(NotNull = true)]
        public User CreatedBy { get; set; }

        [Property]
        public DateTime CreatedAt { get; set; }

        [BelongsTo(NotNull = true)]
        public User AssignedTo { get; set; }

        public static IList<Ticket> AllTicketsOrderedByDate()
        {
            return FindAll(new Order("CreatedAt", false));
        }

        public static IList<Ticket> FindBugsFor(User user)
        {
            return Queryable.Where(t => t.AssignedTo == user && t.Type == TicketType.Bug).ToList();
        }
    }
}