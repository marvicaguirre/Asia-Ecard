using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wizardsgroup.Utilities.Helpers;

namespace MIMS.Utilities.Tests.Helpers
{
    [TestClass]
    public class PasswordHelperTests
    {
        [TestMethod]
        public void CreateHashShouldNotReturnPassedPasswordString()
        {
            string userName = "testusername@medicard.com";
            string testPassword = "P@ssword";
            string salt = PasswordHelper.CreateSalt(userName);
            string hashedPassword = PasswordHelper.HashPassword(salt, testPassword);
            Assert.AreNotSame(testPassword, hashedPassword);
        }
    }
}
