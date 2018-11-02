using Microsoft.AspNetCore.Identity;

namespace WebApi.Entities
{
    public class User : IdentityUser
    {
        public byte[] PasswordSalt { get; set; }
        public new byte[] PasswordHash { get; set; }
    }
}