using System.Collections.Generic;

namespace CurrencyConverter
{
    public class Converter
    {
        public decimal Convert(decimal amount, string fromCurrency, string toCurrency, Dictionary<string, decimal> rates)
        {
            decimal rateToUSD = rates[fromCurrency];
            decimal amountInUSD = amount / rateToUSD;
            decimal targetRate = rates[toCurrency];
            return amountInUSD * targetRate;
        }
    }
}