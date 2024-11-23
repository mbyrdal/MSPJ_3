using ServiceAPI.Models;

namespace ServiceAPI.DatabaseAccess.Interfaces
{
    public interface ICRUD_DB_Guest : ICRUD_DB<Guest>
    {
        int UpdateEntityWithParameters(string email, Guest entity);
    }
}
