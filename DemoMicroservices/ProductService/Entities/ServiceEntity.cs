using Common.Entites;
using Common.Models;

namespace ProductService.Entities;

public class ServiceEntity: IBaseEntity<Guid>, ICreatedEntity, IUpdatedEntity
{
    public ProductServices ServiceType { get; set; }
    public string ServiceName { get; set; }

    public string Description { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Guid Id { get; set; }
    
    public Guid CategoryId { get; set; }
    public virtual ICollection<CategoryEntity> Categories { get; }
}