using System.Collections.Generic;
using System.IO;

using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class StreamTradeDataProvider : ITradeDataProvider
    {
        private readonly Stream _stream;
        private readonly ILogger _logger;

        public StreamTradeDataProvider(Stream stream, ILogger logger)
        {
            _stream = stream;
            _logger = logger;
        }

        public Task<IEnumerable<string>> GetTradeData()
        {
            var tradeData = new List<string>();
            _logger.LogInfo("Reading trades from file stream.");

            // Use StreamReader to read from the stream
            using (var reader = new StreamReader(_stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tradeData.Add(line);
                }
            }

            // Wrap the result in a Task using Task.FromResult for a synchronous result
            return Task.FromResult((IEnumerable<string>)tradeData);
        }
    }
}


