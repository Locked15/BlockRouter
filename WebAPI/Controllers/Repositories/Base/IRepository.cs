namespace BlockRouter.WebAPI.Controllers.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<bool> Create(T element);

        Task<T?> Read(int id);

        Task<T?> Read(string name);

        Task<List<T>> ReadAll();

        Task<bool> Update(T element);

        Task<bool> Delete(int id);
    }
}
