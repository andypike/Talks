using System;
using Castle.ActiveRecord;

namespace AndyPike.ORMBattle.ARBaseClass.Models
{
    [ActiveRecord("`User`")]
    public class User : ActiveRecordValidationBase<User>
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(NotNull = true)]
        public string Name { get; set; }
    }
}