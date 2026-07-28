namespace Orpheon_Bot.Util.Configuration
{
	public class Challenge
	{
		public string IconPath { get; set; }
		public string Name { get; set; }
		public string ViewerDescription {get;set;}
		public string StreamerDescription {get;set;}
		
		public Challenge (string iconPath, string name, string viewerDescription, string streamerDescription) 
		{
			IconPath = iconPath;
			Name = name;
			ViewerDescription = viewerDescription;
			StreamerDescription = streamerDescription;
		}
	}
}