using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Repositories
{
	public interface IFundamentalRepository<T>
	{
		Task Create(T entity);
		Task<T> GetById(int id);
	}

	public abstract class FundamentalRepository<T> : IFundamentalRepository<T>
	{
		public IList<T> Database { get; }

		public FundamentalRepository()
		{
			Database = new List<T>();
		}

		#region Implementando interface
		public virtual async Task Create(T entity)
		{
			int count = Database.Count;
			(entity as dynamic).Id = count + 1;
			await Task.Run(() => Database.Add(entity));
		}

		public async Task<T> GetById(int id)
		{
			T result = Database.Cast<dynamic>().Where(a => a.Id == id).Cast<T>().FirstOrDefault();
			return await Task.FromResult(result);
		}
		#endregion
	}
}
