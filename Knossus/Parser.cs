using System;

namespace Knossus.Language
{
	public partial class Parser
	{
		int yacc_verbose_flag = 0;

		public Parser ()
		{
		}

		public static Expression ParseExpression (string text)
		{
			var p = new Parser ();
			var lex = new Lexer (text);
			var r = p.yyparse (lex);
			return (Expression)r;
		}
	}
}

