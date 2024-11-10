using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        string url;
        ILogger logger;
        HttpClient client = new HttpClient();

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        // Make sure this method is asynchronous and returns a Task<List<string>>
        async Task<List<string>> GetTradeAsync()
        {
            logger.LogInfo("Connecting to the Restful server using HTTP");
            List<string> tradesString = null;

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Read the content as a string and deserialize it into a List<string>
                string content = await response.Content.ReadAsStringAsync();
                tradesString = JsonSerializer.Deserialize<List<string>>(content);
                logger.LogInfo("Received trade strings of length = " + tradesString.Count);
            }
            return tradesString;
        }

        // This method is now asynchronous and returns Task<IEnumerable<string>> 
        public async Task<IEnumerable<string>> GetTradeData()
        {
            List<string> tradeList = await GetTradeAsync();
            return tradeList ?? Enumerable.Empty<string>(); // Return empty if tradeList is null
        }
    }
}

