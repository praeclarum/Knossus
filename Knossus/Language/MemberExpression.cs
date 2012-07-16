using System;

namespace Knossus.Language
{
	public class MemberExpression : Expression
	{
		public Expression Object { get; set; }
		public string Name { get; set; }

		public MemberExpression (Expression obj, string name)
		{
			Object = obj;
			Name = name;
		}

		public override string ToString ()
		{
			return string.Format ("{0}.{1}", Object, Name);
		}
	}
}

