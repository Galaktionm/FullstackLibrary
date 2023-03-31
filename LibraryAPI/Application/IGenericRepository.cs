

namespace LibraryApp.API.Application {

    /// <summary>
    ///
    /// <summary>
    public interface IGenericRepository<T> {

        public IQueryable<T> getAll();

        public Task<T?> findByIdAsync(long id);

        public Task deleteEntityAsync(long id);
        
    }


}