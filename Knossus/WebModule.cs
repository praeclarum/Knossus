using System;
using System.Collections.Generic;
using System.Linq;

namespace Knossus
{
	public class WebModule
	{
		public List<WebResource> Resources { get; set; }

		public WebModule ()
		{
			Resources = new List<WebResource> ();
		}

		public WebResource FindResource (Uri uri)
		{
			var q = from r in Resources
					where r.Matches (uri)
					select r;
			return q.FirstOrDefault ();
		}

		public void LoadFromFile (string path)
		{

		}
	}
}

