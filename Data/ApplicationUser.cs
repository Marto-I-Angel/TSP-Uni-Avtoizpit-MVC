using Microsoft.AspNetCore.Identity;

namespace TSP_Uni_Listovki.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationUser()
        {

        }
    }
}