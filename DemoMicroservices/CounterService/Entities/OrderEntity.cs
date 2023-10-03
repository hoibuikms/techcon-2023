using System;
using Common.Entites;
using Common.Infrastructure;
using Common.Models;

namespace CounterService.Entities;

public class OrderEntity: IBaseEntity<Guid>, ICreatedEntity, IUpdatedEntity
{
    public ProducType ProductTypeType { get; set; }
    public ProductServices ServiceType { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Guid Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
}