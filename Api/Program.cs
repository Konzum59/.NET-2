using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api
{

    public class ApiTest
    {
        public int pageNumber { get; set; }
        public int perPage { get; set; }
        public HttpClient client;

        public async Task GetData(int pageNumber, int perPage)
        {

            client = new HttpClient();

            string call = $"https://cocktails.solvro.pl/api/v1/cocktails?page={pageNumber}&perPage={perPage}";
            try
            {
                string response = await client.GetStringAsync(call);
                //List<Coctail> coctails= JsonSerializer.Deserialize<List<Coctail>>(response);
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(response);

                if (apiResponse?.data != null)
                {

                    foreach (Coctail drink in apiResponse.data)
                    {
                        Console.WriteLine(drink);

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"erroe: {e.Message}");



            }
        }

    }
    internal class ApiResponse
    {
        public List<Coctail> data { get; set; }
    }
    internal class Coctail
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string glass { get; set; }

        public string instructions { get; set; }
        public string imageUrl { get; set; }
        public bool alcoholic { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }



        public override string ToString()
        {
            return $"Name: {name}, category: {category}, glass type served in: {glass}";
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How much coctails per page would you like to see?");
            int coctailsPerPage = int.Parse(Console.ReadLine());
            Console.WriteLine("Which page woul you like to see?");
            int pageNumber = int.Parse(Console.ReadLine());

            ApiTest test = new ApiTest();
            test.GetData(pageNumber, coctailsPerPage).Wait();
            int nothing = int.Parse(Console.ReadLine());
        }
    }
}