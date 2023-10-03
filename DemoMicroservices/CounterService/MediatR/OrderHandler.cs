using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands;
using Common.Infrastructure;
using CounterService.Entities;
using CounterService.Infrastructure;
using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CounterService.MediatR;

public class OrderHandler : IRequestHandler<OrderRequest, ApiResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly DaprClient _daprClient;
    private string PUBSUB_NAME = "order-pub-sub";
    private string TOPIC_NAME = "orders";
    
    public OrderHandler(IUnitOfWork<ApplicationContext> unitOfWork,DaprClient daprClient,  ILogger<OrderHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _daprClient = daprClient;
    }

    public async Task<ApiResult> Handle(OrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var order = new OrderEntity()
            {
                Id = Guid.NewGuid(),
                ProductTypeType = request.ProductType,
                ServiceType = request.ServiceType,
                Description = request.Description,
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            };

            await _unitOfWork.GetRepository<OrderEntity>().InsertAsync(order, cancellationToken);
            await _unitOfWork.CommitAsync();

            var message = new Order()
            {
                OrderId = order.Id,
            };
            await _daprClient.PublishEventAsync(PUBSUB_NAME, TOPIC_NAME, message);

            _logger.LogInformation($"Send a message with Order ID {message.OrderId}");

            return ApiResult.Succeeded();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ApiResult.Failed(HttpCode.InternalServerError);
        }
    }
}