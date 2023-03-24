using Microsoft.EntityFrameworkCore;

namespace TimetablePlanning.App.Server.Data;

    public class TimetablesDbContext : DbContext
    {
        public TimetablesDbContext(DbContextOptions<TimetablesDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // NOTE: User is fully implemented in Module Registry App
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
            });
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_Country");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Person)
                    .HasForeignKey<Person>(d => d.UserId)
                    .HasConstraintName("FK_Person_User");

            });
            base.OnModelCreating(modelBuilder);
        }
    }

