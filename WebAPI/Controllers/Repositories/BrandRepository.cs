using BlockRouter.WebAPI.Controllers.Repositories.Base;
using BlockRouter.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly PostgresDataContext _dataContext;

        public BrandRepository(PostgresDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region Actions

        public async Task<bool> Create(Brand brand)
        {
            if (ValidateModel(brand, new(1)))
            {
                _dataContext.Brands.Add(brand);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            else
            {
                Log.Error("On 'Create' new 'Brand' error happened:\nModel isn't valid.");
                return false;
            }
        }

        public async Task<Brand?> Read(int id)
        {
            return await _dataContext.Brands.FirstOrDefaultAsync(brand =>
                                                                 brand.Id == id);
        }

        public async Task<Brand?> Read(string name)
        {
            return await _dataContext.Brands.FirstOrDefaultAsync(brand =>
                                                                 brand.Name == name);
        }

        public async Task<List<Brand>> ReadAll()
        {
            return await _dataContext.Brands.ToListAsync();
        }

        public async Task<bool> Update(Brand brand)
        {
            if (ValidateModel(brand, new(1)))
            {
                _dataContext.Brands.Update(brand);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            else
            {
                Log.Error("On 'Create' new 'Brand' error happened:\nModel isn't valid.");
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                _dataContext.Brands.Remove(new Brand() { Id = id });
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Internal Functions

        private static bool ValidateModel(Brand brand, List<ValidationResult> results)
        {
            var validationContext = new ValidationContext(brand);
            return Validator.TryValidateObject(brand, validationContext, results, true);
        }
        #endregion
    }
}
