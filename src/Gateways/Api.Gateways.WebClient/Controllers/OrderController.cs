using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateways.Models;
using Api.Gateways.Models.Order.DTOs;
using Api.Gateways.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateways.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderProxy _orderProxy;
        private readonly ICustomerProxy _customerProxy;
        private readonly ICatalogProxy _catalogProxy;

        public OrderController(IOrderProxy orderProxy,
            ICustomerProxy customerProxy,
            ICatalogProxy catalogProxy)
        {
            _catalogProxy = catalogProxy;
            _orderProxy = orderProxy;
            _customerProxy = customerProxy;
        }

        

    }
}