using System;
using System.Collections.Generic;
using System.Text;
using JsonFolderRepository;
using JsonFolderRepository.Tests.Models;


namespace JsonFolderRepository.Tests
{
    public class PersonJsonRepository : JsonFolderRepository<Person>
    {
        public PersonJsonRepository(string folderLocation) : base(folderLocation)
        {
        }
    }
}
