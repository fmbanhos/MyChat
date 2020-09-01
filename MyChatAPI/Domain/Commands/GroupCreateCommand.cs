using AutoMapper;
using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Commands
{
	public class GroupCreateCommandRequest : IRequest<GroupCreateCommandResponse>
	{
		[Required]
		public string Name { get; set; }
	}

	public class GroupCreateCommandResponse : FundamentalCreateCommandResponse<GroupEntity> { }

	public class GroupCreateCommandHandler : IRequestHandler<GroupCreateCommandRequest, GroupCreateCommandResponse>
	{
		private readonly IGroupRepository repository;
		private readonly IMapper mapper;

		public GroupCreateCommandHandler(IGroupRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task<GroupCreateCommandResponse> Handle(GroupCreateCommandRequest request, CancellationToken cancellationToken)
		{
			GroupCreateCommandResponse result = new GroupCreateCommandResponse();			
			GroupEntity group = await repository.Get(request.Name);
			if (group == null)
			{
				group = mapper.Map<GroupEntity>(request);
				await repository.Create(group);
				result.Entity = await repository.Get(request.Name);
				result.Code = 1;
				result.Message = "Group created";
			}
			else
			{
				result.Code = 0;
				result.Message = "Group already exists";
			}
			return result;
		}
	}
}
