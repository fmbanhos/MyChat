using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Queries
{
	public class MessageListQueryRequest : IRequest<MessageListQueryResponse>
	{
		[Required]
		public int IdGroup { get; set; }
		[Required]
		public int IdDestinationPerson { get; set; }
		[Required]
		public DateTime Date { get; set; }
	}

	public class MessageListQueryResponse : FundamentalListQueryResponse<MessageEntity> { }

	public class MessageListQueryHandler : IRequestHandler<MessageListQueryRequest, MessageListQueryResponse>
	{
		private readonly IMessageRepository repository;

		public MessageListQueryHandler(IMessageRepository repository)
		{
			this.repository = repository;
		}

		public async Task<MessageListQueryResponse> Handle(MessageListQueryRequest request, CancellationToken cancellationToken)
		{
			MessageListQueryResponse result = new MessageListQueryResponse();
			result.List = await repository.GetList(request.IdGroup, request.IdDestinationPerson, request.Date);
			int? count = result.List?.Count;
			if (count > 0)
			{
				result.Code = 1;
				result.Message = $"{count} records found";
			}
			else
			{
				result.Code = 0;
				result.Message = "No records found";
			}
			return result;
		}
	}
}
