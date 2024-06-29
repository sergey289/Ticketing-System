using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Data;
using System.Numerics;
using System.Reflection;
using Ticketing_Systems.Models;
using TicketingSystemService.Data;

namespace Ticketing_Systems.Pages
{

    public class MainModel : PageModel
    {

        private readonly TicketingSystemService.TicketingSystemService _service;

        [BindProperty]
        public Employee NewEmployee { get; set; }

        public MainModel(TicketingSystemService.TicketingSystemService service)
        {
            _service = service;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {

        }

        public IActionResult OnGetRolesData()
        {
            try
            {
                var dataSet = _service.GetRolesData();
                var rolesData = Utils.DataTableToList(dataSet.Tables[0]);
                return new JsonResult(rolesData);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<IActionResult> OnPostAddNewUser(Employee person)
        {

            if (string.IsNullOrEmpty(NewEmployee.UserName))
            {
                ModelState.AddModelError("NewEmployee.UserName", "שDSDSDSDSDSDSSDת");
                return Page();
            }

            if (ModelState.IsValid)
            {

                byte[] userImage = default;

                if (person.UserImage != null && person.UserImage.Length != 0)
                {

                    using (var memoryStream = new MemoryStream())
                    {
                        await person.UserImage.CopyToAsync(memoryStream);
                        userImage = memoryStream.ToArray();
                    }
                }

                _service.AddNewEmployee(person.UserName, person.Password, person.FirstName, person.LastName, person.Email, person.Phone, person.RoleID, true, userImage);
            }
            else
            {

               
            }
            return RedirectToPage("/SuccessPage");
        }
  
    }
}
