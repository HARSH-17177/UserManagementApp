namespace UserRoleManagementFrontend.Infrastructure
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void CreateNew(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
    }
    public interface IRepositoryAsync<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> CreateNew(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Remove(int id);
    }
}
