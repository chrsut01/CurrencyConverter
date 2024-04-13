using CurrencyConverter.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Data
{
    public class ConversionRepository : IConversionRepository
    {
        private readonly CurrencyConverterContext _context;

        public ConversionRepository(CurrencyConverterContext context)
        {
            _context = context;
        }

        public async Task AddConversionAsync(Conversion conversion)
        {
            _context.Conversions.Add(conversion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Conversion>> GetConversionsAsync()
        {
            return await _context.Conversions.ToListAsync();
        }
    }
}