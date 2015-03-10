using System.Data.Entity;
using System.Diagnostics;
using SystemTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityDataModel.Attributes;
using SecurityDataModel.Models;
using SecurityDataModel.Repositories;

namespace Security.TestProject
{
    [TestClass]
    public class DataModelTest1
    {
        public DataModelTest1()
        {
            ApplicationCustomizer.SecurityConnectionString = "data source=cito1;initial catalog=Taxorg_Temp;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        }
    }

    public class TestSecurityContext : SecurityContext
    {
        public DbSet<TestObject> TestObjects { get; set; }
    }

    public class TestObject : SecObject
    {
        [ObjectName]
        public string TestObjectName { get; set; }

        [Column1]
        public string Column1 { get; set; }

        [Column2]
        public string Column2 { get; set; }
    }
}
