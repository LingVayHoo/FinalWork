using AuthServer.Models.Responce;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWpf.Models
{
    internal static class TokenManager
    {
        private static string _fileName = "LogFile.txt";

        public static void WriteDataToJson(string json)
        {
            File.WriteAllText(_fileName, json);
        }

        private static string? ReadDataFromJson() 
        {
            if (File.Exists(_fileName))
            {
                return File.ReadAllText(_fileName);
            }
            else
            {
                return null;
            }
        }

        private static AuthenticatedUserResponce? GetData()
        {
            return JsonConvert.DeserializeObject<AuthenticatedUserResponce>(ReadDataFromJson());
        }

        public static string? GetAccessToken()
        {
            var result = GetData();
            if (result == null) return null;
            
            return result.AccessToken;
        }

        public static string? GetRefreshToken()
        {
            var result = GetData();
            if (result == null) return null;

            return result.RefreshToken;
        }

        public static void DeleteAllData()
        {
            File.Delete(_fileName);
        }

        /// <summary>
        /// Метод для проверки существования файла
        /// </summary>
        /// <returns></returns>
        public static bool IsFileExists()
        {
            FileInfo file = new FileInfo(@_fileName);
            return file.Exists;
        }
    }
}
