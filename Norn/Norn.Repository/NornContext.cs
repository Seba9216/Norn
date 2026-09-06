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
    public DbSet<OpenTime> OpenTimes { get; set; }
    public DbSet<Organisation> Organisations { get; set; }
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
        modelBuilder.Entity<Role>()
    .HasIndex(x => x.RoleName)
    .IsUnique();


        modelBuilder.Entity<Organisation>()
            .HasMany(x => x.Rooms).WithMany(x => x.Organisations);


        modelBuilder.Entity<Booking>()
            .HasOne(x => x.BookingStatus)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.BookingStatusId);

        modelBuilder.Entity<Booking>()
            .HasOne(x => x.TimeInterval)
            .WithOne(x => x.Booking)
            .HasForeignKey<Booking>(x => x.TimeIntervalId);

        modelBuilder.Entity<Booking>()
            .HasOne(x => x.User)
            .WithMany(x => x.Bookings).HasForeignKey(x => x.UserId); 


        modelBuilder.Entity<Room>()
            .HasMany(x => x.TimeIntervals)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        

        modelBuilder.Entity<Room>()
            .HasMany(x => x.Bookings)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();
        modelBuilder.Entity<User>().HasOne<Role>()
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
