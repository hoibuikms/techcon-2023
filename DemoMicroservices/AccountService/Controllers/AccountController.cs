using System;
using System.Threading.Tasks;
using AccountService.Entities;
using AccountService.MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapr;
using Dapr.Client;
using Common.Commands;
using MediatR;

namespace AccountService.Controllers;

[ApiController]
public class AccountController: ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private const string DAPR_PUBSUB_NAME = "order-pub-sub";
    private const string TOPIC_NAME = "orders";
    private readonly DaprClient _daprClient;
    
    private readonly IMediator _mediator;
    
    public AccountController(ILogger<AccountController> logger, DaprClient daprClient,  IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
        _mediator = mediator;
    }

    //Subscribe to a topic
    [Topic(DAPR_PUBSUB_NAME, TOPIC_NAME)]
    [HttpPost("account")]
    public async Task CreateAtm([FromBody] Common.Commands.Order order)
    {
        Console.WriteLine("Subscriber received : " + order.OrderId);
        _logger.LogInformation($"Received checkout:  {order.OrderId}");

        var request = new CreateAtmRequest()
        {
            Description = "create new",
            UserId = Guid.NewGuid(),
            OrderId = order.OrderId
        };
        
        await _mediator.Send(request);

        _logger.LogInformation(
            "GetDon Send a message with Order ID {orderId}, {orderNumber}",
            order.OrderId, order.Status);
    }
}