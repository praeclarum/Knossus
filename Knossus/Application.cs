using System;
using Knossus.Server;

namespace Knossus
{
	class Application
	{
		public static void Main (string[] args)
		{
			var app = new WebApp ();


			var server = new HttpServer (app);
			server.Start ();
			new System.Threading.AutoResetEvent (false).WaitOne ();
		}
	}
}
