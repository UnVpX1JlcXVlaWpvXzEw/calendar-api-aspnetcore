namespace HustleAddiction.Platform.CalendarApi.Domain.Repository
{
    using Domain.SeedWork;
    using Microsoft.EntityFrameworkCore;

    public class GenericRepository<Entity> : IRepository<Entity>
            where Entity : EntityBase
    {
        protected readonly CalendarAPIDbContext Context;

        protected readonly DbSet<Entity> Entities;

        public GenericRepository(CalendarAPIDbContext context)
        {
            this.Context = context;

            this.Entities = context.Set<Entity>();
        }

        public IUnitOfWork UnitOfWork => this.Context;

        public async Task<Entity> AddAsync(Entity entity, CancellationToken token)
        {
            var entityEntry = await this.Entities.AddAsync(entity, token);

            return entityEntry.Entity;
        }

        public async Task AddRangeAsync(Entity[] entity, CancellationToken token)
        {
            await this.Entities.AddRangeAsync(entity);
        }

        public async Task<List<Entity>> GetAllAsync(CancellationToken token)
        {
            return await this.Entities.ToListAsync(token);
        }

        public async Task<Entity> GetAsync(Guid uuid, CancellationToken token)
        {
            return await this.Entities.FirstOrDefaultAsync(e => e.UUId == uuid, token);
        }

        public async Task<bool> Remove(Entity entity, CancellationToken token)
        {
            await Task.FromResult(this.Entities.Remove(entity));

            return true;
        }

        public async Task<Entity> Update(Entity entity, CancellationToken token)
        {
            var dataEntity = await Task.FromResult(this.Entities.Update(entity));

            return dataEntity.Entity;
        }
    }
}
