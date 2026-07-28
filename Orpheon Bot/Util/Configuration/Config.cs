namespace Orpheon_Bot.Util.Configuration
{
	public class Config
	{
		string TokenID {get; set;}
		Channel VotingChannel {get; set;}
		Channel InfluenceSays {get;set;}
		Channel ChatSays {get;set;}
		Channel AnswerAsADMGCharacter {get;set;}
		List<Admins> Admins {get;set;}
		List<Challenge> Challenges {get;set;}
	}
}