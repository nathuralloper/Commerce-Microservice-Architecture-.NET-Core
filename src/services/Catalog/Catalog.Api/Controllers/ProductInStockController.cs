using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Queries.Service;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Common.Collection;

namespace Catalog.Api.Controllers
{
    [Route("v1/stocks")]
    [ApiController]
    public class ProductInStockController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;       
        private readonly IMediator _mediator;

        public ProductInStockController(ILogger<ProductController> logger, 
                                 IProductQueryService productQueryService,
                                 IMediator mediator)
        {
            _logger = logger;            
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock(ProductInStockUpdateStockCommand command)
        {
            await _mediator.Publish(command);
            return NoContent();
        }

    }

}