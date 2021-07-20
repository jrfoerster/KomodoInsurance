using KomodoRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KomodoTests
{
    [TestClass]
    public class DevTeamTests
    {
        [DataTestMethod]
        [DataRow("Test Name")]
        [DataRow("Development")]
        [DataRow("Operations")]
        public void DevTeam_Name(string testName)
        {
            var team = new DevTeam(testName, 0);
            Assert.AreEqual(testName, team.Name);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(69)]
        [DataRow(1337)]
        public void DevTeam_ID(int testID)
        {
            var team = new DevTeam("Test Name", testID);
            Assert.AreEqual(testID, team.ID);
        }

        [TestMethod]
        public void DevTeam_AddUnique_ReturnsTrue()
        {
            var team = new DevTeam("Team Name", 0);
            var testDeveloper = new Developer("Test", "Name", 0);
            bool condition = team.Add(testDeveloper);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void DevTeam_AddDuplicate_ReturnsFalse()
        {
            var team = new DevTeam("Team Name", 0);
            var duplicate = new Developer("Test", "Name", 0);
            team.Add(duplicate);
            bool condition = team.Add(duplicate);
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void DevTeam_AddIsNull_ReturnsFalse()
        {
            var team = new DevTeam("Team Name", 0);
            Developer testDeveloper = null;
            bool condition = team.Add(testDeveloper);
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void DevTeam_RemoveDeveloperInTeam_ReturnsTrue()
        {
            var team = new DevTeam("Team Name", 0);
            var developer = new Developer("Test", "Name", 0);
            team.Add(developer);
            bool condition = team.Remove(developer);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void DevTeam_RemoveDeveloperNotInTeam_ReturnsFalse()
        {
            var team = new DevTeam("Team Name", 0);
            var developer = new Developer("Test", "Name", 0);
            bool condition = team.Remove(developer);
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void DevTeam_RemoveDeveloperIsNull_ReturnsFalse()
        {
            var team = new DevTeam("Team Name", 0);
            Developer developer = null;
            bool condition = team.Remove(developer);
            Assert.IsFalse(condition);
        }
    }
}
