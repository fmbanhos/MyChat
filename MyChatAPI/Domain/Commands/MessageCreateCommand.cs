using AutoMapper;
using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Commands
{
	public class MessageCreateCommandRequest : IRequest<MessageCreateCommandResponse>
	{
		[Required]
		public int IdGroup { get; set; }
		[Required]
		public int IdOriginPerson { get; set; }
		[Required]
		public int IdDestinationPerson { get; set; }
		[Required]
		public string Content { get; set; }
		[Required]
		public DateTime Date { get; set; }
	}

	public class MessageCreateCommandResponse : FundamentalCreateCommandResponse<MessageEntity> { }

	public class MessageCreateCommandHanlder : IRequestHandler<MessageCreateCommandRequest, MessageCreateCommandResponse>
	{
		private readonly IMessageRepository repository;
		private readonly IMapper mapper;

		public MessageCreateCommandHanlder(IMessageRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task<MessageCreateCommandResponse> Handle(MessageCreateCommandRequest request, CancellationToken cancellationToken)
		{
			MessageCreateCommandResponse result = new MessageCreateCommandResponse();
			MessageEntity message = await repository.Get(request.IdGroup, request.IdOriginPerson, request.IdDestinationPerson, request.Date);
			if (message == null)
			{
				message = mapper.Map<MessageEntity>(request);
				await repository.Create(message);
				result.Entity = await repository.Get(request.IdGroup, request.IdOriginPerson, request.IdDestinationPerson, request.Date);
				result.Code = 1;
				result.Message = "Message created";
			}
			else
			{
				result.Code = 0;
				result.Message = "Message already exists";
			}			
			return result;
		}
	}
}
