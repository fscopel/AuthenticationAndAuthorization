using System;

namespace VirtualHotel.Data
{
    public class IdentityUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; internal set; }
        public AuthorizedPersonType AuthorizedPersonType { get; set; }
        
    }
}
