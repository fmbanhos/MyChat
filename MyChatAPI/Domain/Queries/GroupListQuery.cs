using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Queries
{
	public class GroupListQueryRequest : IRequest<GroupListQueryResponse> { }

	public class GroupListQueryResponse : FundamentalListQueryResponse<GroupEntity> { } 

	public class GroupListQueryHandler : IRequestHandler<GroupListQueryRequest, GroupListQueryResponse>
	{
		private readonly IGroupRepository repository;

		public GroupListQueryHandler(IGroupRepository repository)
		{
			this.repository = repository;
		}

		public async Task<GroupListQueryResponse> Handle(GroupListQueryRequest request, CancellationToken cancellationToken)
		{
			GroupListQueryResponse result = new GroupListQueryResponse();
			result.List = await repository.GetList();
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
