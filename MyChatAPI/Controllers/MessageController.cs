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
	public class MessageController : FundamentalController
	{
		public MessageController(IMediator mediator) : base(mediator) { }

		// GET: api/<controller>
		[HttpGet]
		public async Task<MessageListQueryResponse> GetList([FromQuery] MessageListQueryRequest request)
		{
			MessageListQueryResponse result = await mediator.Send(request);
			return result;
		}

		// POST api/<controller>
		[HttpPost]
		public async Task<MessageCreateCommandResponse> Post(MessageCreateCommandRequest request)
		{
			MessageCreateCommandResponse result = await mediator.Send(request);
			return result;
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public async Task<MessageDeleteCommandResponse> Delete(MessageDeleteCommandRequest request)
		{
			MessageDeleteCommandResponse result = await mediator.Send(request);
			return result;
		}
	}
}
