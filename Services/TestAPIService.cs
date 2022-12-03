using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Calculator.Services
{
    internal class TestAPIService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        static string apiUrl = "http://3.6.127.17:8080/math/";
        public TestQuestionModel apiQuestion { get; private set; }

        public TestAPIService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<TestQuestionModel> getTestQuestion(int currentQuestionNumber)
        {
            apiQuestion = new TestQuestionModel();

            Uri uri = new Uri(string.Format($"{apiUrl}{currentQuestionNumber}", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    apiQuestion = JsonSerializer.Deserialize<TestQuestionModel>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return apiQuestion;
        }
    }
}
