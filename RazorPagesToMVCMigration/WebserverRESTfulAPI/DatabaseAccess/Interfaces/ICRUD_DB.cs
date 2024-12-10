namespace ServiceAPI.DatabaseAccess.Interfaces
{
    public interface ICRUD_DB<T> where T : class
    {
        List<T> GetAllEntities();
        T GetByIdentifier(int id);
        int CreateEntity(T entity);
        int UpdateEntity(T entity);
        bool DeleteEntity(int id);
    }
}
