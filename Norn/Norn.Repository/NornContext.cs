using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
namespace Norn.Repository;

public class NornContext : DbContext
{
    public NornContext(DbContextOptions<NornContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TimeInterval> TimeIntervals { get; set; }
    public DbSet<BookingStatus> BookingStatuses { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "User" }
        );

        modelBuilder.Entity<User>().HasOne<Role>()
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
