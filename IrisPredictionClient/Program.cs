using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IrisPredictionClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Define the API endpoint and the input data
            var apiUrl = "http://127.0.0.1:8000/predict";
            var input = new
            {
                sepal_length = 5.1,
                sepal_width = 3.5,
                petal_length = 1.4,
                petal_width = 0.2
            };

            // Create an HttpClient instance
            using HttpClient client = new HttpClient();

            try
            {
                // Send a POST request to the API
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, input);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read and display the response
                var prediction = await response.Content.ReadFromJsonAsync<IrisPrediction>();
                Console.WriteLine($"Predicted species: {prediction?.Species}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Define a class to deserialize the response
        public class IrisPrediction
        {
            public string Species { get; set; }
        }
    }
}

