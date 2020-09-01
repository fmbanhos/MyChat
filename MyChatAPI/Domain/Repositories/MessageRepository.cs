using MyChatAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatAPI.Domain.Repositories
{
	public interface IMessageRepository : IFundamentalRepository<MessageEntity>
	{
		Task<MessageEntity> Get(int idGroup, int idOriginPerson, int idDestinationPerson, DateTime date);
		Task<IList<MessageEntity>> GetList(int idGroup, int idPerson, DateTime date);
		Task Delete(PersonEntity person);
		Task Delete(GroupEntity group);
		Task Delete(MessageEntity message);
	}

	public class MessageRepository : FundamentalRepository<MessageEntity>, IMessageRepository
	{
		public async Task Delete(PersonEntity person)
		{
			await Task.Run(() => 
			{
				IList<MessageEntity> messageList = Database.Where(a =>
															a.IdOriginPerson == person.Id ||
															a.IdDestinationPerson == person.Id)
														.ToList();
				if (messageList?.Count > 0)
				{
					foreach (MessageEntity message in messageList)
					{
						Database.Remove(message);
					}
				}
			});
		}

		public async Task Delete(GroupEntity group)
		{
			await Task.Run(() =>
			{
				IList<MessageEntity> messageList = Database.Where(a => a.IdGroup == group.Id).ToList();
				if (messageList?.Count > 0)
				{
					foreach (MessageEntity message in messageList)
					{
						Database.Remove(message);
					}
				}
			});
		}

		public async Task Delete(MessageEntity message)
		{
			await Task.Run(() => Database.Remove(message));
		}

		public async Task<MessageEntity> Get(int idGroup, int idOriginPerson, int idDestinationPerson, DateTime date)
		{
			MessageEntity result = Database.Where(a =>
												a.IdGroup == idGroup &&
												a.IdOriginPerson == idOriginPerson &&
												a.IdDestinationPerson == idDestinationPerson &&
												a.Date == date)
										.FirstOrDefault();
			return await Task.FromResult(result);
		}

		public async Task<IList<MessageEntity>> GetList(int idGroup, int idDestinationPerson, DateTime date)
		{
			IList<MessageEntity> result = Database.Where(a => 
													a.IdGroup == idGroup &&
													a.IdDestinationPerson == idDestinationPerson &&
													a.Date > date)
												.Take(100)
												.ToList();
			return await Task.FromResult(result);
		}
	}
}
