namespace ServiceAPI.DatabaseAccess.Interfaces
{
    public interface ICRUD_DB<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        int Create(T entity);
        int Update(T entity);
        bool Delete(string id);
    }
}
