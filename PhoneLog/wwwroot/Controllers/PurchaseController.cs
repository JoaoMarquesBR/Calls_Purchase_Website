
using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using ViewModels;

namespace TheFactory_PhoneForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

   
        [HttpPost]
        public async Task<ActionResult> Post(PurchaseViewModel viewmodel)
        {
            try
            {
                Console.WriteLine("Viel model as \n\n\n\n\n\n\n\n\n\n");
                await viewmodel.addPurchase();
                return viewmodel.Purchase_ID >= 0
                ? Ok(new { msg = "Form " + viewmodel.Purchase_ID + " added!" })
                : Ok(new { msg = "Form " + viewmodel.Purchase_ID + " not added!" });
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
                PurchaseViewModel viewmodel = new();
                List<PurchaseViewModel> allForms = await viewmodel.GetAll(accountID);
                Debug.WriteLine("\n\n\n Count is " + allForms.Count);
                return Ok(allForms);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // something went wrong
            }
        }

        //update password
        [HttpPut("{status},{id},{accountID}")]
        public async Task<ActionResult> updateStatus(string status,int id,int accountID)
        {
            try
            {
                PurchaseViewModel viewmodel = new() { Purchase_ID = id };
                Purchase acc = await viewmodel.getPurchaseByID(id);

                if (acc != null)
                {

                    viewmodel.Purchase_ID = acc.Purchase_ID;
                    viewmodel.accountID = acc.accountID;
                    viewmodel.purchaseDate = acc.purchaseDate;
                    viewmodel.supplier = acc.supplier;
                    viewmodel.quantity = acc.quantity;
                    viewmodel.productPrice = acc.productPrice;
                    viewmodel.tax = acc.tax;
                    viewmodel.net = acc.net;
                    viewmodel.totalAfterTax = acc.totalAfterTax;
                    viewmodel.reference = acc.reference;
                    viewmodel.status = status;
                    viewmodel.accountID_approver = accountID;
                    


                    int result = await viewmodel.UpdateStatus();

                    return Ok(result);
                    //return Ok(-1);

                }



            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return Ok(-1);
        }


        [HttpGet("[action]/{purchaseID}")]
        public async Task<IActionResult> getByID(int purchaseID)
        {
            try
            {
                PurchaseViewModel viewmodel = new();
                Purchase allForms = await viewmodel.getPurchaseByID(purchaseID);
                return Ok(allForms);
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
