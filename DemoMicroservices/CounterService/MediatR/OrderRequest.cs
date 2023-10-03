using System;
using Common.Infrastructure;
using Common.Models;
using MediatR;

namespace CounterService.MediatR;

public class OrderRequest: IRequest<ApiResult>
{
    public ProducType ProductType { get; set; }
    public ProductServices ServiceType { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Guid Id { get; set; }
}