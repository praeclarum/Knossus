using System;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace Knossus.Server
{
	public class HttpServer
	{
		HttpListener _listener;
		WebApp _app;

		public HttpServer (WebApp app)
		{
			_app = app;
		}

		public void Start ()
		{
			_listener = new HttpListener ();
			var prefix = "http://localhost:8081/";
			Debug.WriteLine (prefix);
			_listener.Prefixes.Add (prefix);
			_listener.Start ();
			_listener.BeginGetContext (HandleContext, null);
		}

		public void Stop ()
		{
			if (_listener != null) {
				_listener.Stop ();
				_listener = null;
			}
		}

		void Process (HttpListenerContext c)
		{
			var r = _app.FindResource (c.Request.Url);

			if (r == null) {
				c.Response.StatusCode = 404;
				c.Response.Close ();
			}
			else {
				c.Response.StatusCode = 200;
				c.Response.SendChunked = true;
				r.Respond (c);
			}
		}

		void HandleContext (IAsyncResult ar)
		{
			//
			// Get the context
			//
			HttpListenerContext ctx = null;
			try {
				ctx = _listener.EndGetContext (ar);
			}
			catch (Exception ex) {
				Console.WriteLine (ex);
			}

			//
			// Get the next request
			//
			_listener.BeginGetContext (HandleContext, null);

			//
			// Process
			//
			if (ctx != null) {
				Process (ctx);
			}
		}
	}
}

