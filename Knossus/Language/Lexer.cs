using System;
using System.Collections.Generic;

namespace Knossus.Language
{
	public class Lexer : yyParser.yyInput
	{
		string _text;
		int _end;
		int _p;

		int _token;
		object _value;

		public Lexer (string text)
		{
			_text = text;
			_end = _text.Length;
			_p = 0;
		}

		#region yyInput implementation
		public bool advance ()
		{
			//
			// Advance
			//
			if (_p >= _end) {
				return false;
			}

			while (_p < _end && char.IsWhiteSpace (_text [_p])) {
				_p++;
			}

			if (_p >= _end) {
				return false;
			}

			//
			// Recognize
			//
			var ch = _text [_p];
			if (char.IsDigit (ch)) {
				ReadNumber ();
			}
			else if (char.IsLetter (ch) || ch == '_') {
				ReadIdentifier ();
			}
			else if (ch == '+' || ch == '.') {
				_p++;
				_token = (int)ch;
			}
			else if (ch == '=') {
				_p++;
				if (_p < _end && _text [_p] == '=') {
					_p++;
					_token = Token.EQ_OP;
				}
				else {
					_token = '=';
				}
			}
			else {
				throw new NotSupportedException (ch.ToString ());
			}

			return true;
		}

		public int token ()
		{
			return _token;
		}

		public object value ()
		{
			return _value;
		}
		#endregion

		Dictionary<string, int> Keywords = new Dictionary<string, int> {
			{ "as", Token.AS },
			{ "by", Token.BY },
			{ "from", Token.FROM },
			{ "group", Token.GROUP },
			{ "having", Token.HAVING },
			{ "inner", Token.INNER },
			{ "join", Token.JOIN },
			{ "on", Token.ON },
			{ "order", Token.ORDER },
			{ "select", Token.SELECT },
			{ "where", Token.WHERE },
		};

		void ReadIdentifier ()
		{
			var s = _p;
			while (_p < _end && (char.IsLetterOrDigit (_text [_p]) || _text [_p] == '_')) {
				_p++;
			}

			var t = _text.Substring (s, _p - s);
			_value = t;

			var lower = t.ToLowerInvariant ();
			if (!Keywords.TryGetValue (lower, out _token)) {
				_token = Token.IDENTIFIER;
			}
		}

		void ReadNumber ()
		{
			var s = _p;
			while (_p < _end && (char.IsDigit (_text [_p]) || _text [_p] == '.')) {
				_p++;
			}

			_value = double.Parse (_text.Substring (s, _p - s));
			_token = Token.CONSTANT;
		}
	}
}

