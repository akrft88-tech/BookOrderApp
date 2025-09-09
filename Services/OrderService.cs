using System;
using System.Collections.Generic;
using System.Linq;
using BookOrder.Data;
using BookOrder.Models;
using Microsoft.EntityFrameworkCore;

namespace BookOrder.Services;

public class OrderService
{
    private readonly OrderDbContextFactory _factory = new();
    private OrderDbContext GetDbContext() => _factory.CreateDbContext(System.Array.Empty<string>());
    // Alle Bestellungen laden, inkl. Bücher
    public IEnumerable<Order> GetAll()
    {
        using var db = GetDbContext();
        return db.Orders
                 .Include(o => o.OrderBooks)
                 .ThenInclude(ob => ob.Book)
                 .AsNoTracking()
                 .OrderBy(o => o.Id)
                 .ToList();
    }

    // Neue Bestellung hinzufügen
    public void Add(string readerName, string readerAddress, List<(int bookId, int quantity)> books)
    {
        if (string.IsNullOrWhiteSpace(readerName) || string.IsNullOrWhiteSpace(readerAddress))
            throw new ArgumentException("Name und Adresse müssen ausgefüllt sein.");
        if (books == null || books.Count == 0)
            throw new ArgumentException("Es muss mindestens ein Buch bestellt werden.");

        using var db = GetDbContext();

        var order = new Order
        {
            ReaderName = readerName,
            ReaderAddress = readerAddress,
            OrderBooks = books.Select(b => new OrderBook
            {
                BookId = b.bookId,
                Quantity = b.quantity
            }).ToList()
        };

        db.Orders.Add(order);
        db.SaveChanges();
    }

    // Bestellung löschen
    public void Delete(int orderId)
    {
        using var db = GetDbContext();
        var order = db.Orders
                      .Include(o => o.OrderBooks)
                      .FirstOrDefault(o => o.Id == orderId);

        if (order != null)
        {
            db.Orders.Remove(order);
            db.SaveChanges();
        }
    }

    // Bestellung aktualisieren
    public void Update(Order updatedOrder)
    {
        using var db = GetDbContext();

        var existingOrder = db.Orders
            .Include(o => o.OrderBooks)
            .FirstOrDefault(o => o.Id == updatedOrder.Id);

        if (existingOrder != null)
        {
            existingOrder.ReaderName = updatedOrder.ReaderName;
            existingOrder.ReaderAddress = updatedOrder.ReaderAddress;

            db.OrderBooks.RemoveRange(existingOrder.OrderBooks);
            existingOrder.OrderBooks = updatedOrder.OrderBooks;

            db.SaveChanges();
        }
    }

    // Suche
    public IEnumerable<Order> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return GetAll();

        query = query.ToLower();

        using var db = GetDbContext();
        return db.Orders
                 .Include(o => o.OrderBooks)
                 .ThenInclude(ob => ob.Book)
                 .AsNoTracking()
                 .Where(o => o.ReaderName.ToLower().Contains(query) ||
                             o.OrderBooks.Any(ob => ob.Book != null && ob.Book.Title.ToLower().Contains(query)))
                 .OrderBy(o => o.Id)
                 .ToList();
    }
}
