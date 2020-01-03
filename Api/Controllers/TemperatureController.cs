using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain;
using MediatR;
using Application.Temperatures;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private readonly ILogger<TemperatureController> _logger;

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public TemperatureController(ILogger<TemperatureController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<object>>> Get()
        {
            return await Mediator.Send(new RetrieveList.Query { });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Temperature>> Get(int id)
        {
            return await Mediator.Send(new RetrieveSingle.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Put(Update.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(Delete.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
