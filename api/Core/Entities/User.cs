using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Data;
using Newtonsoft.Json;

namespace Core.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<RefreshToken> RefreshTokens{ get; set; }
    }
}
