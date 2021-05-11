using System;
using System.Collections.Generic;
using System.Text;

namespace JsonFolderRepository.Interfaces
{
    public interface IRepositoryItem
    {
        public Guid Guid { get; set; }
    }
}
