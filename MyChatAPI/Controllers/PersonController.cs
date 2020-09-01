using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyChatAPI.Domain.Commands;
using MyChatAPI.Domain.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyChatAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PersonController : FundamentalController
	{
		public PersonController(IMediator mediator) : base(mediator) { }

		// GET: api/<controller>
		[HttpGet]
		public async Task<PersonListQueryResponse> GetList([FromQuery] PersonListQueryRequest request)
		{			
			PersonListQueryResponse result = await mediator.Send(request);
			return result;
		}

		// POST api/<controller>
		[HttpPost]
		public async Task<PersonCreateCommandResponse> Post(PersonCreateCommandRequest request)
		{
			PersonCreateCommandResponse result = await mediator.Send(request);
			return result;
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public async Task<PersonDeleteCommandResponse> Delete(PersonDeleteCommandRequest request)
		{
			PersonDeleteCommandResponse result = await mediator.Send(request);
			return result;
		}
	}
}
