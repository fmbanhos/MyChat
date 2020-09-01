using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Domain.Entities
{
	public abstract class FundamentalEntity
	{
		[Key]
		public int Id { get; set; }
	}
}
