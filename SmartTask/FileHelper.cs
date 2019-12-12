using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTask
{
    public static class FileHelper
    {
        private static readonly string FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "SmartTaskbar");
        public static string FileName = "config.json";
        public static string FilePath = Path.Combine(FolderPath, FileName);

        public static void SaveJosn(object o)
        {
            string jsonString = JsonConvert.SerializeObject(o);
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            File.WriteAllText(FilePath, jsonString, Encoding.UTF8);
        }

        public static T ReadJosn<T>(string path = "")
        {
            var jsonPath = !string.IsNullOrWhiteSpace(path) ? path : FilePath;
            if (File.Exists(jsonPath))
            {
                string jsonString = File.ReadAllText(jsonPath);

                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return default;
        }

        public static Task<T> AsyncReadJosn<T>(string path = "")
        {
            return Task.Run(() =>
            {
                var jsonPath = !string.IsNullOrWhiteSpace(path) ? path : FilePath;
                if (File.Exists(jsonPath))
                {
                    string jsonString = File.ReadAllText(jsonPath);

                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                return default;
            });
        }
    }
}
