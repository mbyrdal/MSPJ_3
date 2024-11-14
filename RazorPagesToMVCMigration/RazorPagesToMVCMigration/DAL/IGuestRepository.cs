using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL
{
    public interface IGuestRepository
    {
        List<Guest> GetAllGuests();
        Guest GetGuestByID(int id);
        void CreateGuest(Guest guest);
        void UpdateGuest(Guest guest);
        void DeleteGuest(int id);
        void Save();
    }
}
