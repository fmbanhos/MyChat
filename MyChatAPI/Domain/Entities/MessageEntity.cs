using System;
using System.ComponentModel.DataAnnotations;

namespace MyChatAPI.Domain.Entities
{
	public class MessageEntity : FundamentalEntity
	{
		[Required]
		public int IdGroup { get; set; }
		[Required]
		public int IdOriginPerson { get; set; }
		[Required]
		public int IdDestinationPerson { get; set; }
		[Required]
		public string Content { get; set; }
		[Required]
		public DateTime Date { get; set; }
	}
}
