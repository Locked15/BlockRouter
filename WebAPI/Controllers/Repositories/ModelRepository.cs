using BlockRouter.WebAPI.Models.Entities;
using WebAPI.Controllers.Repositories.Base;

namespace WebAPI.Controllers.Repositories
{
    public class ModelRepository : IModelRepository
    {
        public Task<Model> Create(Model model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Model> Read(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Model>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<Model> ReadByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Model> Update(Model brand)
        {
            throw new NotImplementedException();
        }
    }
}
