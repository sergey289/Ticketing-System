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

        public async Task<IActionResult> OnPostAddNewUser()
        {

            if (ModelState.IsValid)
            {

                byte[] userImage = default;

                if (NewEmployee.UserImage != null && NewEmployee.UserImage.Length != 0)
                {

                    using (var memoryStream = new MemoryStream())
                    {
                        await NewEmployee.UserImage.CopyToAsync(memoryStream);
                        userImage = memoryStream.ToArray();
                    }
                }

               _service.AddNewEmployee(NewEmployee.UserName, NewEmployee.Password, NewEmployee.FirstName, NewEmployee.LastName, NewEmployee.Email, NewEmployee.Phone, NewEmployee.RoleID, true, userImage);

                return new JsonResult(new { success = true, message = "User added successfully" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Model state is invalid" });

            }          
        }
  
    }
}
