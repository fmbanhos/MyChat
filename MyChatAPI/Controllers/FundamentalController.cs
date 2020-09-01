using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyChatAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public abstract class FundamentalController : ControllerBase
	{
		protected readonly IMediator mediator;

		public FundamentalController(IMediator mediator)
		{
			this.mediator = mediator;
		}
	}
}
