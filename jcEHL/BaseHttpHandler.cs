using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace jcEHL {
    public class BaseHttpHandler {
        private readonly string _baseURL;
        private readonly bool _useBSON;
        private readonly bool _useGZIP;

        public BaseHttpHandler(string baseURL, bool useBSON = true, bool useGZIP = true) {
            _baseURL = baseURL;
            _useBSON = useBSON;
            _useGZIP = useGZIP;
        }

        public HttpClient GetHttpClient() {
            HttpClient httpClient;

            if (_useGZIP) {
                var messageHandler = new HttpClientHandler {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };

                httpClient = new HttpClient(messageHandler);
            } else {
                httpClient = new HttpClient();
            }

            if (!_useBSON) {
                return httpClient;
            }

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));

            return httpClient;
        }

        public async Task<T> Get<T>(string urlArguments) {
            var client = GetHttpClient();
            
            if (_useBSON) {
                var result = await client.GetAsync($"{_baseURL}{urlArguments}");

                var formatters = new MediaTypeFormatter[] {
                    new BsonMediaTypeFormatter()
                };

                return await result.Content.ReadAsAsync<T>(formatters);
            } 

            var resultStr = await client.GetStringAsync($"{_baseURL}{urlArguments}");            

            return JsonConvert.DeserializeObject<T>(resultStr);
        }

        public async Task<K> Post<T, K>(string urlArguments, T obj) {
            var client = GetHttpClient();

            MediaTypeFormatter formatter;

            if (_useBSON) {
                formatter = new BsonMediaTypeFormatter();
            } else {
                formatter = new JsonMediaTypeFormatter();
            }
            
            var response = await client.PostAsync(new Uri($"{_baseURL}{urlArguments}"), obj, formatter);

            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<K>(data);            
        }
    }
}