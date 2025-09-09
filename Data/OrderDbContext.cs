using Microsoft.EntityFrameworkCore;
using BookOrder.Models;

namespace BookOrder.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<OrderBook> OrderBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.ReaderName).IsRequired();
            entity.Property(o => o.ReaderAddress).IsRequired();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.BookCode).IsRequired();
            entity.Property(b => b.Title).IsRequired();
            entity.Property(b => b.Author).IsRequired();
        });

        modelBuilder.Entity<OrderBook>(entity =>
        {
            entity.HasKey(ob => ob.Id);

            entity.HasOne(ob => ob.Order)
                  .WithMany(o => o.OrderBooks)
                  .HasForeignKey(ob => ob.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ob => ob.Book)
                  .WithMany(b => b.OrderBooks)
                  .HasForeignKey(ob => ob.BookId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
