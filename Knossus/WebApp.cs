using System;
using System.Collections.Generic;
using System.Linq;

namespace Knossus
{
	public class WebApp
	{
		public List<WebModule> Modules { get; set; }

		public WebApp ()
		{
			Modules = new List<WebModule> ();
		}

		public WebResource FindResource (Uri uri)
		{
			var q = from m in Modules
					let r = m.FindResource (uri)
					where r != null
					select r;
			return q.FirstOrDefault ();
		}
	}
}

