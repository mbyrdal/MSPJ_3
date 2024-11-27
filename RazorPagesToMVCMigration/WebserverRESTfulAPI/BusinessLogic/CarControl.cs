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
                    throw new InvalidOperationException($"A CarModel with the ID '{car.ID}' and VINNumber '{car.VINNumber}' already exists in the CarModel table.");
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

        public bool AddCarDTO(CarViewModel carModel)
        {
            bool carModelExists = false;
            bool wasCarModelInserted = false;
            int numberOfRowsInserted;
            try
            {
                carModelExists = _dbCarAccess.CarExists(carModel.ID, carModel.VINNumber);
                if (carModelExists) // CASE: CarModel does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{carModel.ID}' and VINNumber '{carModel.VINNumber}' already exists in the CarModel table.");
                }

                // CarModel does not exist
                numberOfRowsInserted = _dbCarAccess.CreateEntityDTO(carModel);
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
                carModelExists = _dbCarAccess.CarExists(carModel.ID, carModel.VINNumber);
                if (!carModelExists) // CASE: CarModel does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{carModel.ID}' and VINNumber '{carModel.VINNumber}' does not exist in the Customer table.");
                }

                // CarModel does exist
                numberOfRowsUpdated = _dbCarAccess.UpdateEntity(carModel);
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
                carModelExists = _dbCarAccess.CarExists(carModel.ID, carModel.VINNumber);
                if (!carModelExists) // CASE: CarModel does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{carModel.ID}' and VINNumber '{carModel.VINNumber}' does not exist in the Customer table.");
                }

                // CarModel does exist
                numberOfRowsUpdated = _dbCarAccess.UpdateEntityDTO(carModel);
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
                string carModelVINNumber = GetCarByID(ID).VINNumber;
                carModelExists = _dbCarAccess.CarExists(ID, carModelVINNumber);
                if (!carModelExists)
                {
                    throw new InvalidOperationException($"A CarModel with the ID '{ID}' and VINNumber '{carModelVINNumber}' does not exist in the Customer table.");
                }

                // CASE: Customer does exist in DB
                wasCarModelDeleted = _dbCarAccess.DeleteEntity(ID);
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
