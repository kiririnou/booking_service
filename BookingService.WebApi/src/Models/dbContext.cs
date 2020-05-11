using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookingService.WebApi.Models
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public DbSet<Reservation> Reservations  { get; set; }
        public DbSet<Flight> Flights            { get; set; }
        public DbSet<Country> Countries         { get; set; }
        public DbSet<User> Users                { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if (!optionsBuilder.IsConfigured)
            // {
            //     optionsBuilder
            //         .UseLazyLoadingProxies()
            //         .UseNpgsql("");
            // }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Country>()
                .HasMany(c => c.FlightsFrom)
                .WithOne(f => f.From)
                .HasForeignKey(nameof(Flight.FromId))
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Country>()
                .HasMany(c => c.FlightsTo)
                .WithOne(f => f.To)
                .HasForeignKey(nameof(Flight.ToId))
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(nameof(Reservation.UserId))
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Flight>()
                .HasMany(f => f.Reservations)
                .WithOne(r => r.Flight)
                .HasForeignKey(nameof(Reservation.FlightId))
                .OnDelete(DeleteBehavior.NoAction);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}