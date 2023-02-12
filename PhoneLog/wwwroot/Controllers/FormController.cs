
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using ViewModels;

namespace TheFactory_PhoneForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {

   
        [HttpPost]
        public async Task<ActionResult> Post(FormsViewModel viewmodel)
        {
            try
            {
                Console.WriteLine("Viel model as \n\n\n\n\n\n\n\n\n\n");
                await viewmodel.addForm();
                return viewmodel.formID > 1
                ? Ok(new { msg = "Form " + viewmodel.formID + " added!" })
                : Ok(new { msg = "Form " + viewmodel.formID + " not added!" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }

        }

        [HttpGet("{accountID}")]
        public async Task<IActionResult> GetAll(int accountID)
        {
            try
            {
                FormsViewModel viewmodel = new();
                List<FormsViewModel> allForms = await viewmodel.GetAll(accountID);
                return Ok(allForms  );
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
