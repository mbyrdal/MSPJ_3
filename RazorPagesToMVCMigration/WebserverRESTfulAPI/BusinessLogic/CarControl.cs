using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic
{
    public class CarControl : ICarControl
    {
        private readonly DbCar _dbCarAccess;

        public CarControl(DbCar dbCarAccess)
        {
            _dbCarAccess = dbCarAccess;
        }


        public bool AddCar(Car car)
        {
            bool carExists = false;
            bool wasCarInserted = false;
            int numberOfRowsInserted;
            try
            {
                carExists = _dbCarAccess.CarExists(car.ID, car.VINNumber);
                if (carExists) // CASE: Car does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A Car with the ID '{car.ID}' and VINNumber '{car.VINNumber}' already exists in the CarModel table.");
                }

                // Customer does not exist
                numberOfRowsInserted = _dbCarAccess.CreateEntity(car);
                wasCarInserted = (numberOfRowsInserted == 1);
            }
            catch (Exception ex)
            {
                car = null;
                Debug.WriteLine(ex.Message);
            }
            return wasCarInserted;
        }

        public bool AddCarDTO(CarViewModel car)
        {
            bool carExists = false;
            bool wasCarInserted = false;
            int numberOfRowsInserted;
            try
            {
                carExists = _dbCarAccess.CarExists(car.ID, car.VINNumber);
                if (carExists) // CASE: Car does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A Car with the ID '{car.ID}' and VINNumber '{car.VINNumber}' already exists in the CarModel table.");
                }

                // Car does not exist
                numberOfRowsInserted = _dbCarAccess.CreateEntityDTO(car);
                wasCarInserted = (numberOfRowsInserted == 1);
            }
            catch (Exception ex)
            {
                car = null;
                Debug.WriteLine(ex.Message);
            }
            return wasCarInserted;
        }

        public List<Car> GetAllCars()
        {
            List<Car> allCars = new List<Car>();
            try
            {
                allCars = _dbCarAccess.GetAllEntities();
            }
            catch (Exception ex)
            {
                allCars = null;
                Debug.WriteLine(ex.Message);
            }
            return allCars;
        }

        public Car GetCarByID(int ID)
        {
            Car carPlaceholder = null;
            try
            {
                carPlaceholder = _dbCarAccess.GetByIdentifier(ID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return carPlaceholder;
        }

        public bool UpdateCar(Car car)
        {
            bool carExists = false;
            bool wasCarUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                carExists = _dbCarAccess.CarExists(car.ID, car.VINNumber);
                if (!carExists) // CASE: Car does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A Car with the ID '{car.ID}' and VINNumber '{car.VINNumber}' does not exist in the Customer table.");
                }

                // CarModel does exist
                numberOfRowsUpdated = _dbCarAccess.UpdateEntity(car);
                wasCarUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasCarUpdated;
        }

        public bool UpdateCarDTO(CarViewModel car)
        {
            bool carExists = false;
            bool wasCarUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                carExists = _dbCarAccess.CarExists(car.ID, car.VINNumber);
                if (!carExists) // CASE: Car does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A Car with the ID '{car.ID}' and VINNumber '{car.VINNumber}' does not exist in the Car table.");
                }

                // Cardoes exist
                numberOfRowsUpdated = _dbCarAccess.UpdateEntityDTO(car);
                wasCarUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasCarUpdated;
        }

        public bool DeleteCar(int ID)
        {
            bool carExists;
            bool wasCarDeleted = false;
            try
            {
                string carVinNumber = GetCarByID(ID).VINNumber;
                carExists = _dbCarAccess.CarExists(ID, carVinNumber);
                if (!carExists)
                {
                    throw new InvalidOperationException($"A Car with the ID '{ID}' and VINNumber '{carVinNumber}' does not exist in the Car table.");
                }

                // CASE: Car does exist in DB
                wasCarDeleted = _dbCarAccess.DeleteEntity(ID);
            }
            catch (Exception ex)
            {
                wasCarDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasCarDeleted;
        }
    }
}
