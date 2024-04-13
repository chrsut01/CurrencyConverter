using CurrencyConverter.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Data
{
    public class CurrencyConverterContext : DbContext
    {
        public CurrencyConverterContext(DbContextOptions<CurrencyConverterContext> options) : base(options)
        {
        }

        public DbSet<Conversion> Conversions { get; set; }
    }
}