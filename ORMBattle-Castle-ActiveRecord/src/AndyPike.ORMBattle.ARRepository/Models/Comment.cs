using System;
using AndyPike.ORMBattle.ARRepository.Persistence;
using Castle.ActiveRecord;

namespace AndyPike.ORMBattle.ARRepository.Models
{
    [ActiveRecord]
    public class Comment : IEntity
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property(SqlType = "nvarchar(max)")]
        public string Text { get; set; }

        [BelongsTo]
        public User CreatedBy { get; set; }

        [BelongsTo]
        public Post Post { get; set; }
    }
}