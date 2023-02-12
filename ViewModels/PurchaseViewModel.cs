using DAL;
using DatabaseDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PurchaseViewModel
    {

        readonly private PurchaseDAO daoForm;

        public int Purchase_ID { get; set; }

        public int? accountID { get; set; }

        public DateTime purchaseDate { get; set; }

        public string? supplier { get; set; }

        public int quantity { get; set; }

        public decimal? productPrice { get; set; }

        public decimal? tax { get; set; }

        public decimal? net { get; set; }

        public decimal? totalAfterTax { get; set; }

        public string? reference { get; set; }

        public string? status { get; set; }

        public int? accountID_approver { get; set; }

        public PurchaseViewModel()
        {
            daoForm = new PurchaseDAO();
        }

        public async Task addPurchase()
        {
            try
            {
                //DateTime dateTimeNow = DateTime.Now;
                //var dateString1 = DateTime.Now.ToString("yyyy-MM-dd");

                Purchase myPurchase = new()
                {
                    accountID = accountID,
                    supplier = supplier,
                    quantity = quantity,
                    productPrice = productPrice,
                    tax = tax,
                    net = net,
                    totalAfterTax = totalAfterTax,
                    purchaseDate = purchaseDate,
                    reference = reference,
                    status =status ,
                    accountID_approver = accountID_approver
                };


                Purchase_ID = await daoForm.addPurchase(myPurchase);

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<List<PurchaseViewModel>> GetAll(int accountID)
        {
            List<PurchaseViewModel> allVms = new();

            try
            {
                List<Purchase> allPurchases = await daoForm.GetAll();

                foreach (Purchase f in allPurchases)
                {
                    //var parsedDate = DateTime.Parse(callDate);

                    PurchaseViewModel newView = new()
                    {
                       Purchase_ID = f.Purchase_ID,
                       accountID = f.accountID,
                       supplier = f.supplier,
                       quantity = f.quantity,
                       productPrice = f.productPrice,
                       tax = f.tax,
                       net = f.net,
                       totalAfterTax = f.totalAfterTax,
                       purchaseDate=f.purchaseDate,
                       reference = f.reference,
                       accountID_approver = f.accountID_approver,
                       status = f.status
                    };



                    if (f.accountID == accountID || accountID == 1)
                    {
                        allVms.Add(newView);
                    }

                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return allVms;
        }

        public async Task<Purchase> getPurchaseByID(int purchaseID)
        {
            Purchase purchase = new Purchase();
            purchase = await daoForm.getPurchase(purchaseID);

            return purchase;
            
            //Purchase? selectedPurchase;
            //try
            //{

            //    TheFactory_Context _db = new();
            //    selectedPurchase = await _db.Purchases.FirstOrDefaultAsync(acc => acc.Purchase_ID == purchaseID);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Problem in " + GetType().Name + " " +
            //    MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
            //    throw;
            //}
            //return selectedPurchase!;
        }   

        public async Task<int> UpdateStatus()
        {
            int updatestatus;
            try
            {
                Purchase p = new Purchase();

                p.Purchase_ID = Purchase_ID;
                p.accountID = accountID;
                p.purchaseDate = purchaseDate;
                p.quantity = quantity;
                p.productPrice = productPrice;  
                p.tax = tax;
                p.net = net;
                p.totalAfterTax = totalAfterTax;
                p.status = status;
                p.accountID_approver = accountID_approver;
                p.reference = reference;
                p.status = status;
                p.supplier = supplier;


                updatestatus = (int)await daoForm.Update(p);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }


            return updatestatus;
        }

    }
}
