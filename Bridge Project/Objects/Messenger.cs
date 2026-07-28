using System;
using System.IO;
using System.Text;

namespace Bridge_Project.Objects
{
	/// <summary>
	/// This is the base Object for the Sender.
	/// </summary>
	/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
	public class Messenger
	{
		/// <summary>
		/// This is the Sender and defines where this Messenger is going to Send a message to.
		/// </summary>
		public Sender Sender {get; set;}
		/// <summary>
		/// This is the Receiver and defines where this Messenger is going to Receive a message back.
		/// </summary>
		public Receiver Receiver {get; set;}
		
		/// <summary>
		/// This is the message variable and is what you'll read to get the raw message that a side has received.
		/// </summary>
		public string message = "";
		/// <summary>
		/// This is a internal boolean that determines whether the message has been received yet or not.
		/// </summary>
		private volatile bool receivedMessage = false;
		/// <summary>
		/// This is a internal string used to check if the previous line checked in the receiver is the same as the one it currently sees.
		/// </summary>
		private string previousLine = "";
		/// <summary>
		/// This deems the MutualFilePath in which messages should be sent note the Text File must be created at the given path.
		/// </summary>
		public string MutualFilePath = "";
		
		/// <summary>
		/// This is what we call a tupple before tupples existed... It wouldn't be here if tupples were possible. but what this does is stores the message and key for returns.
		/// </summary>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		private class retpeices
		{
			/// <summary>
			/// This is the message getting sent back.
			/// </summary>
			public string message {get; set;}
			/// <summary>
			/// This is the key getting sent back.
			/// </summary>
			public string key {get; set;}
			
			/// <summary>
			/// This is the tupple constructor.
			/// </summary>
			/// <param name="message">The message being returned.</param>
			/// <param name="key">The key being returned.</param>
			public retpeices(string message, string key)
			{
				this.message = message;
				this.key = key;
			}
		}
		
		/// <summary>
		/// This is the messenger builder, and is used to create a messenger to reach another messenger and send/receive messages back and forth.
		/// </summary>
		/// <param name="sender">The sender of the messenger, determines where messages will be sent to.</param>
		/// <param name="receiver">The receiver of the messenger, determines where messages will be sent back to.</param>
		/// <param name="PathToFile">The path to the file where messages will be stored.</param>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		public Messenger(Sender sender, Receiver receiver, string PathToFile) 
		{
			Sender = sender;
			Receiver = receiver;
			MutualFilePath = PathToFile;
		}

		/// <summary>
		/// This will Send a message to the target of this Messenger's Sender, specifically to its inverse match Messenger.
		/// </summary>
		/// <param name="message">The plain string message you want to send across, this could also be a int, or in theory another Base64, you'll need to experiment.</param>
		/// <returns>A sent message to the receiving end, as long as your messenger's are properly set.</returns>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		public void SendMessage(string message)
		{
			receivedMessage = false;
			retpeices peices = EncryptMessage(message);
			StoreMessage(Receiver, peices, Sender);
		}

		/// <summary>
		/// This encrypt's the message, and returns it and the generated key.
		/// </summary>
		/// <param name="message">The plain text message passed into SendMessage.</param>
		/// <returns>A Encrypted Message and a Decrypting Key.</returns>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		private retpeices EncryptMessage(string message)
		{
			Random random = new Random();
			byte[] randByte = new byte[random.Next(32,129)];
			random.NextBytes(randByte);
			
			string key = Convert.ToBase64String(randByte);
			byte[] bytes = Encoding.UTF8.GetBytes(key+" "+message);
			
			string base64string = Convert.ToBase64String(bytes);
			return new retpeices(base64string, key);
		}
		
		/// <summary>
		/// Takes the receiver, retpeices from the Encryption, and the Sender and stores the message to the messenger file [NOTE MAKE THE FILE LOCATION CHANGABLE]
		/// </summary>
		/// <param name="receiver">The receiver in which things will be sent back to.</param>
		/// <param name="ret">The Message and Key Post-Encryption.</param>
		/// <param name="sender">The sender in which things will be marked for sending out to.</param>
		/// <returns>A stored message to the Messenger file.</returns>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		private void StoreMessage(Receiver receiver, retpeices ret, Sender sender) 
		{
			using (FileStream fs = new FileStream(Path.GetFullPath(MutualFilePath), FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
			using (StreamWriter file = new StreamWriter(fs))
			{
				file.WriteLine("SendsTo;Mesage;Key;SendBackTo");
				file.WriteLine($"\"{sender.SendsTo}\";\"{ret.message}\";\"{ret.key}\";\"{receiver.ReceivesFrom}\"");
			}
		}
		
		/// <summary>
		/// A bool for checking if a Message has been received yet or not, and also a check to get the message received from the receiver.
		/// </summary>
		/// <returns>A true if a message has been received, and a false if it has not yet been received.</returns>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		public bool ReceiveMessage()
		{
			try
			{
				string[] lines;
				using (FileStream fs = new FileStream(Path.GetFullPath(MutualFilePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				using (StreamReader reader = new StreamReader(fs))
				{ 
					lines = reader.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				}
				String curr = lines[1];
				if (curr == previousLine || curr.Split(';')[0].Replace("\"", "") != Receiver.ReceivesFrom || curr.Split(';')[3].Replace("\"", "") != Sender.SendsTo)
					return false;
				this.message = DecryptMessage(curr.Split(';')[1].Replace("\"", ""), curr.Split(';')[2].Replace("\"", ""));
				previousLine = curr;
				this.receivedMessage = true;
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary>
		/// This decrypts the message and returns it.
		/// </summary>
		/// <param name="message">The encrypted message.</param>
		/// <param name="key">Tne encrypted key.</param>
		/// <returns>A decrypted message.</returns>
		/// <remarks>This code is provided by Creator/Chaosyr/SaxbyMod/The Stoat Lord.</remarks>
		private string DecryptMessage(string message, string key)
		{
			byte[] bytes = Convert.FromBase64String(message);
			string origMessage = Encoding.UTF8.GetString(bytes);
			return origMessage.Replace(key+" ", "");
		}
	}
}