using System.Collections.Generic;
using System.Linq;
using BookOrder.Data;
using BookOrder.Models;
using Microsoft.EntityFrameworkCore;

namespace BookOrder.Services;

public class BookService
{
    private readonly OrderDbContextFactory _factory = new();

    private OrderDbContext GetDbContext() => _factory.CreateDbContext(System.Array.Empty<string>());

    public IEnumerable<Book> GetAll()
    {
        using var db = GetDbContext();
        return db.Books.AsNoTracking().OrderBy(b => b.Title).ToList();
    }

    public void Add(Book book)
    {
        using var db = GetDbContext();
        db.Books.Add(book);
        db.SaveChanges();
    }

    public void Delete(int bookId)
    {
        using var db = GetDbContext();
        var book = db.Books.FirstOrDefault(b => b.Id == bookId);
        if (book != null)
        {
            db.Books.Remove(book);
            db.SaveChanges();
        }
    }
}
