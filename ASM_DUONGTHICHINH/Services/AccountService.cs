using ASM_DUONGTHICHINH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ASM_DUONGTHICHINH.Services
{
   public class AccountService
    {
        public async Task<bool> RegisterAsyn(Account account)
        {
            try
            {

                var accountjson = Newtonsoft.Json.JsonConvert.SerializeObject(account);
                HttpClient httpClient = new HttpClient();
                Console.WriteLine(accountjson);
                var httpContent = new StringContent(accountjson, Encoding.UTF8, "application/json");
                var requestConnection = await httpClient.PostAsync("https://music-i-like.herokuapp.com/api/v1/accounts", httpContent);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<Credential> loginAsync(LoginInfomation loginInfomation)
        {
            try
            {

                var accountjson = Newtonsoft.Json.JsonConvert.SerializeObject(loginInfomation);
                HttpClient httpClient = new HttpClient();
                Console.WriteLine(accountjson);
                var httpContent = new StringContent(accountjson, Encoding.UTF8, "application/json");
                var requestConnection = await httpClient.PostAsync("https://music-i-like.herokuapp.com/api/v1/accounts/authentication", httpContent);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var content = await requestConnection.Content.ReadAsStringAsync();
                    var credential = Newtonsoft.Json.JsonConvert.DeserializeObject<Credential>(content);
                    Console.WriteLine(content);
                    await WriteTokenToFile(content);
                    return credential;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        private async Task WriteTokenToFile(string content)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
            StorageFile storageFile = await storageFolder.CreateFileAsync("milt.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, content);
        }

        public async Task<Credential> LoadAccessTokenFromFile()
        {
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalCacheFolder;
                StorageFile storageFile = await storageFolder.GetFileAsync("milt.txt");
                var fileContent = await FileIO.ReadTextAsync(storageFile);
                var credential = Newtonsoft.Json.JsonConvert.DeserializeObject<Credential>(fileContent);
                return credential;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Account> GetInfomation()
        {
            var credential = await LoadAccessTokenFromFile();
            if (credential == null)
            {
                return null;
            }
            try
            {

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {credential.access_token}");
                var requestConnection = await httpClient.GetAsync("https://music-i-like.herokuapp.com/api/v1/accounts");
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var content = await requestConnection.Content.ReadAsStringAsync();
                    var account = Newtonsoft.Json.JsonConvert.DeserializeObject<Account>(content);
                    Console.WriteLine(content);
                    return account;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
