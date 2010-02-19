using System;
using Castle.ActiveRecord;

namespace AndyPike.ORMBattle.ARBaseClass.Models
{
    [ActiveRecord]
    public class Post : ActiveRecordBase<Post>
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property]
        public string Title { get; set; }
    }
}