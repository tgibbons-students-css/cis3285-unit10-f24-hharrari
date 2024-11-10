using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class AdjustTradeDataProvider : ITradeDataProvider
    {
        ITradeDataProvider origProvider;
        private IEnumerable<String> lines;

        AdjustTradeDataProvider(ITradeDataProvider origProvider)
        {
            this.origProvider = origProvider;
        }
        public IEnumerable<string> GetTradeData()
        {
            // calling the original one
            IEnumerable<string> lines = origProvider.GetTradeData();

            List<String> result = new List<string>();

            foreach (String line in lines)
            {
                String newLine = line.Replace("GBP", "EUR");
                result.Add(newLine);
            }
            return lines;
        }

    }
    

}
