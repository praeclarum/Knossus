using System;
using Knossus.Server;

namespace Knossus
{
	class Application
	{
		public static void Main (string[] args)
		{
			var app = new WebApp ();

			var mod = new WebModule ();
			mod.Load ("/Users/fak/Projects/Knossus/Knossus/Modules/movie.w");
			app.Modules.Add (mod);

			var server = new HttpServer (app);
			server.Start ();
			new System.Threading.AutoResetEvent (false).WaitOne ();
		}
	}
}
