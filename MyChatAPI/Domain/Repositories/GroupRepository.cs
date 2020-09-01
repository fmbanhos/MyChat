using MyChatAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Repositories
{
	public interface IGroupRepository : IFundamentalRepository<GroupEntity>
	{
		Task<GroupEntity> Get(string name);
		Task<IList<GroupEntity>> GetList();
		Task Delete(GroupEntity group);
	}

	public class GroupRepository : FundamentalRepository<GroupEntity>, IGroupRepository
	{
		public async Task Delete(GroupEntity group)
		{
			await Task.Run(() => Database.Remove(group));
		}

		public async Task<GroupEntity> Get(string name)
		{
			GroupEntity result = Database.Where(a => a.Name == name).FirstOrDefault();
			return await Task.FromResult(result);
		}

		public async Task<IList<GroupEntity>> GetList()
		{
			IList<GroupEntity> result = Database.Take(100).ToList();
			return await Task.FromResult(result);
		}
	}
}
