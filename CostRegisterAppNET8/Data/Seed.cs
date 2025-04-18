﻿using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Data;

public class Seed
{
    public static async Task SeedCostCategories(DataContext context)
    {
        // NOTE: To prevent duplicate data.
        if (await context.CostCategories.AnyAsync())
        {
            return;
        }

        var costCategories = await File.ReadAllTextAsync("Data/CostCategorySeed.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var costCategoriesData = JsonSerializer.Deserialize<List<CostCategory>>(costCategories, options);

        if (costCategoriesData == null)
        {
            return;
        }

        await context.CostCategories.AddRangeAsync(costCategoriesData);
    }

    public static async Task SeedIncomeCategories(DataContext context)
    {
        // NOTE: To prevent duplicate data.
        if (await context.IncomeCategories.AnyAsync())
        {
            return;
        }

        var incomeCategories = await File.ReadAllTextAsync("Data/IncomeCategorySeed.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var incomeCategoriesData = JsonSerializer.Deserialize<List<IncomeCategory>>(incomeCategories, options);

        if (incomeCategoriesData == null)
        {
            return;
        }

        await context.IncomeCategories.AddRangeAsync(incomeCategoriesData);
    }

    public static async Task SeedCurrencies(DataContext context)
    {
        // NOTE: To prevent duplicate data.
        if (await context.Currencies.AnyAsync())
        {
            return;
        }

        var currencies = await File.ReadAllTextAsync("Data/Currencies.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var currenciesData = JsonSerializer.Deserialize<List<Currency>>(currencies, options);

        if (currenciesData == null)
        {
            return;
        }

        await context.Currencies.AddRangeAsync(currenciesData);
    }
}
