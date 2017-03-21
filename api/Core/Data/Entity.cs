using System;

namespace Core.Data
{
    public abstract class Entity : IEntity
    {
        public virtual Guid Id { get; set; }
    }
}
