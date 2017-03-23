using System;
using System.Runtime.Serialization;
using Core.Data;
using Newtonsoft.Json;

namespace Core.Entities
{
    public class RefreshToken : Entity
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual User User { get; set; }
    }
}