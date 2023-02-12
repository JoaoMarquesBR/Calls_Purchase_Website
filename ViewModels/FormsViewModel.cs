using DAL;
using DatabaseDAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class FormsViewModel
    {

        readonly private FormDAO daoForm;
        public int formID { get; set; }

        public int? accountID { get; set; }

        public string? companyName { get; set; } = null!;

        public string? repName { get; set; } = null!;

        public  DateTime callDate { get; set; }

        public string? callLength { get; set; } = null!;

        public string? callDesc { get; set; } = null!;

        public string? issueSolved { get; set; } = null!;

        public string? followUp { get; set; } = null!;




        public FormsViewModel()
        {
            daoForm = new FormDAO();
        }

        public async Task addForm()
        {
            try
            {
                //DateTime dateTimeNow = DateTime.Now;
                //var dateString1 = DateTime.Now.ToString("yyyy-MM-dd");

                Form myForm = new()
                {
                    accountID = accountID,
                    companyName = companyName,
                    repName = repName,
                    callDate = DateTime.Now,
                    callDesc = callDesc,
                    timeLength = callLength,
                    issueSolved = issueSolved,
                    followUp = followUp
                };

              

                formID = await daoForm.addForm(myForm);

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<List<FormsViewModel>> GetAll(int accountID)
        {
            List<FormsViewModel> allVms = new();

            try
            {
                List<Form> allForms = await daoForm.GetAll();

                foreach (Form f in allForms)
                {
                    //var parsedDate = DateTime.Parse(callDate);

                    FormsViewModel newView = new()
                    {
                        formID = f.formID,
                        accountID = (f.accountID),
                        companyName = f.companyName,
                        repName = f.repName,
                        callDate = f.callDate,

                        callLength = f.timeLength,
                        callDesc = f.callDesc,
                        issueSolved = f.issueSolved,
                        followUp = f.followUp
                    };



                    if (f.accountID == accountID || (accountID == 1))
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

    }
}
