using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic
{
    public class GuestControl : IGuestControl
    {
        private readonly DbGuest _dbGuestAccess;

        public GuestControl(DbGuest dbGuestAccess)
        {
            _dbGuestAccess = dbGuestAccess;
        }

        public Guest GetGuestByEmail(string email)
        {
            Guest guestPlaceholder = null;

            try
            {
                guestPlaceholder = _dbGuestAccess.GetByIdentifier(email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return guestPlaceholder;
        }

        public List<Guest> GetAllGuests()
        {
            List<Guest> allGuests = new List<Guest>();

            try
            {
                allGuests = _dbGuestAccess.GetAllEntities();
            }
            catch (Exception ex)
            {
                allGuests = null;
                Debug.WriteLine(ex);
            }

            return allGuests;
        }

        public bool AddGuest(Guest guest)
        {
            bool guestExists = false;
            bool wasGuestInserted = false;
            int numberOfRowsInserted;
            
            try
            {
                guestExists = _dbGuestAccess.GuestExists(guest.Email);
                if(!guestExists) // CASE: Guest does not exist in DB
                {
                    numberOfRowsInserted = _dbGuestAccess.CreateEntity(guest);
                    wasGuestInserted = (numberOfRowsInserted == 1);
                }
            }
            catch (Exception ex)
            {
                guest = null;
                Debug.WriteLine(ex.Message);
            }

            return wasGuestInserted;
        }

        public bool UpdateGuest(Guest guest)
        {
            bool guestExists = false;
            bool wasGuestUpdated = false;
            int numberOfRowsUpdated;

            try
            {
                guestExists = _dbGuestAccess.GuestExists(guest.Email);
                if (guestExists) // CASE: Guest does exist in DB
                {
                    numberOfRowsUpdated = _dbGuestAccess.UpdateEntity(guest);
                    wasGuestUpdated = (numberOfRowsUpdated == 1);
                }
            }
            catch(Exception ex)
            {
                guest = null;
                Debug.WriteLine(ex.Message);
            }
            return wasGuestUpdated;
        }

        public bool DeleteGuest(string email)
        {
            bool guestExists = false;
            bool wasGuestDeleted = false;

            try
            {
                guestExists = _dbGuestAccess.GuestExists(email);
                if(!guestExists)
                {
                    throw new InvalidOperationException($"A Guest with the email '{email}' does not exist in the dbo.Guest table.");
                }

                wasGuestDeleted = _dbGuestAccess.DeleteEntity(email);
            }
            catch (Exception ex)
            {
                wasGuestDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasGuestDeleted;
        }
    }
}
