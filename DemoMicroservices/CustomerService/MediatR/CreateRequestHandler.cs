using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands;
using Common.Models;
using Common.Infrastructure;
using ConsumerAPI.Entities;
using ConsumerAPI.Infrastructure;
using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConsumerAPI.MediatR;

public class CreateRequestHandler: IRequestHandler<CreateRequest, ApiResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly DaprClient _daprClient;
    
    public CreateRequestHandler(IUnitOfWork<ApplicationContext> unitOfWork,DaprClient daprClient,  ILogger<CreateRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _daprClient = daprClient;
    }

    public  async Task<ApiResult> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var order = new RequestEntity()
            {
                Id = Guid.NewGuid(),
                ProductType = request.ProductType,
                ServiceType = request.ServiceType,
                Description = "create atm card",
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Hoi Bui",
                IsApproved = false
            };

            await _unitOfWork.GetRepository<RequestEntity>().InsertAsync(order, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ApiResult.Succeeded();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ApiResult.Failed(HttpCode.InternalServerError);
        }
    }
}