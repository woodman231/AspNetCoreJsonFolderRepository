using System;
using System.Collections.Generic;
using System.Text;
using JsonFolderRepository.Interfaces;

namespace JsonFolderRepository.Tests.Models
{
    public class Person : IRepositoryItem
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            else
            {
                Person p = (Person)obj;

                bool guidMatch = (Guid == p.Guid);
                bool firstNameMatch = (FirstName == p.FirstName);
                bool middleNameMatch = (MiddleName == p.MiddleName);
                bool lastNameMatch = (LastName == p.LastName);

                return guidMatch && firstNameMatch && middleNameMatch && lastNameMatch;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Guid},{FirstName},{MiddleName},{LastName}";
        }
    }
}
