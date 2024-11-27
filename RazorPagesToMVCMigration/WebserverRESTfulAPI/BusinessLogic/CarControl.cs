using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic
{
    public class CarControl : ICarControl
    {
        private readonly DbCar _dbCarModelAccess;

        public CarControl(DbCar dbCarModelAccess)
        {
            _dbCarModelAccess = dbCarModelAccess;
        }

        public Car GetCarByID(int ID)
        {
            Car carModelPlaceholder = null;
            try
            {
                carModelPlaceholder = _dbCarModelAccess.GetByIdentifier(ID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return carModelPlaceholder;
        }

        public List<Car> GetAllCars()
        {
            List<Car> allCarModels = new List<Car>();
            try
            {
                allCarModels = _dbCarModelAccess.GetAllEntities();
            }
            catch (Exception ex)
            {
                allCarModels = null;
                Debug.WriteLine(ex.Message);
            }
            return allCarModels;
        }

        public bool AddCar(Car carModel)
        {
            bool carModelExists = false;
            bool wasCarModelInserted = false;
            int numberOfRowsInserted;
            try
            {
                carModelExists = _dbCarModelAccess.CarModelExists(carModel.ID, carModel.VINNumber);
                if (carModelExists) // CASE: Customer does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{carModel.ID}' and VINNumber '{carModel.VINNumber}' already exists in the CarModel table.");
                }

                // Customer does not exist
                numberOfRowsInserted = _dbCarModelAccess.CreateEntity(carModel);
                wasCarModelInserted = (numberOfRowsInserted == 1);
            }
            catch (Exception ex)
            {
                carModel = null;
                Debug.WriteLine(ex.Message);
            }
            return wasCarModelInserted;
        }

        public bool AddCarDTO(CarViewModel carModel)
        {
            bool carModelExists = false;
            bool wasCarModelInserted = false;
            int numberOfRowsInserted;
            try
            {
                carModelExists = _dbCarModelAccess.CarModelExists(carModel.ID, carModel.VINNumber);
                if (carModelExists) // CASE: CarModel does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{carModel.ID}' and VINNumber '{carModel.VINNumber}' already exists in the CarModel table.");
                }

                // CarModel does not exist
                numberOfRowsInserted = _dbCarModelAccess.CreateEntityDTO(carModel);
                wasCarModelInserted = (numberOfRowsInserted == 1);
            }
            catch (Exception ex)
            {
                carModel = null;
                Debug.WriteLine(ex.Message);
            }
            return wasCarModelInserted;
        }

        public bool UpdateCar(Car carModel)
        {
            bool carModelExists = false;
            bool wasCustomerUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                carModelExists = _dbCarModelAccess.CarModelExists(carModel.ID, carModel.VINNumber);
                if (!carModelExists) // CASE: CarModel does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{carModel.ID}' and VINNumber '{carModel.VINNumber}' does not exist in the Customer table.");
                }

                // CarModel does exist
                numberOfRowsUpdated = _dbCarModelAccess.UpdateEntity(carModel);
                wasCustomerUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasCustomerUpdated;
        }

        public bool UpdateCarDTO(CarViewModel carModel)
        {
            bool carModelExists = false;
            bool wasCustomerUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                carModelExists = _dbCarModelAccess.CarModelExists(carModel.ID, carModel.VINNumber);
                if (!carModelExists) // CASE: CarModel does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{carModel.ID}' and VINNumber '{carModel.VINNumber}' does not exist in the Customer table.");
                }

                // CarModel does exist
                numberOfRowsUpdated = _dbCarModelAccess.UpdateEntityDTO(carModel);
                wasCustomerUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasCustomerUpdated;
        }

        public bool DeleteCar(int ID)
        {
            bool carModelExists;
            bool wasCarModelDeleted = false;
            try
            {
                string carModelVINNumber = GetCarModelByID(ID).VINNumber;
                carModelExists = _dbCarModelAccess.CarModelExists(ID, carModelVINNumber);
                if (!carModelExists)
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{ID}' and VINNumber '{carModelVINNumber}' does not exist in the Customer table.");
                }

                // CASE: Customer does exist in DB
                wasCarModelDeleted = _dbCarModelAccess.DeleteEntity(ID);
            }
            catch (Exception ex)
            {
                wasCarModelDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasCarModelDeleted;
        }
    }
}
