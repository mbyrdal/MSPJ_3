namespace BrowserWebPage.BusinessLogic.Interfaces
{
    public interface ICRUD<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(int ID);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(int ID);
    }
}
