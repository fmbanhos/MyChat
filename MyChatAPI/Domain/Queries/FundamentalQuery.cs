using System.Collections.Generic;

namespace MyChatAPI.Domain.Queries
{
	public class FundamentalQueryResponse
	{
		public int Code { get; set; }
		public string Message { get; set; }
	}

	public class FundamentalListQueryResponse<T> : FundamentalQueryResponse
	{
		public IList<T> List { get; set; }
	}
}
