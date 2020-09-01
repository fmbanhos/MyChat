using AutoMapper;
using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Commands
{
	public class PersonCreateCommandRequest : IRequest<PersonCreateCommandResponse>
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public int IdGroup { get; set; }
	}

	public class PersonCreateCommandResponse : FundamentalCreateCommandResponse<PersonEntity> { }

	public class PersonCreateCommandHanlder : IRequestHandler<PersonCreateCommandRequest, PersonCreateCommandResponse>
	{
		private readonly IPersonRepository repository;
		private readonly IMapper mapper;

		public PersonCreateCommandHanlder(IPersonRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task<PersonCreateCommandResponse> Handle(PersonCreateCommandRequest request, CancellationToken cancellationToken)
		{
			PersonCreateCommandResponse result = new PersonCreateCommandResponse();		
			PersonEntity person = await repository.Get(request.Name, request.IdGroup);
			if (person == null)
			{
				person = mapper.Map<PersonEntity>(request);
				await repository.Create(person);
				result.Entity = await repository.Get(request.Name, request.IdGroup);
				result.Code = 1;
				result.Message = "Person created";
			}
			else
			{
				result.Code = 0;
				result.Message = "Person already exists";
			}
			return result;
		}
	}
}
