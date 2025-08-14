namespace HustleAddiction.Platform.CalendarApi.Infrastructure
{
    using Domain.SeedWork;
    using HustleAddiction.Platform.CalendarApi.Infrastructure.EntityConfiguration.Calendar;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CalendarAPIDbContext(
        IConfiguration configuration,
        DbContextOptions<CalendarAPIDbContext> options) : DbContext(options), IUnitOfWork
    {

        private const string DatabaseConnectionSection = "DatabaseConnectionString";

        private readonly IConfiguration configuration = configuration;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {

            this.AddTimestamps();

            await this.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySql(
                    this.configuration.GetSection(DatabaseConnectionSection).Value,
                    new MySqlServerVersion(new Version(8, 0, 28)));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CalendarEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EventEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecurrenceRuleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReminderEntityTypeConfiguration());

            var properties = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));

            foreach (var property in properties)
            {
                property.SetColumnType("decimal(18,5)");
            }
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is EntityBase
                    && (x.State is EntityState.Added || x.State is EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State is EntityState.Added)
                {
                    ((EntityBase)entity.Entity).CreationDate = DateTime.UtcNow;
                }

                ((EntityBase)entity.Entity).ModificationDate = DateTime.UtcNow;
            }
        }
    }
}
