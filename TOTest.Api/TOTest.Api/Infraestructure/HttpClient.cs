using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TOTest.Api.Models;

namespace TOTest.Api.Infraestructure
{
    public class UserResponse
    {
        public ImageViewModel[] data { get; set; }
    }
    public interface IHttpClient
    {
        Task<IEnumerable<ImageViewModel>> Get();
    }
    public class HttpClient : IHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<ImageViewModel>> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, " https://webservice2.mobimanage.com/json.aspx?domainid=2285&fn=listings");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                var imageResponse = await JsonSerializer.DeserializeAsync
                    <IEnumerable<ImageViewModel>>(responseStream);
                var images = imageResponse;
                return images;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
