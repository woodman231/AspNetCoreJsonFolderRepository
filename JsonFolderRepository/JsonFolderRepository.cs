using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonFolderRepository.Interfaces;

namespace JsonFolderRepository
{
    public abstract class JsonFolderRepository<TRepositoryItem> : IRepository<TRepositoryItem> where TRepositoryItem : IRepositoryItem
    {
        private string _folderPath;

        public string FolderPath
        {
            get
            {
                return _folderPath;
            }
        }

        public JsonFolderRepository(string folderPath)
        {
            this._folderPath = folderPath;
        }

        public TRepositoryItem Create(TRepositoryItem repositoryItem)
        {
            if(repositoryItem.Guid == null)
            {
                repositoryItem.Guid = Guid.NewGuid();
            }

            string myFileName = Path.Combine(_folderPath, $"{repositoryItem.Guid}.json");
            string myFileContent = JsonSerializer.Serialize(repositoryItem, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(myFileName, myFileContent);

            return repositoryItem;
        }

        public void Delete(Guid guid)
        {
            string myFileName = Path.Combine(_folderPath, $"{guid}.json");

            if(File.Exists(myFileName))
            {
                File.Delete(myFileName);
            }
        }

        public List<TRepositoryItem> GetAll()
        {
            List<TRepositoryItem> results = new List<TRepositoryItem>();
            string[] files = Directory.GetFiles(_folderPath, "*.json", SearchOption.TopDirectoryOnly);

            foreach(string file in files)
            {
                string jsonString = File.ReadAllText(file);
                TRepositoryItem repositoryItem = JsonSerializer.Deserialize<TRepositoryItem>(jsonString);

                results.Add(repositoryItem);
            }

            return results;
        }

        public TRepositoryItem GetOne(Guid guid)
        {
            string myFileName = Path.Combine(_folderPath, $"{guid}.json");
            string jsonString = File.ReadAllText(myFileName);

            TRepositoryItem repositoryItem = JsonSerializer.Deserialize<TRepositoryItem>(jsonString);

            return repositoryItem;
        }

        public TRepositoryItem Update(TRepositoryItem repositoryItem)
        {
            if (repositoryItem.Guid == null)
            {
                repositoryItem.Guid = Guid.NewGuid();
            }

            string myFileName = Path.Combine(_folderPath, $"{repositoryItem.Guid}.json");
            string myFileContent = JsonSerializer.Serialize(repositoryItem, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(myFileName, myFileContent);

            return repositoryItem;
        }
    }
}
