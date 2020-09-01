using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Commands
{
	public class GroupDeleteCommandRequest : IRequest<GroupDeleteCommandResponse>
	{
		[Required]
		public int Id { get; set; }
	}

	public class GroupDeleteCommandResponse : FundamentalCommandResponse { }

	public class GroupDeleteCommandHanlder : IRequestHandler<GroupDeleteCommandRequest, GroupDeleteCommandResponse>
	{
		private readonly IGroupRepository groupRepository;
		private readonly IMessageRepository messageRepository;
		private readonly IPersonRepository personRepository;

		public GroupDeleteCommandHanlder(IGroupRepository groupRepository, IMessageRepository messageRepository, IPersonRepository personRepository)
		{
			this.groupRepository = groupRepository;
			this.messageRepository = messageRepository;
			this.personRepository = personRepository;
		}

		public async Task<GroupDeleteCommandResponse> Handle(GroupDeleteCommandRequest request, CancellationToken cancellationToken)
		{
			GroupDeleteCommandResponse result = new GroupDeleteCommandResponse();
			GroupEntity Group = await groupRepository.GetById(request.Id);
			if (Group == null)
			{
				result.Code = 0;
				result.Message = "Group not found";
			}
			else
			{
				await messageRepository.Delete(Group);
				await personRepository.Delete(Group);
				await groupRepository.Delete(Group);
				result.Code = 1;
				result.Message = "Group deleted";
			}
			return result;
		}
	}
}
