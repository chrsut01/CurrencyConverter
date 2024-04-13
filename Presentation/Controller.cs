using CurrencyConverter.Data;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Presentation
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly Converter _converter;
        private readonly IConversionRepository _conversionRepository;

        public Controller(Converter converter, IConversionRepository conversionRepository)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
            _conversionRepository = conversionRepository ?? throw new ArgumentNullException(nameof(conversionRepository));
        }

        [HttpGet("convert")]
        public IActionResult Convert(string fromCurrency, string toCurrency, decimal amount)
        {
            var rates = new Dictionary<string, decimal>
            {
                {"USD", 1m},
                {"EUR", 0.93m},
                {"GBP", 0.76m},
                {"JPY", 130.53m},
                {"AUD", 1.31m}
            };

            if (!rates.ContainsKey(fromCurrency.ToUpper()) || !rates.ContainsKey(toCurrency.ToUpper()))
            {
                return BadRequest("Unsupported currency.");
            }

            decimal convertedAmount = _converter.Convert(amount, fromCurrency.ToUpper(), toCurrency.ToUpper(), rates);

            var result = new Conversion
            {
                Source = fromCurrency.ToUpper(),
                Target = toCurrency.ToUpper(),
                Value = amount,
                Result = convertedAmount,
                Date = DateTime.Now
            };

            // Save the conversion to the database
            _conversionRepository.AddConversionAsync(result);

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var conversions = await _conversionRepository.GetConversionsAsync();
            return Ok(conversions);
        }
    }
}
