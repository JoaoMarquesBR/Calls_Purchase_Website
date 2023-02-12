using DAL;
using DatabaseDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace ExercisesTest
{
    public class DAOTests
    {

        [Fact]
        public async Task account_check()
        {
            AccountDAO dao = new();
            Account selectedAcc = await dao.accountDAO_CheckAccount("admin","admin");
                    
            
            Assert.NotNull(selectedAcc);

        }


    }
}
