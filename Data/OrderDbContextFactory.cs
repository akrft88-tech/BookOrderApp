using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace BookOrder.Data;

public class OrderDbContextFactory
{
    private static string GetDatabasePath()
    {
        var projectRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
        var dbPath = Path.Combine(projectRoot, "orders.db");
        return Path.GetFullPath(dbPath);
    }

    public OrderDbContext CreateDbContext(string[] args)
    {
        var dbPath = GetDatabasePath();
        var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
        return new OrderDbContext(optionsBuilder.Options);
    }
}
