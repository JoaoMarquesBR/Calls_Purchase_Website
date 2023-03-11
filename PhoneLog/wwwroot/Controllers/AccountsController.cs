using DAL;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics;
using System.Reflection;
using ViewModels;

namespace TheFactory_PhoneForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> getAllSupervisors()
        {
            try
            {
                ProfilesViewModels viewmodel = new();
                List<Account> allSupervisors = await viewmodel.getAccountByPermission("Administrator");
                return Ok(allSupervisors);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }

    }
}
