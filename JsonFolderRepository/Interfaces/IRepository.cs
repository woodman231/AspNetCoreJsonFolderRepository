using System;
using System.Collections.Generic;
using System.Text;

namespace JsonFolderRepository.Interfaces
{
    public interface IRepository<RepositoryItemType> where RepositoryItemType : IRepositoryItem
    {
        public List<RepositoryItemType> GetAll();
        public RepositoryItemType GetOne(Guid guid);
        public RepositoryItemType Create(RepositoryItemType repositoryItemType);
        public RepositoryItemType Update(RepositoryItemType repositoryItemType);
        public void Delete(Guid guid);
    }
}
