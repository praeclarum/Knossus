using System;
using System.Text.RegularExpressions;

namespace Knossus
{
	public class WebResource
	{
		Regex _pathRe;
		string _path;
		public string Path {
			get { return _path; }
			set {
				_path = value;
				_pathRe = new Regex (_path);
			}
		}

		public WebResource ()
		{
			Path = "";
		}

		public bool Matches (Uri uri)
		{
			var path = uri.AbsolutePath;
			return _pathRe.IsMatch (path);
		}
	}
}

