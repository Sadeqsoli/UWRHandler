namespace SleeplessDev
{
	public static class DB
	{
		public static readonly HeaderData AHeader = new HeaderData("Accept", "application/json");
		public static readonly HeaderData BHeader = new HeaderData("Authorization", "Bearer " + "YOUR_ACCESS_TOKEN");
		public static readonly HeaderData CHeader = new HeaderData("Content-Type", "application/json");

		public class HeaderData
		{
			public string Name { get; }
			public string Value { get; }

			public HeaderData(string name, string value)
			{
				Name = name;
				Value = value;
			}
		}
	}
}
