namespace Orpheon_Bot.Util.Configuration
{
	public class Channel
	{
		public int ServerID {get; set;}
		public int ChannelID {get; set;}
		
		public Channel (int serverID, int channelID) 
		{
			ServerID = serverID;
			ChannelID = channelID;
		}
	}
}