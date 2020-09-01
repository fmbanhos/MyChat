using MediatR;
using MyChatAPI.Domain.Entities;
using MyChatAPI.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Commands
{
	public class PersonDeleteCommandRequest : IRequest<PersonDeleteCommandResponse> 
	{
		[Required]
		public int Id { get; set; }
	}

	public class PersonDeleteCommandResponse : FundamentalCommandResponse { }

	public class PersonDeleteCommandHanlder : IRequestHandler<PersonDeleteCommandRequest, PersonDeleteCommandResponse>
	{
		private readonly IPersonRepository personRepository;
		private readonly IMessageRepository messageRepository;

		public PersonDeleteCommandHanlder(IPersonRepository personRepository, IMessageRepository messageRepository)
		{
			this.personRepository = personRepository;
			this.messageRepository = messageRepository;
		}

		public async Task<PersonDeleteCommandResponse> Handle(PersonDeleteCommandRequest request, CancellationToken cancellationToken)
		{
			PersonDeleteCommandResponse result = new PersonDeleteCommandResponse();
			PersonEntity person = await personRepository.GetById(request.Id);
			if (person == null)
			{
				result.Code = 0;
				result.Message = "Person not found";
			}
			else
			{
				await messageRepository.Delete(person);
				await personRepository.Delete(person);
				result.Code = 1;
				result.Message = "Person deleted";
			}
			return result;
		}
	}
}
