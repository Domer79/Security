using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataRepository.InternalTests
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void GetDatabaseNameFromConnectionStringTest()
        {
            const string connectionString = "data source=.;database=Taxorg;User Id=developer;Password=sppdeveloper;MultipleActiveResultSets=True;App=EntityFramework";
            Assert.AreEqual("Taxorg", Tools.GetDatabaseNameFromConnectionString(connectionString));
        }
    }
}
