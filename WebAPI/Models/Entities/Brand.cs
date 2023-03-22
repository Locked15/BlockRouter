using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlockRouter.WebAPI.Models.Entities;

[Serializable]
public partial class Brand
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [Required]
    public bool IsActive { get; set; }

    [JsonIgnore]
    public virtual ICollection<Model> Models { get; } = new List<Model>();
}
