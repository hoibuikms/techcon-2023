using Dapr.Client;
using Common.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using CounterService.MediatR;
using Dapr;
using MediatR;

namespace CounterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        private const string PUBSUB_NAME = "order-pub-sub";
        private const string TOPIC_NAME = "orders";
        private const string ATM_TOPIC_NAME = "atm_created";

        private readonly ILogger<OrderController> _logger;
        private readonly DaprClient _daprClient;

        public OrderController(ILogger<OrderController> logger, DaprClient daprClient, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
            _mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("Get Order API");
            return new List<string>();
        }

        [HttpPost]
        public async Task<IActionResult> OrderProduct(OrderRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}