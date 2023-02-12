using DAL;
using ExercisesDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Reflection;


namespace DatabaseDAL
{
    public class PurchaseDAO
    {
        private readonly IRepository<Purchase> _repo;

        public PurchaseDAO()
        {
            _repo = new TheFactory_Repository<Purchase>();
        }



        public async Task<int> addPurchase(Purchase newForm)
        {
            try
            {
                //selectedAcc = await _db.accounts.FirstOrDefaultAsync(acc => acc.accountName == username && acc.accountPassword == password);
                TheFactory_Context _db = new();
                newForm = await _repo.Add(newForm);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("\n\nProblem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }

            return newForm.Purchase_ID;
        }


        public async Task<List<Purchase>> GetAll()
        {
            List<Purchase> purchases;
            try
            {
                purchases = await _repo.GetAll();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

            return purchases;
        }

        public async Task<UpdateStatus> Update(Purchase updatedPurchase)
        {
            UpdateStatus status = UpdateStatus.Failed;
            try
            {
                TheFactory_Context _db = new();
                Purchase? currentStudent = await _db.Purchases.FirstOrDefaultAsync(stu => stu.Purchase_ID == updatedPurchase.Purchase_ID);
                _db.Entry(currentStudent!).CurrentValues.SetValues(updatedPurchase);
                if (await _db.SaveChangesAsync() == 1)
                {
                    status = UpdateStatus.Ok;
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                status = UpdateStatus.Stale;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }

            return status;
        }

        public async Task<Purchase> getPurchase(int purchaseID)
        {
            Purchase? selectedAcc;
            try
            {

                TheFactory_Context _db = new();
                //var testa = _db.Accounts.FirstOrDefault(stu => stu.accountID == 1);

                //var test = _db.Purchases.FirstOrDefault(stu => stu.Purchase_ID == 1003);

                selectedAcc = await _db.Purchases.FirstOrDefaultAsync(acc => acc.Purchase_ID == purchaseID);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return selectedAcc!;
        }



    }





}
    
    
