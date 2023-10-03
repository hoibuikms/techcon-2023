using Common.Infrastructure;
using Common.Models;
using MediatR;

namespace ProductService.Queries;

public class GetCategoryRequest: IRequest<ApiResult>
{
    public string Description { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Guid Id { get; set; }
}