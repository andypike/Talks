using System;

namespace AndyPike.ORMBattle.ARRepository.Persistence
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}