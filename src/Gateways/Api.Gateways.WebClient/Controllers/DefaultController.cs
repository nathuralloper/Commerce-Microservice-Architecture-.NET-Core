using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateways.WebClient.Controllers
{
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        [HttpGet]
        public string Index()
        {
            return "Running...";
        }

    }
}