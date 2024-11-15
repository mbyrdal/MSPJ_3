namespace RazorPagesToMVCMigration.DAL.Interface
{
    public interface ICRUD<T> where T : class
    {
        IEnumerable<T> GetAllEntities();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
