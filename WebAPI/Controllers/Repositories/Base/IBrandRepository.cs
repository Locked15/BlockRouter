using BlockRouter.WebAPI.Models.Entities;

namespace BlockRouter.WebAPI.Controllers.Repositories.Base
{
    public interface IBrandRepository
    {
        Task<bool> Create(Brand brand);

        Task<Brand?> Read(int id);

        Task<Brand?> Read(string name);

        Task<List<Brand>> ReadAll();

        Task<bool> Update(Brand brand);

        Task<bool> Delete(int id);
    }
}
