using CurrencyConverter.Models;
using System.Threading.Tasks;

namespace CurrencyConverter.Data
{
    public interface IConversionRepository
    {
        Task AddConversionAsync(Conversion conversion);
        Task<IEnumerable<Conversion>> GetConversionsAsync();
    }
}