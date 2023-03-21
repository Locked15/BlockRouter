namespace BlockRouter.WebAPI.Models.Entities;

public partial class Model
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual Brand Brand { get; set; } = null!;
}
