using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLAsyncProvider : ITradeDataProvider
    {
        ITradeDataProvider origProvider;
        public URLAsyncProvider(ITradeDataProvider origProvider)
        {
            this.origProvider = origProvider;
        }


        public Task<IEnumerable<string>> GetTradeData()
        {
            // Return the task without waiting for the result, ensuring it's asynchronous
            return Task.Run(() => origProvider.GetTradeData());
        }

    }
}

