using System.IO;

namespace Bridge_Project.Objects
{
	/// <summary>
	/// The Sender Object, associated with the Messenger.
	/// </summary>
	public class Sender
	{
		/// <summary>
		/// A string storing the Senders path
		/// </summary>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		public string SendsTo {get; set;}
		
		/// <summary>
		/// The constructor of the Sender.
		/// </summary>
		/// <param name="sendToWhere">The path in which any message will be sent out to.</param>
		public Sender (string sendToWhere) 
		{
			SendsTo = Path.GetFullPath(sendToWhere);
		}
	}
}