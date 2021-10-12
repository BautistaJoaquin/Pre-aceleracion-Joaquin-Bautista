using Microsoft.AspNetCore.Identity;

namespace preAceleracionDisney.Entities
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
