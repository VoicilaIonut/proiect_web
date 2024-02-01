using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Game> Games { get; set; }
        //public DbSet<Board> Boards { get; set; }

        public DbSet<Move> Moves { get; set; }

        public DbSet<ShipCoord> ShipsCoord { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=database_test;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });


            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<Game>()
               .HasOne(g => g.Player1)
               .WithMany(u => u.Player1Games)
               .HasForeignKey(g => g.Player1Id)
               .OnDelete(DeleteBehavior.Restrict); // Do not cascade on delete for Player1;

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Player2)
                .WithMany(u => u.Player2Games)
                .HasForeignKey(g => g.Player2Id)
                .OnDelete(DeleteBehavior.Restrict); // Do not cascade on delete for Player2;

            modelBuilder.Entity<Move>().HasKey(move => new { move.MoveId, move.GameId, move.UserId });
            modelBuilder.Entity<Move>()
                .HasOne(m => m.Game)
                .WithMany(g => g.Moves)
                .HasForeignKey(m => m.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Move>()
                .HasOne(m => m.User)
                .WithMany(u => u.Moves)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShipCoord>().HasKey(ship => new { ship.ShipId, ship.GameId, ship.UserId });
            modelBuilder.Entity<ShipCoord>()
                .HasOne(s => s.Game)
                .WithMany(g => g.ShipsCoord)
                .HasForeignKey(s => s.GameId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<ShipCoord>()
                .HasOne(s => s.User)
                .WithMany(u => u.ShipsCoord)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
