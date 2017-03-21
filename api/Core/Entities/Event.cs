using System;
using Core.Data;

namespace Core.Entities
{
    public class Event : Entity
    {
        public string Name { get; set; }
        public Enums.Enums.EventType EventType { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
    }
}