using FeedbackService.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Rating Property Configuration
            modelBuilder.Entity<Rating>()
            .HasKey(e => e.Id);

            modelBuilder.Entity<Rating>()
                .Property(e => e.TitleId)
                .IsRequired();

            //Enum Null Configuration   
            modelBuilder
                .Entity<Rating>()
                .Property(s => s.RatingValue)
                .HasConversion(
                v => v.HasValue ? v.Value.ToString() : null,
                v => string.IsNullOrEmpty(v) ? (RatingEnum?)null : (RatingEnum)Enum.Parse(typeof(RatingEnum), v));

            modelBuilder.Entity<Rating>().ToTable("ratings");
        }
        public DbSet<Rating> Ratings { get; set; } = default!;
    }
}