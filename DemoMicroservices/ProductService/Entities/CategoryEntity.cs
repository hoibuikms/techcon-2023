using Common.Entites;
using Common.Models;

namespace ProductService.Entities;

public class CategoryEntity : IBaseEntity<Guid>, ICreatedEntity, IUpdatedEntity
{
    public ProducType Type { get; set; }
    public string Description { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Guid Id { get; set; }
    
}