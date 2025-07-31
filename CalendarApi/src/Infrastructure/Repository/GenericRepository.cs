namespace HustleAddiction.Platform.CalendarApi.Infrastructure.Repository
{
    using Domain.SeedWork;
    using Microsoft.EntityFrameworkCore;

    public class GenericRepository<Entity>(CalendarAPIDbContext context) : IRepository<Entity>
            where Entity : EntityBase
    {
        protected readonly CalendarAPIDbContext Context = context;

        protected readonly DbSet<Entity> Entities = context.Set<Entity>();

        public IUnitOfWork UnitOfWork => this.Context;

        public async Task<Entity> AddAsync(Entity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await this.Entities.AddAsync(entity, cancellationToken);

            return entityEntry.Entity;
        }

        public async Task AddRangeAsync(Entity[] entity, CancellationToken cancellationToken = default)
        {
            await this.Entities.AddRangeAsync(entity);
        }

        public async Task<List<Entity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await this.Entities.ToListAsync(cancellationToken);
        }

        public async Task<Entity?> GetAsync(Guid uuid, CancellationToken cancellationToken = default)
        {
            return await this.Entities.FirstOrDefaultAsync(e => e.UUId == uuid, cancellationToken);
        }

        public Task<bool> Remove(Entity entity, CancellationToken cancellationToken = default)
        {
            this.Entities.Remove(entity);

            return Task.FromResult(true);
        }

        public Task<Entity> Update(Entity entity, CancellationToken cancellationToken = default)
        {
            var dataEntity = this.Entities.Update(entity);

            return Task.FromResult(dataEntity.Entity);
        }
    }
}
