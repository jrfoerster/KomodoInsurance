using KomodoRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KomodoTests
{
    [TestClass]
    public class DevTeamRepoTests
    {
        private readonly DevTeamRepo _repository = new DevTeamRepo();

        [TestInitialize]
        public void Initialize()
        {
            _repository.Add(new DevTeam("Team One", 1));
            _repository.Add(new DevTeam("Team Two", 2));
            _repository.Add(new DevTeam("Team Three", 3));
            _repository.Add(new DevTeam("Team Four", 4));
            _repository.Add(new DevTeam("Team Five", 5));
        }

        [TestMethod]
        public void DevTeamRepo_AddTeamWithUniqueID_ReturnsTrue()
        {
            string testName = "Team Six";
            int testID = 6;
            var testTeam = new DevTeam(testName, testID);
            bool condition = _repository.Add(testTeam);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void DevTeamRepo_AddTeamWithDuplicateID_ReturnsFalse()
        {
            string testName = "Team Six";
            int testID = 1;
            var testTeam = new DevTeam(testName, testID);
            bool condition = _repository.Add(testTeam);
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void DevTeamRepo_GetTeamInRepo_IsNotNull()
        {
            string testName = "Team Two";
            int testID = 2;
            var devTeam = _repository.Get(testID);
            Assert.IsNotNull(devTeam);
            Assert.AreEqual(testName, devTeam.Name);
            Assert.AreEqual(testID, devTeam.ID);
        }

        [TestMethod]
        public void DevTeamRepo_GetTeamNotInRepo_IsNull()
        {
            int testID = -1;
            var devTeam = _repository.Get(testID);
            Assert.IsNull(devTeam);
        }

        [TestMethod]
        public void DevTeamRepo_GetAll()
        {
            foreach (var devTeam in _repository.GetAll())
            {
                Console.WriteLine(devTeam.Name);
            }
        }

        [TestMethod]
        public void DevTeamRepo_RemoveDeveloperFromAllTeams()
        {
            var developer = new Developer("First", "Last", 0);
            foreach (var devTeam in _repository.GetAll())
            {
                bool condition = devTeam.Add(developer);
                Assert.IsTrue(condition);
            }

            _repository.RemoveDeveloperFromAllTeams(developer);

            foreach (var devTeam in _repository.GetAll())
            {
                bool condition = devTeam.Remove(developer);
                Assert.IsFalse(condition);
            }
        }

        [TestMethod]
        public void DevTeamRepo_RemoveIsInRepo_ReturnsTrue()
        {
            int testID = 1;
            bool condition = _repository.Remove(testID);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void DevTeamRepo_RemoveIsNotInRepo_ReturnsFalse()
        {
            int testID = -1;
            bool condition = _repository.Remove(testID);
            Assert.IsFalse(condition);
        }
    }
}
