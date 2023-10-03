using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands;
using Common.Infrastructure;
using ConsumerAPI.Entities;
using ConsumerAPI.Infrastructure;
using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConsumerAPI.MediatR;

public class UpdateHandler: IRequestHandler<UpdateRequest, ApiResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly DaprClient _daprClient;
    private string PUBSUB_NAME = "order-pub-sub";
    private string TOPIC_NAME = "send-email";
    
    public UpdateHandler(IUnitOfWork<ApplicationContext> unitOfWork,DaprClient daprClient,  ILogger<UpdateHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _daprClient = daprClient;
    }

    public async Task<ApiResult> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _unitOfWork.GetRepository<RequestEntity>().SingleOrDefaultAsync(
                predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);

            item.IsApproved = true;
            _unitOfWork.GetRepository<RequestEntity>().Update(item);
            await _unitOfWork.CommitAsync();

            var message = new Order()
            {
                OrderId = item.Id,
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