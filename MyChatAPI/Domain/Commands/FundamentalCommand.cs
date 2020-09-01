namespace MyChatAPI.Domain.Commands
{
	public class FundamentalCommandResponse
	{
		public int Code { get; set; }
		public string Message { get; set; }
	}

	public class FundamentalCreateCommandResponse<T> : FundamentalCommandResponse
	{
		public T Entity { get; set; }
	}
}
