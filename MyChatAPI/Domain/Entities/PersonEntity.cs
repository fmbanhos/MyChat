using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Domain.Entities
{
	public class PersonEntity : FundamentalEntity
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public int IdGroup { get; set; }
	}
}
