using BlockRouter.WebAPI.Models.Entities;

namespace WebAPI.Controllers.Repositories.Base
{
    public interface IModelRepository
    {
        Task<Model> Create(Model model);

        Task<Model> Read(int id);

        Task<Model> ReadByName(string name);

        Task<List<Model>> ReadAll();

        Task<Model> Update(Model brand);

        Task<bool> Delete(int id);
    }
}
