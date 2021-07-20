using KomodoRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KomodoTests
{
    [TestClass]
    public class DeveloperRepoTests
    {
        private readonly DeveloperRepo _repository = new DeveloperRepo();

        [TestInitialize]
        public void Initialize()
        {
            _repository.Add(new Developer("TestName", "One", 1, true));
            _repository.Add(new Developer("TestName", "Two", 2, false));
            _repository.Add(new Developer("TestName", "Three", 3, true));
            _repository.Add(new Developer("TestName", "Four", 4, false));
            _repository.Add(new Developer("TestName", "Five", 5, true));
            _repository.Add(new Developer("TestName", "Six", 6, false));
        }

        [TestMethod]
        public void DeveloperRepo_AddDeveloperWithUniqueID_ReturnsTrue()
        {
            int testID = 777;
            var testDeveloper = new Developer("Lucky", "Sevens", testID, true);
            bool condition = _repository.Add(testDeveloper);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void DeveloperRepo_AddDeveloperWithDuplicateID_ReturnsFalse()
        {
            int testID = 6;
            var testDeveloper = new Developer("Duplicate", "ID", testID, true);
            bool condition = _repository.Add(testDeveloper);
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void DeveloperRepo_GetInRepo_IsNotNull()
        {
            string testFirstName = "TestName";
            string testLastName = "Six";
            int testID = 6;
            var developer = _repository.Get(testID);
            Assert.IsNotNull(developer);
            Assert.AreEqual(testFirstName, developer.FirstName);
            Assert.AreEqual(testLastName, developer.LastName);
            Assert.AreEqual(testID, developer.ID);
        }

        [TestMethod]
        public void DeveloperRepo_GetNotInRepo_IsNull()
        {
            int testID = -1;
            var developer = _repository.Get(testID);
            Assert.IsNull(developer);
        }

        [TestMethod]
        public void DeveloperRepo_GetAll()
        {
            foreach (var developer in _repository.GetAll())
            {
                Console.WriteLine($"{developer.FirstName} {developer.LastName}");
            }
        }

        [TestMethod]
        public void DeveloperRepo_GetAllWithoutPluralSight()
        {
            foreach (var developer in _repository.GetAllDevelopersWithoutPluralSight())
            {
                Console.WriteLine($"{developer.FirstName} {developer.LastName} {developer.HasPluralsight}");
                Assert.IsFalse(developer.HasPluralsight);
            }
        }

        [TestMethod]
        public void DeveloperRepo_RemoveIsInRepo_ReturnsTrue()
        {
            int testID = 3;
            var condition = _repository.Remove(testID);
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void DeveloperRepo_RemoveIsNotInRepo_ReturnsFalse()
        {
            int testID = -1;
            var condition = _repository.Remove(testID);
            Assert.IsFalse(condition);
        }
    }
}
