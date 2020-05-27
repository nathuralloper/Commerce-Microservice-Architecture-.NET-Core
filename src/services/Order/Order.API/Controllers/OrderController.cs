using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Service.EventHandlers.Commands;
using Order.Service.Queries;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Order.API.Controllers
{
    [Route("v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderQueryService _orderQueryService;
        private readonly IMediator _mediator;

        public OrderController(IOrderQueryService orderQueryService, IMediator mediator)
        {
            _orderQueryService = orderQueryService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page=1, int take=10, string orders = "")
        {
            IEnumerable<int> order = null;
            if (!string.IsNullOrEmpty(orders))
            {
                order = orders.Split(',').Select(x => Convert.ToInt32(x));
            }
            return await _orderQueryService.GetAllAsync(page, take, order);
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            return await _orderQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }

    }
}