
using DatabaseDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                return Ok(allForms);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }

        [HttpPut("{newDesc},{formID}")]
        public async Task<int> Put(string newDesc, int formID)
        {
            try
            {
                FormsViewModel view = new();
                DAL.Form formOBJ = await view.getOneByID(formID);
                formOBJ.callDesc = newDesc;

                int status = await view.Update(formOBJ);

                if(status != -1){
                    return 1;
                }

                return -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return -1; // something went wrong
            }
        }

    }
}
