using KomodoRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KomodoTests
{
    [TestClass]
    public class DeveloperTests
    {
        [DataTestMethod]
        [DataRow("Test", "Name")]
        [DataRow("Adam", "Wolanin")]
        [DataRow("Jenna", "Hollam")]
        public void Developer_FirstName(string testFirstName, string testLastName)
        {
            var developer = new Developer(testFirstName, testLastName, 0);
            Assert.AreEqual(testFirstName, developer.FirstName);
        }

        [DataTestMethod]
        [DataRow("Test", "Name")]
        [DataRow("Adam", "Wolanin")]
        [DataRow("Jenna", "Hollam")]
        public void Developer_LastName(string testFirstName, string testLastName)
        {
            var developer = new Developer(testFirstName, testLastName, 0);
            Assert.AreEqual(testLastName, developer.LastName);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(69)]
        [DataRow(1337)]
        public void Developer_ID(int testID)
        {
            var developer = new Developer("Test", "Name", testID);
            Assert.AreEqual(testID, developer.ID);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Developer_HasPluralsight(bool testHasPluralsight)
        {
            var developerA = new Developer("Test", "Name", 0, testHasPluralsight);
            Assert.AreEqual(testHasPluralsight, developerA.HasPluralsight);
        }
    }
}
