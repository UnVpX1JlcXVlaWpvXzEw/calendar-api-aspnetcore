namespace HustleAddiction.Platform.CalendarApi.Domain.SeedWork
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(TEntity[] entity, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity?> GetAsync(Guid uuid, CancellationToken cancellationToken = default);

        Task<bool> Remove(TEntity entity, CancellationToken cancellationToken = default);

        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    }
}
