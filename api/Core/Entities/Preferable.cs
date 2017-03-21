using System;
using System.Collections.Generic;
using Core.Data;

namespace Core.Entities
{
    public class Preferable : Entity
    {
        public Guid UserId { get; set; }
        public IEnumerable<Enums.Enums.EventType> EventTypes { get; set; }
    }
}