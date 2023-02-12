using DAL;
using DatabaseDAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace ExercisesTest
{
    public class LinqTests
    {


        [Fact]
        public void LinqTest_CheckAccount()
        {

            var username = "admin";
            var password = "admin";

            TheFactory_Context _db = new();
            var accSelected = from acc in _db.Accounts
                              where acc.username == username && acc.password == password
                                   select acc;
            Assert.True(accSelected.Count()>0);

        }

    }
  
}