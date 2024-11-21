namespace ServiceAPI.DatabaseAccess.Interfaces
{
    public interface ICRUD_DB<T> where T : class
    {
        IEnumerable<T> GetAllEntities();
        T GetByIdentifier(string id);
        int CreateEntity(T entity);
        int UpdateEntity(T entity);
        bool DeleteEntity(string id);
    }
}
