using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using Cecil_Bridge.Objects;
using PonyIslandChallengeRuns.Util;
using System.IO;
using System.Reflection;

namespace PonyIslandChallengeRuns
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	public class PonyIslandChallengeRuns : BaseUnityPlugin
    {
		public const string PluginName = "PonyIslandChallengeRuns";
		public const string PluginVersion = "1.0.0";
		public const string PluginGUID = "PonyIsland.Chaosyr.PonyIslandChallengeRuns";
		public static ManualLogSource CodeBaseLogger = new ManualLogSource("CodeBase");
		public static ManualLogSource PatchLogger = new ManualLogSource("Patch");
		
		public void Awake()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string DLLPath = Path.GetDirectoryName(assembly.Location);
			string Orpheon = Path.GetFullPath(GoUpAFolderUntilX.Approach("Pony Island", DLLPath) + "\\Orpheon\\Orpheon_Bot.dll");
			Logger.LogInfo("Testing...");
			Sender sender = new Sender(Orpheon);
			Receiver receiver = new Receiver(Path.GetFullPath(DLLPath + "\\Pony Island Challenge Runs.dll"));
			Messenger messenger = new Messenger(sender, receiver, Path.GetFullPath(GoUpAFolderUntilX.Approach("Pony Island", DLLPath) + "\\BepInEx\\plugins\\Bridge\\messenger.txt"));
			Logger.LogInfo("Sending message: Apple");
			messenger.SendMessage("Apple");
			StartCoroutine(new MessageReceivedCheck().Check(messenger));
		}
	}
}