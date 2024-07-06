using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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

        [BindProperty]
        public Ticket NewTicket { get; set; }

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

        public IActionResult OnGetPriorities()
        {
            try
            {
                var dataSet = _service.GetPriorities();
                var rolesData = Utils.DataTableToList(dataSet.Tables[0]);
                return new JsonResult(rolesData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult OnGetIssueTypes()
        {
            try
            {
                var dataSet = _service.GetIssueTypes();
                var rolesData = Utils.DataTableToList(dataSet.Tables[0]);
                return new JsonResult(rolesData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult OnGetApplicationList()
        {
            try
            {
                var dataSet = _service.GetApplications();
                var rolesData = Utils.DataTableToList(dataSet.Tables[0]);
                return new JsonResult(rolesData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult OnGetDepartments()
        {
            try
            {
                var dataSet = _service.GetDepartments();
                var rolesData = Utils.DataTableToList(dataSet.Tables[0]);
                return new JsonResult(rolesData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> OnPostAddNewUser(Employee employee)
        {
            if ((TryValidateModel(employee)))
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
       
        public async Task<IActionResult> OnPostAddNewTicket(Ticket NewTicket)
        {

            try
            {

                ModelState.Clear();

                if ((TryValidateModel(NewTicket)))
                {

                    byte[] ticketFile = default;

                    int employeeID = default;


                    string employeeJson = HttpContext.Session.GetString("Employee");

                    if (!string.IsNullOrEmpty(employeeJson))
                    {
                        var employee = JsonConvert.DeserializeObject<Employee>(employeeJson);
                        employeeID = employee.EmployeeID;
                    }

                    if (NewTicket.TicketFile != null && NewTicket.TicketFile.Length != 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await NewTicket.TicketFile.CopyToAsync(memoryStream);
                            ticketFile = memoryStream.ToArray();
                        }
                    }

                    _service.AddNewTicket(employeeID, NewTicket.TicketTitle, NewTicket.ApplicationID, NewTicket.DepartmentID, NewTicket.PriorityID, NewTicket.TypeTreatmentID, ticketFile, NewTicket.Description);

                    return new JsonResult(new { success = true, message = "Ticket added successfully" });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "Model state is invalid" });
                }


            }catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = "Model state is invalid" });
            }
        }
    }
}