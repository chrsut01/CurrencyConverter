namespace CurrencyConverter.Models;

public class Conversion
{

        public string Source { get; set; }
        public string Target { get; set; }
        public decimal Value { get; set; }
        public decimal Result { get; set; }
        public DateTime Date { get; set; }
}
