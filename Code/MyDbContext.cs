using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace AngularScratch
{
  public partial class MyDbContext : DbContext
  {
    public virtual DbSet<User> User { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      var userBuilder = modelBuilder.Entity<User>(entity =>
      {
        entity.Property(e => e.UserId).HasColumnName("userId").HasColumnType("uniqueidentifier");
        entity.Property(e => e.FirstName).HasColumnName("firstName").HasColumnType("nvarchar(50)").IsRequired();
        entity.Property(e => e.LastName).HasColumnName("lastName").HasColumnType("nvarchar(50)").IsRequired();
        entity.Property(e => e.DOB).HasColumnName("dob").HasColumnType("date").IsRequired();
        entity.Property(e => e.Title).HasColumnName("title").HasColumnType("nvarchar(50)").HasDefaultValue<string>(string.Empty);
        entity.HasKey(c => c.UserId);
      });
    }
  }

  public class User
  {
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DOB { get; set; }
    public string Title { get; set; }
  }
}
