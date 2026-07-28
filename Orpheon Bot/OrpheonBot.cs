using System.Reflection;
using Cecil_Bridge.Objects;
using Orpheon_Bot.Util;

namespace Orpheon_Bot;

public class OrpheonBot 
{
	public static void Main()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		string DLLPath = Path.GetDirectoryName(assembly.Location);
		string Challenges = Path.GetFullPath(GoUpAFolderUntilX.Approach("Pony Island", DLLPath) + "\\BepInEx\\plugins\\Challenges\\Pony Island Challenge Runs.dll");
		Console.WriteLine("Testing");
		Sender sender = new Sender(Challenges);
		Receiver receiver = new Receiver(DLLPath + "\\Orpheon_Bot.dll");
		Messenger messenger = new Messenger(sender, receiver, Path.GetFullPath(GoUpAFolderUntilX.Approach("Pony Island", DLLPath) + "\\BepInEx\\plugins\\Bridge\\messenger.txt"));
		RunWaiting(messenger);
	}
	
	public static string waitForMessageAndReturn(Messenger messenger)
	{
		while (!messenger.ReceiveMessage())
		{
			Thread.Sleep(100);
		}

		return messenger.message;
	}

	public static async Task RunWaiting(Messenger messenger)
	{
		while (true) {
			string message = waitForMessageAndReturn(messenger);
			if (message.Contains("Apple")) 
			{
				Console.WriteLine("Received Key 'Apple'.");
				messenger.SendMessage("Heeding your call sir! Here's your key [Bannana]");
			} else {
				Console.WriteLine("Received invalid Key.");
			}
		}
	}
}