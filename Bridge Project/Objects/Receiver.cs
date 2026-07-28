using System.IO;

namespace Bridge_Project.Objects
{
	/// <summary>
	/// The Receiver Object, associated with the Messenger.
	/// </summary>
	/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
	public class Receiver
	{
		/// <summary>
		/// A string storing the Receivers path
		/// </summary>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		public string ReceivesFrom {get; set; }

		/// <summary>
		/// The constructor of the Receiver.
		/// </summary>
		/// <param name="receivesFrom">The path in which any message should be sent back to.</param>
		public Receiver(string receivesFrom)
		{
			ReceivesFrom = Path.GetFullPath(receivesFrom);
		}
	}
}