using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceAPI.BusinessLogic
{
    public class CarPartControl : ICarPartControl
    {
        private readonly DbCarPart _dbCarPartAccess;
        private readonly DbProduct _dbProductAccess;

        public CarPartControl(DbCarPart dbCarPartAccess, DbProduct dbProductAccess)
        {
            _dbCarPartAccess = dbCarPartAccess;
            _dbProductAccess = dbProductAccess;
        }

        public bool AddCarPart(CarPart carPart)
        {
            bool carPartExists = false;
            bool wasCarPartInserted = false;
            int numberOfRowsInserted;
            try
            {
                carPartExists = _dbCarPartAccess.CarPartExists(carPart.ID);
                if (carPartExists) // CASE: CarPart does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A Car part with the ID '{carPart.ID}' and Name '{carPart.Name}' already exists in the CarModel table.");
                }

                // Customer does not exist
                numberOfRowsInserted = _dbCarPartAccess.CreateEntity(carPart);
                wasCarPartInserted = (numberOfRowsInserted == 1);
            }
            catch (Exception ex)
            {
                carPart = null;
                Debug.WriteLine(ex.Message);
            }
            return wasCarPartInserted;
        }

        public bool AddCarPartDTO(CarPartViewModel carPart)
        {
            bool carPartExists = false;
            bool wasCarPartInserted = false;
            int numberOfRowsInserted;
            try
            {
                carPartExists = _dbCarPartAccess.CarPartExists(carPart.ID);
                if (carPartExists) // CASE: CarPart does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A car part with the ID '{carPart.ID}' and already exists in the CarPart table.");
                }

                // Car does not exist
                numberOfRowsInserted = _dbCarPartAccess.CreateEntityDTO(carPart);
                wasCarPartInserted = (numberOfRowsInserted == 1);
            }
            catch (Exception ex)
            {
                carPart = null;
                Debug.WriteLine(ex.Message);
            }
            return wasCarPartInserted;
        }

        public bool DeleteCarPart(int ID)
        {
            bool carPartExists;
            bool wasCarPartDeleted = false;
            try
            {
                string carPartName = GetCarPartByID(ID).Name;
                carPartExists = _dbCarPartAccess.CarPartExists(ID);
                if (!carPartExists)
                {
                    throw new InvalidOperationException($"A Car part with the ID '{ID}' and Name '{carPartName}' already exists in the CarPart table.");
                }

                // CASE: Car does exist in DB
                wasCarPartDeleted = _dbCarPartAccess.DeleteEntity(ID);
            }
            catch (Exception ex)
            {
                wasCarPartDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasCarPartDeleted;
        }

        public List<CarPart> GetAllCarParts()
        {
            List<CarPart> allCarParts = new List<CarPart>();
            try
            {
                allCarParts = _dbCarPartAccess.GetAllEntities();
            }
            catch (Exception ex)
            {
                allCarParts = null;
                Debug.WriteLine(ex.Message);
            }
            return allCarParts;
        }

        public CarPart GetCarPartByID(int ID)
        {
            CarPart carPartPlaceholder = null;
            try
            {
                carPartPlaceholder = _dbCarPartAccess.GetByIdentifier(ID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return carPartPlaceholder;
        }

        public string GetCarPartName(int ID)
        {
            string tempName = "";
            try
            {
                tempName = _dbProductAccess.GetProductNameByID(ID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine (ex.Message);
            }
            return tempName;
        }

        public bool UpdateCarPart(CarPart carPart)
        {
            bool carPartExists = false;
            bool wasCarPartUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                carPartExists = _dbCarPartAccess.CarPartExists(carPart.ID);
                if (!carPartExists) // CASE: Car does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A Car part with the ID '{carPart.ID}' and Name '{carPart.Name}' does not exist in the CarPart table.");
                }

                // CarModel does exist
                numberOfRowsUpdated = _dbCarPartAccess.UpdateEntity(carPart);
                wasCarPartUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasCarPartUpdated;
        }

        public bool UpdateCarPartDTO(CarPartViewModel carPart)
        {
            bool carPartExists = false;
            bool wasCarPartUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                carPartExists = _dbCarPartAccess.CarPartExists(carPart.ID);
                if (!carPartExists) // CASE: Car does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A Car part with the ID '{carPart.ID}' and Name '{carPart.Name}' does not exist in the CarPart table.");
                }

                // CarModel does exist
                numberOfRowsUpdated = _dbCarPartAccess.UpdateEntityDTO(carPart);
                wasCarPartUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasCarPartUpdated;
        }
    }
}
