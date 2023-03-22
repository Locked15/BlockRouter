using BlockRouter.WebAPI.Controllers.Repositories.Base;
using BlockRouter.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.ComponentModel.DataAnnotations;

namespace BlockRouter.WebAPI.Controllers.Repositories
{
    public class ModelRepository : IRepository<Model>
    {
        private readonly PostgresDataContext _dataContext;

        public ModelRepository(PostgresDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region Actions

        public async Task<bool> Create(Model model)
        {
            if (ValidateModel(model, new(1)))
            {
                _dataContext.Models.Add(model);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            else
            {
                Log.Error("On 'Create' new 'Model' error happened:\nModel isn't valid.");
                return false;
            }
        }

        public async Task<Model?> Read(int id)
        {
            return await _dataContext.Models.FirstOrDefaultAsync(model =>
                                                                 model.Id == id);
        }

        public async Task<Model?> Read(string name)
        {
            return await _dataContext.Models.FirstOrDefaultAsync(model =>
                                                                 model.Name == name);
        }

        public async Task<List<Model>> ReadAll()
        {
            return await _dataContext.Models.ToListAsync();
        }

        public async Task<bool> Update(Model model)
        {
            if (ValidateModel(model, new(1)))
            {
                _dataContext.Models.Update(model);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            else
            {
                Log.Error("On 'Update' new 'Model' error happened:\nModel isn't valid.");
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                _dataContext.Models.Remove(new Model() { Id = id });
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

        private static bool ValidateModel(Model model, List<ValidationResult> results)
        {
            var validationContext = new ValidationContext(model);
            return Validator.TryValidateObject(model, validationContext, results, true);
        }
        #endregion
    }
}
