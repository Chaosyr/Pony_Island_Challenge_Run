namespace Orpheon_Bot.Util.Configuration
{
	public class Admins
	{
		public string Name {get; set;}
		public int ID { get; set; }
		public Admins (string name, int id) 
		{
			Name = name;
			ID = id;
		}
	}
}