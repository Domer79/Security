using System;
using SystemTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Security.TestProject
{
    [TestClass]
    public class DataModelTest1
    {
        [TestMethod]
        public void CreateUserTest()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=cito1;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        }
    }
}
