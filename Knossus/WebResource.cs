using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;
using System.IO;

namespace Knossus
{
	public class WebResource
	{
		class Code
		{
			public string Method;
			public string CodeText;
		}
		Dictionary<string, Code> _codes = new Dictionary<string, Code> ();

		Regex _pathRe;

		string _path;
		public string Path {
			get { return _path; }
			set {
				_path = value;
				var re = new StringBuilder ();
				for (var i = 0; i < _path.Length; i++) {
					if (_path [i] == '{') {
						var s = i + 1;
						while (i < _path.Length && _path [i] != '}') {
							i++;
						}
						var a = _path.Substring (s, i - s);
						var c = a.IndexOf (':');
						var r = c > 0 ? a.Substring (c + 1) : ".*?";
						re.Append ("(?<" + a.Substring (0, c) + ">" + r + ")");
					}
					else {
						re.Append (_path [i]);
					}
				}
				_pathRe = new Regex (re.ToString ());
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

		public void Load (List<string> verse)
		{
			var code = new List<string> ();

			for (var i = 0; i < verse.Count; i++) {
				var t = verse [i].Trim ();
				if (i == 0) {
					Path = verse [i];
				}
				else {
					if (t == "GET" || t == "POST" || t == "PUT" || t == "PATCH" || t == "DELETE") {
						if (code.Count > 0) {
							LoadCode (code);
						}
						code.Clear ();
					}
					code.Add (verse [i]);
				}
			}
			if (code.Count > 0) {
				LoadCode (code);
			}
		}

		void LoadCode (List<string> verse)
		{
			var method = "";
			var s = 0;
			if (char.IsUpper (verse[0].Trim ()[0])) {
				method = verse[0].Trim ();
				s = 1;
			}

			var code = new Code {
				Method = method,
				CodeText = string.Join ("\n", verse.Skip (s)),
			};

			_codes [code.Method] = code;
		}

		public void Respond (HttpListenerContext c)
		{
			var verb = c.Request.HttpMethod;

			Code code;
			if (_codes.TryGetValue (verb, out code)) {
				c.Response.StatusCode = 200;
				using (var w = new StreamWriter (c.Response.OutputStream)) {
					w.WriteLine (code.CodeText);
				}
				c.Response.Close ();
			}
			else {
				c.Response.StatusCode = 405;
				c.Response.Close ();
			}
		}
	}
}

