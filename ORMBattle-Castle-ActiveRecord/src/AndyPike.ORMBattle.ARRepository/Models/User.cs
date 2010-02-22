using System;
using AndyPike.ORMBattle.ARRepository.Persistence;
using Castle.ActiveRecord;

namespace AndyPike.ORMBattle.ARRepository.Models
{
    [ActiveRecord("`User`")]
    public class User : IEntity
    {
        [PrimaryKey(PrimaryKeyType.GuidComb)]
        public Guid Id { get; set; }

        [Property]
        public string FirstName { get; set; }

        [Property]
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}