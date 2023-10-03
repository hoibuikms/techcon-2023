using Common.Infrastructure;
using MediatR;
using ProductService.Entities;
using ProductService.Infrastructure;

namespace ProductService.Queries;

public class GetCategoryHandler : IRequestHandler<GetCategoryRequest, ApiResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public GetCategoryHandler(IUnitOfWork<ApplicationContext> unitOfWork,
        ILogger<GetCategoryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ApiResult> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _unitOfWork.GetRepository<CategoryEntity>().GetListAsync(
                cancellationToken: cancellationToken);
            return ApiResult.Succeeded(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ApiResult.Failed(HttpCode.InternalServerError);
        }
    }
}