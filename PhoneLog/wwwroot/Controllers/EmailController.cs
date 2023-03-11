
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
    public class EmailController : ControllerBase
    {


        [HttpPost("{emailReceiver}")]
        public async Task<int> Post(PurchaseViewModel purchaseView, string emailReceiver)
        {
            try
            {
                EmailViewModels emailView = new();
                emailView.emailReceiver = emailReceiver;
                emailView.purchase = purchaseView;

                await emailView.sendEmail();
                return 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return -1; // something went wrong
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
