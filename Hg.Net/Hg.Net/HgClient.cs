using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hg.Net
{
	public class HgClient
	{
		public List<string> Capabilities { get; private set; }
		public string HgEncoding { get; private set; }
		private readonly string _hgPath = "hg";
		private const byte HeaderLength = 5;
		private Process _cmdServer;

		public HgClient()
		{

		}

		public HgClient(string hgPath)
			: this()
		{
			_hgPath = hgPath;
		}

		public bool Connect(string pathToRepo)
		{
			var args = string.Format(@"serve --cmdserver pipe --cwd ""{0}"" --repository ""{0}""", pathToRepo);
			var serverInfo = new ProcessStartInfo(_hgPath, args)
			{
				RedirectStandardInput = true,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				CreateNoWindow = true,
				UseShellExecute = false
			};

			try
			{
				_cmdServer = Process.Start(serverInfo);
			}
			catch (Exception) { return false; }

			return Hello();
		}

		private bool Hello()
		{
			var buffer = new byte[HeaderLength];
			var readCount = ReadBytes(_cmdServer.StandardOutput.BaseStream, buffer, 0, HeaderLength);

			if (readCount != HeaderLength)
			{
				return false;
			}

			var messageLen = GetMessageLength(buffer, 1);

			var messageBuffer = new byte[messageLen];

			readCount = ReadBytes(_cmdServer.StandardOutput.BaseStream, messageBuffer, 0, messageLen);

			if (readCount != messageLen)
			{
				return false;
			}

			var message = Encoding.UTF8.GetString(messageBuffer);

			var parsedMessage = message.Split('\n')
				.Select(s => s.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries)).ToDictionary(t => t[0], t => t[1]);

			Capabilities = parsedMessage["capabilities"].Split(' ').ToList();
			HgEncoding = parsedMessage["encoding"];

			return true;
		}


		private int ReadBytes(Stream stream, byte[] buffer, int offset, int length)
		{
			var remaining = length;
			var read = 1;

			while (remaining > 0 && read > 0)
			{
				read = stream.Read(buffer, offset, remaining);
				offset += read;
				remaining -= read;
			}

			return length - remaining;
		}

		private int GetMessageLength(byte[] buffer, int offset)
		{
			return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, offset));
		}

	}
}

