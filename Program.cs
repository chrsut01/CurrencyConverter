using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CurrencyConverter;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Converter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var rates = new Dictionary<string, decimal>
{
    {"USD", 1m},
    {"EUR", 0.93m},
    {"GBP", 0.76m},
    {"JPY", 130.53m},
    {"AUD", 1.31m}
};

app.MapGet("/currencyconverter", (Converter converter, string fromCurrency, string toCurrency, decimal amount) =>
    {
        if (!rates.ContainsKey(fromCurrency.ToUpper()) || !rates.ContainsKey(toCurrency.ToUpper()))
        {
            return Results.BadRequest("Unsupported currency.");
        }

        // Use the Converter class to perform the currency conversion
        decimal convertedAmount = converter.Convert(amount, fromCurrency.ToUpper(), toCurrency.ToUpper(), rates);

        var result = new Conversion
        {
            Source = fromCurrency.ToUpper(),
            Target = toCurrency.ToUpper(),
            Value = amount,
            Result = convertedAmount,
            Date = DateTime.Now
        };

        return Results.Ok(JsonSerializer.Serialize(result));
    })
    .WithName("CurrencyConverter")
    .WithMetadata(new AllowAnonymousAttribute());


app.Run();



record ConversionResult(string SourceCurrency, string TargetCurrency, decimal Value, decimal Result, DateTime Date);