using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RickAndMorthyApp.Models;

namespace RickAndMorthyApp.Managers
{
    public class ApiManager
    {
        public ApiManager()
        {

        }

        ///<summary>
        ///Realiza petición a la API.
        ///</summary>
        public async Task<CharacterResponse> GetCharacters(string Page = "1")
        {
            using (var client = new HttpClient())
            {
                CharacterResponse ResponseModel = new CharacterResponse();
                try
                {
                    client.BaseAddress = new Uri("https://rickandmortyapi.com");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync($"/api/character?page={Page}");

                    if (Res.IsSuccessStatusCode)
                    {
                        var apiResult = Res.Content.ReadAsStringAsync().Result;
                        ResponseModel = JsonConvert.DeserializeObject<CharacterResponse>(apiResult);
                    }
                }
                catch (Exception ex)
                {
                    //loggeo
                }

                return ResponseModel;
            }
        }
    }
}
