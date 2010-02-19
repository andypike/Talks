using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Linq;

namespace AndyPike.ORMBattle.ARBaseClass.Models
{
    [ActiveRecord("`User`")]
    public class User : ActiveRecordLinqBase<User>
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Version]
        public int Version { get; set; }

        [Property(NotNull = true)]
        public string Name { get; set; }

        [Property(NotNull = true)]
        public string Email { get; set; }
    }
}