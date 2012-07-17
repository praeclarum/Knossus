using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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

		public void Load (string path)
		{
			using (var r = new StreamReader (path)) {
				Load (r);
			}
		}

		public void Load (TextReader reader)
		{
			var verse = new List<string> ();

			for (var line = reader.ReadLine (); line != null; line = reader.ReadLine ()) {

				var tline = line.Trim ();

				if (tline.Length == 0 || tline [0] == '#') {
					continue;
				}

				if (line [0] == '.' || line [0] == '/' || line [0] == '!') {
					if (verse.Count > 0) {
						LoadVerse (verse);
					}
					verse = new List<string> ();
				}

				verse.Add (line);
			}

			if (verse.Count > 0) {
				LoadVerse (verse);
			}
		}

		void LoadVerse (List<string> verse)
		{
			switch (verse[0][0]) {
			case '/': {
				var r = new WebResource ();
				r.Load (verse);
				Resources.Add (r);
			}
				break;
			}
		}
	}
}

