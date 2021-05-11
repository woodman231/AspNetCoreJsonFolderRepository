# ASP.NET Core Json Folder Repository

Handy little tool if you want to store JSON files in a directory and use that directory as a defacto database.

This is to also demonstrate my ASP.NET Core skills as well as Unit Testing Skills.

## Getting started

Create a Model / POCO Class that inherits from JsonFolderRepository.Interfaces.IRepositoryItem ex Person.cs

```cs

using JsonFolderRepository.Interfaces;

public class Person : IRepositoryItem 
{
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
}
```

Then create a repository for your class ex PersonJsonRepository.cs
```cs

using JsonFolderRepository;
// using namespace for where you created the model class if not in the same folder as the Repository class

    public class PersonJsonRepository : JsonFolderRepository<Person>
    {
        public PersonJsonRepository(string folderLocation) : base(folderLocation)
        {
        }
    }

```

Use dependency injection or in the consuming class:

```cs
private PersonJsonRepository _personJsonRepository;

ClassConstructor() {
    string peopleDirectory = "C:\\Wherever\\people";
    _personJsonRepository = new PersonJsonRepository(peopleDirectory);
}
```