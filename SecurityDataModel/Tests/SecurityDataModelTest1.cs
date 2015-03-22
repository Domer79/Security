using System.Diagnostics;
using SystemTools.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityDataModel.Infrastructure;

namespace SecurityDataModel.Tests
{
    [TestClass]
    public class SecurityDataModelTest1
    {
        [TestMethod]
        public void GetEnumDescriptionTest()
        {
            Debug.WriteLine(MemberType.User.GetDescription());
            Assert.AreEqual("Пользователь", MemberType.User.GetDescription());
        }

        [TestMethod]
        public void TestTest()
        {
            Debug.WriteLine("Hello World!!!");
        }
    }
}
