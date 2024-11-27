using System;
using System.IO;
using UnityEngine;

namespace Assets.CourseGame.Develop.CommonServices.DataManagement
{
    public class LocalDataRepository : IDataRepository
    {
        private const string SaveFileExtension = "json";

        private string FolderPath => Application.persistentDataPath;

        public bool Exists(string key) => File.Exists(FullPathFor(key));

        public string Read(string key) => File.ReadAllText(FullPathFor(key));

        public void Remove(string key) => File.Delete(FullPathFor(key));

        public void Write(string key, string serializedData) => File.WriteAllText(FullPathFor(key), serializedData);

        private string FullPathFor(string key) =>
            Path.Combine(FolderPath, key) + "." + SaveFileExtension;
    }
}
