namespace Hg.Net
{
	public enum Channel
	{
		/// <summary>
		/// Output channel: most of the communication happens on this channel. When running commands, output Mercurial writes to stdout is written to this channel.
		/// </summary>
		O = 'o',

		/// <summary>
		/// Error channel: when running commands, this correlates to stderr.
		/// </summary>
		E = 'e',

		/// <summary>
		/// Result channel: the server uses this channel to tell the client that a command finished by writing its return value (command specific).
		/// </summary>
		R = 'r',

		/// <summary>
		/// Debug channel: used when the server is started with logging to '-'.
		/// </summary>
		D = 'd'
	}
}