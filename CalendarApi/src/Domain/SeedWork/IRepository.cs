namespace HustleAddiction.Platform.CalendarApi.Domain.SeedWork
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken token);

        Task AddRangeAsync(TEntity[] entity, CancellationToken token);

        Task<List<TEntity>> GetAllAsync(CancellationToken token);

        Task<TEntity?> GetAsync(Guid uuid, CancellationToken token);

        Task<bool> Remove(TEntity entity, CancellationToken token);

        Task<TEntity> Update(TEntity entity, CancellationToken token);
    }
}
