namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface ICRUD<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(string id);
    }
}
