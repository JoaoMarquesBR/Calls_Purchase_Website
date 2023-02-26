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

        public async Task<Form> getFormByID(int formID)
        {
            Form? selectedAcc;
            try
            {

                TheFactory_Context _db = new();
                selectedAcc = await _db.Forms.FirstOrDefaultAsync(acc => acc.formID == formID);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return selectedAcc!;
        }

        public async Task<int>updateDesc(Form form)
        {

            try
            {
                TheFactory_Context _db = new();
                Form? currentStudent = await _db.Forms.FirstOrDefaultAsync(stu => stu.formID == form.formID);
                _db.Entry(currentStudent!).OriginalValues["callDesc"] = form.callDesc;
                _db.Entry(currentStudent!).CurrentValues.SetValues(form);
                if (await _db.SaveChangesAsync() == 1)
                {
                    return 1;
                };
                return -1;
            }catch(Exception e)
            {
                throw;
            }

        }

    }





}
    
    
