
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class TradeProcessor
    {
        public TradeProcessor(ITradeDataProvider tradeDataProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            this.tradeDataProvider = tradeDataProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }

        // Make ProcessTrades async because GetTradeData is now async
        public async Task ProcessTrades()
        {
            // Await the asynchronous GetTradeData call
            var lines = await tradeDataProvider.GetTradeData();  // Now awaited
            var trades = tradeParser.Parse(lines);
            tradeStorage.Persist(trades);
        }

        private readonly ITradeDataProvider tradeDataProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;
    }
}

