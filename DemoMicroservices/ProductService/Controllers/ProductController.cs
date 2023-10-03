using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Queries;

namespace ProductService.Controllers;

public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpGet("categories")]
    public async Task<IActionResult> GetListAsync([FromQuery] GetCategoryRequest request)
    {
        return await _mediator.Send(request);
    }
}