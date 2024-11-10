using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLTradeDataProvider : ITradeDataProvider
    {
        private readonly string url;
        private readonly ILogger logger;

        public URLTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        // Update GetTradeData to return Task<IEnumerable<string>> and make it async
        public async Task<IEnumerable<string>> GetTradeData()
        {
            List<string> tradeData = new List<string>();
            logger.LogInfo("Reading trades from URL: " + url);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);  // Use await here
                if (!response.IsSuccessStatusCode)
                {
                    logger.LogWarning($"Failed to retrieve data. Status code: {response.StatusCode}");
                    throw new Exception($"Error retrieving data from URL: {url}");
                }

                // Use ReadAsStreamAsync and StreamReader.ReadLineAsync for async reading
                using (Stream stream = await response.Content.ReadAsStreamAsync())  // Use await here
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)  // Use await here
                    {
                        tradeData.Add(line);
                    }
                }
            }
            // Return the tradeData list directly; no need to wrap it in Task.FromResult
            return tradeData;
        }
    }
}
