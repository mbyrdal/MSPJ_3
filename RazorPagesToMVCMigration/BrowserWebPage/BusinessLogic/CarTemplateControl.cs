using BrowserWebPage.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using BrowserWebPage.DTOs;
using BrowserWebPage.Models;
using System.Diagnostics;

namespace BrowserWebPage.BusinessLogic
{
    public class CarTemplateControl : ICarTemplateControl
    {
        private readonly DbCarTemplate _dbCarTemplateAccess;

        public CarTemplateControl(DbCarTemplate dbCarTemplateAccess)
        {
            _dbCarTemplateAccess = dbCarTemplateAccess;
        }

        public List<CarTemplate> GetAllCarTemplates()
        {
            List<CarTemplate> allCarTemplates = new List<CarTemplate>();
            try
            {
                allCarTemplates = _dbCarTemplateAccess.GetAllEntities();
            }
            catch (Exception ex)
            {
                allCarTemplates = null;
                Debug.WriteLine(ex.Message);
            }
            return allCarTemplates;
        }

        public CarTemplate GetCarTemplateByID(int ID)
        {
            CarTemplate carTemplatePlaceholder = null;
            try
            {
                carTemplatePlaceholder = _dbCarTemplateAccess.GetByIdentifier(ID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return carTemplatePlaceholder;
        }

        public bool AddCarTemplate(CarTemplate newCarTemplate)
        {
            bool carTemplateExists = false;
            bool wasCarTemplateInserted = false;
            int numberOfRowsInserted;
            try
            {
                carTemplateExists = _dbCarTemplateAccess.CarTemplateExists(newCarTemplate.ID);
                if (carTemplateExists) // CASE: CarTemplate does exist in DB --> Cannot be created
                {
                    throw new InvalidOperationException($"A CarTemplate with the ID '{newCarTemplate.ID}' already exists in the CarTemplate table.");
                }

                // Customer does not exist
                numberOfRowsInserted = _dbCarTemplateAccess.CreateEntity(newCarTemplate);
                wasCarTemplateInserted = (numberOfRowsInserted == 1);
            }
            catch (Exception ex)
            {
                newCarTemplate = null;
                Debug.WriteLine(ex.Message);
            }
            return wasCarTemplateInserted;
        }

        public bool UpdateCarTemplate(CarTemplate updateCarTemplate)
        {
            bool carTemplateExists = false;
            bool wasCarTemplateUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                carTemplateExists = _dbCarTemplateAccess.CarTemplateExists(updateCarTemplate.ID);
                if (!carTemplateExists) // CASE: Car does not exist in DB --> Cannot be updated
                {
                    throw new InvalidOperationException($"A CarTemplate with the ID '{updateCarTemplate.ID}' does not exist in the CarTemplate table.");
                }

                // CarModel does exist
                numberOfRowsUpdated = _dbCarTemplateAccess.UpdateEntity(updateCarTemplate);
                wasCarTemplateUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasCarTemplateUpdated;
        }

        public bool DeleteCarTemplate(int ID)
        {
            bool carTemplateExists;
            bool wasCarTemplateDeleted = false;
            try
            {
                carTemplateExists = _dbCarTemplateAccess.CarTemplateExists(ID);
                if (!carTemplateExists)
                {
                    throw new InvalidOperationException($"A Car with the ID '{ID}' does not exist in the Car table.");
                }

                // CASE: CarTemplate does exist in DB
                wasCarTemplateDeleted = _dbCarTemplateAccess.DeleteEntity(ID);
            }
            catch (Exception ex)
            {
                wasCarTemplateDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasCarTemplateDeleted;
        }
    }
}
