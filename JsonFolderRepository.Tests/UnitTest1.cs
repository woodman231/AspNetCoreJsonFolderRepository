using NUnit.Framework;
using JsonFolderRepository.Tests.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace JsonFolderRepository.Tests
{
    public class Tests
    {
        private string _folderpath;        
        private PersonJsonRepository _personJsonRepository;
        private Person _johnDoe;
        private Person _janeDoe;

        private static Guid _johnDoeGuid = Guid.NewGuid();
        private static Guid _janeDoeGuid = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {            
            _folderpath = Path.Combine(Path.GetDirectoryName(typeof(Tests).Assembly.Location), "people");

            if(!Directory.Exists(_folderpath))
            {
                Directory.CreateDirectory(_folderpath);
            }            

            _personJsonRepository = new PersonJsonRepository(_folderpath);

            _johnDoe = new Person
            {
                Guid = _johnDoeGuid,
                FirstName = "John",
                LastName = "Doe"
            };

            _janeDoe = new Person
            {
                Guid = _janeDoeGuid,
                FirstName = "Jane",
                LastName = "Doe"
            };
        }

        [Test, Order(1)]
        public void SerializesNewObjectsToFiles()
        {
            // John Doe
            string johnDoeFileName = Path.Combine(_folderpath, $"{_johnDoe.Guid}.json");
            string johnDoeAsJson = JsonSerializer.Serialize(_johnDoe, new JsonSerializerOptions { WriteIndented = true });

            _personJsonRepository.Create(_johnDoe);

            string johnDoeFileText = File.ReadAllText(johnDoeFileName);

            // Jane Doe
            string janeDoeFileName = Path.Combine(_folderpath, $"{_janeDoeGuid}.json");
            string janeDoeAsJson = JsonSerializer.Serialize(_janeDoe, new JsonSerializerOptions { WriteIndented = true });

            _personJsonRepository.Create(_janeDoe);

            string janeDoeFileText = File.ReadAllText(janeDoeFileName);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(johnDoeAsJson, johnDoeFileText);
                Assert.AreEqual(janeDoeAsJson, janeDoeFileText);
            });

        }

        [Test, Order(2)]
        public void DeserializesObjectsFromFiles()
        {
            Person johnDoe = _personJsonRepository.GetOne(_johnDoeGuid);
            Person janeDoe = _personJsonRepository.GetOne(_janeDoeGuid);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(johnDoe, _johnDoe);
                Assert.AreEqual(janeDoe, _janeDoe);
            });
        }

        [Test, Order(3)]
        public void ListsObjectsFromFiles()
        {
            List<Person> expectedPersonsList = new List<Person>();
            expectedPersonsList.Add(_johnDoe);
            expectedPersonsList.Add(_janeDoe);

            List<Person> retrievedPersonsList = _personJsonRepository.GetAll();

            CollectionAssert.AreEquivalent(expectedPersonsList, retrievedPersonsList);
        }

        [Test, Order(4)]
        public void UpdatesObjects()
        {
            string johnDoeFileName = Path.Combine(_folderpath, $"{_johnDoeGuid}.json");
            Person personRead = _personJsonRepository.GetOne(_johnDoeGuid);
            personRead.MiddleName = "Updated";

            string updatedJohnDoeAsJson = JsonSerializer.Serialize(personRead, new JsonSerializerOptions { WriteIndented = true });

            _personJsonRepository.Update(personRead);

            string updatedFileText = File.ReadAllText(johnDoeFileName);

            Assert.AreEqual(updatedJohnDoeAsJson, updatedFileText);
        }


        [Test, Order(5)]
        public void DeletesFiles()
        {
            string johnDoeFileName = Path.Combine(_folderpath, $"{_johnDoe.Guid}.json");
            string janeDoeFileName = Path.Combine(_folderpath, $"{_janeDoeGuid}.json");

            _personJsonRepository.Delete(_johnDoeGuid);
            _personJsonRepository.Delete(_janeDoeGuid);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(File.Exists(johnDoeFileName));
                Assert.IsFalse(File.Exists(janeDoeFileName));
            });
        }

    }
}