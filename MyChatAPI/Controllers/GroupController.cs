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
	public class GroupController : FundamentalController
	{
		public GroupController(IMediator mediator) : base(mediator) { }

		// GET: api/<controller>
		[HttpGet]
		public async Task<GroupListQueryResponse> GetList()
		{
			GroupListQueryRequest request = new GroupListQueryRequest();
			GroupListQueryResponse result = await mediator.Send(request);
			return result;
		}

		// POST api/<controller>
		[HttpPost]
		public async Task<GroupCreateCommandResponse> Post(GroupCreateCommandRequest request)
		{
			GroupCreateCommandResponse result = await mediator.Send(request);
			return result;
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public async Task<GroupDeleteCommandResponse> Delete(GroupDeleteCommandRequest request)
		{
			GroupDeleteCommandResponse result = await mediator.Send(request);
			return result;
		}
	}
}
