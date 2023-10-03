using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapr;
using Dapr.Client;
using Common.Commands;
using Common.Models;
using ConsumerAPI.MediatR;
using MediatR;
using System.Web.Http.Cors;

namespace CheckoutService.controller
{
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CheckoutServiceController : ControllerBase
    {
        private readonly ILogger<CheckoutServiceController> _logger;
        private const string DAPR_PUBSUB_NAME = "order-pub-sub";
        private const string TOPIC_NAME = "atm_created";
      
        private readonly DaprClient _daprClient;
        private readonly IMediator _mediator;
        
        public CheckoutServiceController(ILogger<CheckoutServiceController> logger, DaprClient daprClient,  IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
            _mediator = mediator;
        }

        //Subscribe to a topic
        [Topic(DAPR_PUBSUB_NAME, TOPIC_NAME)]
        [HttpPost("review")]
        public async Task Checkout([FromBody] Common.Commands.Order order)
        {
            Console.WriteLine($"Subscriber received : {order.OrderId} - Need to review");
            
            var request = new CreateRequest()
            {
                ProductType = ProducType.ATM,
                ServiceType = ProductServices.CREATE
            };

            
            await _mediator.Send(request);
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get([FromQuery] GetRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}