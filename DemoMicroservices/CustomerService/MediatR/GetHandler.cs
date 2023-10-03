using System.Threading;
using System.Threading.Tasks;
using Common.Infrastructure;
using ConsumerAPI.Entities;
using ConsumerAPI.Infrastructure;
using Dapr.Client;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConsumerAPI.MediatR;

public class GetHandler: IRequestHandler<GetRequest, ApiResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly DaprClient _daprClient;
    
    public GetHandler(IUnitOfWork<ApplicationContext> unitOfWork,DaprClient daprClient,  ILogger<CreateRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _daprClient = daprClient;
    }

    public async Task<ApiResult> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.GetRepository<RequestEntity>().GetListAsync();
        return ApiResult.Succeeded(result);
    }
}