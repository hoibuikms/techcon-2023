using AccountService.Entities;
using AccountService.Infrastructure;
using Common.Commands;
using Common.Infrastructure;
using Dapr.Client;
using MediatR;

namespace AccountService.MediatR;

public class CreateAtmHandler: IRequestHandler<CreateAtmRequest, ApiResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly DaprClient _daprClient;
    private string PUBSUB_NAME = "order-pub-sub";
    private string TOPIC_NAME = "atm_created";
    
    public CreateAtmHandler(IUnitOfWork<ApplicationContext> unitOfWork,DaprClient daprClient,  ILogger<CreateAtmHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _daprClient = daprClient;
    }

    public async Task<ApiResult> Handle(CreateAtmRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var model = new ATMEntity()
            {
                Id = Guid.NewGuid(),
                CardNumber = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.UtcNow.AddYears(3),
                Description = request.Description,
                CardHolderName = "Hoi Bui",
                UpdatedBy = "",
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Hoi Bui",
                OrderId = request.OrderId
            };

            await _unitOfWork.GetRepository<ATMEntity>().InsertAsync(model, cancellationToken);
            await _unitOfWork.CommitAsync();

            var message = new Order()
            {
                OrderId = model.Id
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