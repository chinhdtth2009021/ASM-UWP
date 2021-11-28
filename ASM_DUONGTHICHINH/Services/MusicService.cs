using ASM_DUONGTHICHINH.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASM_DUONGTHICHINH.Services
{
    public class MusicService
    {
        private const string ApiBaseUrl = "https://music-i-like.herokuapp.com";
        private const string ApiSongPath = "/api/v1/songs";
        public async Task<List<Music>> GetLatestSongAsync()
        {
            List<Music> result = new List<Music>();
            try
            {
                HttpClient httpClient = new HttpClient();
                var requestConnection =
                    await httpClient.GetAsync(ApiBaseUrl + ApiSongPath);
                Console.WriteLine(requestConnection.StatusCode);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await requestConnection.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Music>>(content);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<Music> CreateSongAsync(Music music)
        {
            AccountService accountService = new AccountService();
            var credential = await accountService.LoadAccessTokenFromFile();
            try
            {
                var songJson = Newtonsoft.Json.JsonConvert.SerializeObject(music);
                HttpClient httpClient = new HttpClient();

                Debug.WriteLine(songJson);
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {credential.access_token}");
                var httpContent = new StringContent(songJson, Encoding.UTF8, "application/json");

                //Thực hiện gửi dữ liệu sử dụng async, await
                var requestConnection =
                    await httpClient.PostAsync("https://music-i-like.herokuapp.com/api/v1/songs/mine", httpContent);
                Debug.WriteLine(requestConnection.StatusCode);
                if (requestConnection.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return music;
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
