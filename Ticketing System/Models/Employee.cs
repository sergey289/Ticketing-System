using System.ComponentModel.DataAnnotations;

namespace Ticketing_Systems.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "שדה שם המשתמש הוא שדה חובה")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "שדה סיסמא הוא שדה חובה")]
        public string Password { get; set; }

        [Required(ErrorMessage = "שדה שם פרטי הוא שדה חובה")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "שדה שם משפחה הוא שדה חובה")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "שדה טלפון הוא שדה חובה")]
        public string Phone { get; set; }

        public int RoleID { get; set; }

        public IFormFile UserImage { get; set; }

    }
}
