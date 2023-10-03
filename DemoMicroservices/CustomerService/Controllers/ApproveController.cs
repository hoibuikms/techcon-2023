using System;
using System.Threading.Tasks;
using ConsumerAPI.MediatR;
using Dapr;
using Dapr.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckoutService.controller;

[ApiController]
[Route("[controller]")]
public class ApproveController : ControllerBase
{
    private readonly ILogger<ApproveController> _logger;
    private const string DAPR_PUBSUB_NAME = "order-pub-sub";
    private const string TOPIC_NAME = "atm_created";
      
    private readonly DaprClient _daprClient;
    private readonly IMediator _mediator;
        
    public ApproveController(ILogger<ApproveController> logger, DaprClient daprClient,  IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Approve(UpdateRequest request)
    {
        return await _mediator.Send(request);
    }
}