using Common.Infrastructure;
using Common.Models;
using MediatR;

namespace AccountService.MediatR;

public class CreateAtmRequest: IRequest<ApiResult>
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public string CardNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CardHolderName { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
}