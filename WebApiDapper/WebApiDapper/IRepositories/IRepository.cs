namespace WebApiDapper.IRepositories
{
    public interface IRepository<T, K> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(K id);
        public Task<K?> AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int id);
        public Task<List<T>> GetPagingAsync(int pageNumber, int pageSize);
    }
}
