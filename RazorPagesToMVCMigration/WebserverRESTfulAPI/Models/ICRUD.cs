namespace ServiceAPI.DataAccess.Models
{
    public interface ICRUD<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
