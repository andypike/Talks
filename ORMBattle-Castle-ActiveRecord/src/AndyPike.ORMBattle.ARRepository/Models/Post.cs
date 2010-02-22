using System;
using System.Collections.Generic;
using AndyPike.ORMBattle.ARRepository.Persistence;
using Castle.ActiveRecord;

namespace AndyPike.ORMBattle.ARRepository.Models
{
    [ActiveRecord]
    public class Post : IEntity
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property]
        public string Title { get; set; }

        [Property(SqlType = "nvarchar(max)")]
        public string Body { get; set; }

        [BelongsTo]
        public User CreatedBy { get; set; }

        [HasMany]
        public ICollection<Comment> Comments { get; set; }
    }
}