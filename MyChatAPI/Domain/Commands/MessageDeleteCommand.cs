using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Commands
{
	public class MessageDeleteCommandRequest : IRequest<MessageDeleteCommandResponse>
	{
		[Required]
		public int Id { get; set; }
	}

	public class MessageDeleteCommandResponse : FundamentalCommandResponse { }

	public class MessageDeleteCommandHanlder : IRequestHandler<MessageDeleteCommandRequest, MessageDeleteCommandResponse>
	{
		private readonly IMessageRepository repository;

		public MessageDeleteCommandHanlder(IMessageRepository repository)
		{
			this.repository = repository;
		}

		public async Task<MessageDeleteCommandResponse> Handle(MessageDeleteCommandRequest request, CancellationToken cancellationToken)
		{
			MessageDeleteCommandResponse result = new MessageDeleteCommandResponse();
			MessageEntity message = await repository.GetById(request.Id);
			if (message == null)
			{
				result.Code = 0;
				result.Message = "Message not found";
			}
			else
			{
				await repository.Delete(message);
				result.Code = 1;
				result.Message = "Message deleted";
			}
			return result;
		}
	}
}
