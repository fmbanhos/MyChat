using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Domain.Entities
{
	public class GroupEntity : FundamentalEntity
	{
		[Required]
		public string Name { get; set; }
	}
}
