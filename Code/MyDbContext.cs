using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AngularScratch
{
  public partial class MyDbContext : DbContext
  {
    public virtual DbSet<User> User { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    //manually mapping entities.. Can't stand generated code
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

    //Example of SP support in EF.. Not pretty
    public async Task<(bool found, User user)> TryGetUserByIdUsingSP(Guid userId)
    {
      var param = new SqlParameter("@userId", SqlDbType.UniqueIdentifier) { Value = userId };
      var users = this.User.FromSql($"GetUser @userId", param);
      var user = await users.FirstOrDefaultAsync();
      return (user != null, user);
    }
  }
}
