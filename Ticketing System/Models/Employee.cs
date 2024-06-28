using System.ComponentModel.DataAnnotations;

namespace Ticketing_Systems.Models
{
    public class Employee
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public IFormFile UserImage { get; set; }

    }
}
