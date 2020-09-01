using MyChatAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Repositories
{
	public interface IPersonRepository : IFundamentalRepository<PersonEntity>
	{
		Task<PersonEntity> Get(string name, int idGroup);
		Task<IList<PersonEntity>> GetList(int idGroup);
		Task Delete(PersonEntity person);
		Task Delete(GroupEntity group);
	}

	public class PersonRepository : FundamentalRepository<PersonEntity>, IPersonRepository
	{
		public async Task Delete(GroupEntity group)
		{
			await Task.Run(() => 
			{
				IList<PersonEntity> personList = Database.Where(a => a.IdGroup == group.Id).ToList();
				if (personList?.Count > 0)
				{
					foreach (PersonEntity person in personList)
					{
						Database.Remove(person);
					}
				}
			});
		}

		public async Task Delete(PersonEntity person)
		{
			await Task.Run(() => Database.Remove(person));
		}

		public async Task<PersonEntity> Get(string name, int idGroup)
		{
			PersonEntity result = Database.Where(a => a.Name == name && a.IdGroup == idGroup).FirstOrDefault();
			return await Task.FromResult(result);
		}

		public async Task<IList<PersonEntity>> GetList(int idGroup)
		{
			IList<PersonEntity> result = Database.Where(a => a.IdGroup == idGroup).Take(100).ToList();
			return await Task.FromResult(result);
		}
	}
}
