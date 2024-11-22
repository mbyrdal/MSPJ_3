using ServiceAPI.Models;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface IGuestControl
    {
        Guest GetGuestByEmail(string email);
        List<Guest> GetAllGuests();
        bool AddGuest(Guest guest);
        bool UpdateGuest(Guest guest);
        bool DeleteGuest(string email);
    }
}
