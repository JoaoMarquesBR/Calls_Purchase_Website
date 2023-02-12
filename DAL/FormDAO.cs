using DAL;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;


namespace DatabaseDAL
{
    public class FormDAO
    {
        private readonly IRepository<Form> _repo;

        public FormDAO()
        {
            _repo = new TheFactory_Repository<Form>();
        }



        public async Task<int> addForm(Form newForm)
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

            return newForm.formID;
        }


        public async Task<List<Form>> GetAll()
        {
            List<Form> forms;
            try
            {
                forms = await _repo.GetAll(); 
            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

            return forms;
        }

    }





}
    
    
