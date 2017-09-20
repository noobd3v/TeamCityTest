using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamCityTest.Controllers;
using System.Web.Mvc;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            HomeController ctr = new HomeController();
            var ar = ctr.About() as ViewResult;
            Assert.AreEqual("Testing team city trigger", ar.ViewData["Message"]);
        }
    }
}
