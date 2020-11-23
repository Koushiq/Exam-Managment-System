using ExamManagementSystem.Models.ServiceAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExamManagementSystemTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int n = 0;
            n.SetBit(5);
            Assert.AreEqual(n.CheckBit(5), true);
        }
        public static void Main(string[] args)
        {

        }
    }
}
