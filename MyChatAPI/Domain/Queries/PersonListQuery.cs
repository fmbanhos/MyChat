using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Queries
{
	public class PersonListQueryRequest : IRequest<PersonListQueryResponse>
	{
		[Required]
		public int IdGroup { get; set; }
	}

	public class PersonListQueryResponse : FundamentalListQueryResponse<PersonEntity> { }

	public class PersonListQueryHandler : IRequestHandler<PersonListQueryRequest, PersonListQueryResponse>
	{
		private readonly IPersonRepository repository;

		public PersonListQueryHandler(IPersonRepository repository)
		{
			this.repository = repository;
		}

		public async Task<PersonListQueryResponse> Handle(PersonListQueryRequest request, CancellationToken cancellationToken)
		{			
			PersonListQueryResponse result = new PersonListQueryResponse();
			result.List = await repository.GetList(request.IdGroup);
			int? count = result.List?.Count;
			if (count > 0)
			{
				result.Code = 1;
				result.Message = $"{count} records found";
			}
			else
			{
				result.Code = 0;
				result.Message = $"No records found";
			}
			return result;
		}
	}
}
