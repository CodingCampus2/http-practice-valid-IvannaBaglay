using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpClients
{
    class Client
    {
        private HttpClient m_Client;

        public void Init()
        {
            m_Client = new HttpClient();
            m_Client.BaseAddress = new Uri("http://localhost:5000/somedata/");
            m_Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            m_Client.DefaultRequestHeaders.TryAddWithoutValidation("appId", "campus-task");
            return;
        }
        public async Task GetDataByIdRequest(string id) => await SendRequest(new HttpRequestMessage(HttpMethod.Get, id), false);
        public async Task GetAllDataRequest(bool isSorted) => await SendRequest(new HttpRequestMessage(HttpMethod.Get, isSorted ? "?sorted=True" : ""), false);
        public async Task DeleteDataByIdRequest(string id) => await SendRequest(new HttpRequestMessage(HttpMethod.Delete, id), true);
        public async Task PostRequest(string stringData)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "");

            message.Content = new StringContent(stringData, Encoding.UTF8, "application/json");

            await SendRequest(message, true);
        }
        public async Task PutRequest(string id, string stringData)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, id);

            message.Content = new StringContent(stringData, Encoding.UTF8, "application/json");

            await SendRequest(message, true);
        }

        private async Task SendRequest(HttpRequestMessage message, bool showOnlyReturnCode)
        {
            try
            {
                var result = await m_Client.SendAsync(message);

                Console.WriteLine("Server responce:");
                if (result.IsSuccessStatusCode)
                {
                    if (showOnlyReturnCode)
                        Console.WriteLine($"Return code: {result.StatusCode}");
                    else
                        Console.WriteLine(await result.Content.ReadAsStringAsync());
                }
                else
                {
                    Console.WriteLine(
                        $"Return code: {result.StatusCode}\nMessage: {await result.Content.ReadAsStringAsync()}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
