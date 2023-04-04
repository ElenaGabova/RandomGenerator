using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Access for Numbers table
        /// </summary>
        public DbSet<NumberEntity> Numbers { get; set; }
        /// <summary>
        /// Access for NumberRepetitions table
        /// </summary>
        public DbSet<NumberRepetitionEntity> NumberRepetitions { get; set; }

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<NumberEntity>(NumberEntityConfigure); 
            modelBuilder.Entity<NumberRepetitionEntity>(NumberRepetitionEntityConfigure);
        }

        // конфигурация для типа NumberRepetitionEntity
        public void NumberRepetitionEntityConfigure(EntityTypeBuilder<NumberRepetitionEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Number).IsRequired();
            builder.Property(p => p.RepetitionAmount).IsRequired();
        }

        // конфигурация для типа NumberEntity
        public void NumberEntityConfigure(EntityTypeBuilder<NumberEntity> builder)
        {   
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Number).IsRequired();
        }
     
    }
}