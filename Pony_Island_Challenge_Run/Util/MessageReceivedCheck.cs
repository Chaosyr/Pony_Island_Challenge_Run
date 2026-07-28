using System.Collections;
using Cecil_Bridge.Objects;
using UnityEngine;

namespace PonyIslandChallengeRuns.Util
{
	public class MessageReceivedCheck
	{
		public IEnumerator Check(Messenger messenger)
		{
			while (true)
			{
				if (messenger.ReceiveMessage())
				{
					if (messenger.message.Contains("Bannana")) 
					{
						PonyIslandChallengeRuns.CodeBaseLogger.LogInfo("Received Key 'Bannana'.");
						messenger.SendMessage("Heeding your call sir! Here's your key [Apple]");
					} else {
						PonyIslandChallengeRuns.CodeBaseLogger.LogError("Received invalid Key.");
					}
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
	}
}