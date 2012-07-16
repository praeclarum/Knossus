using System;

namespace Knossus.Language
{
	public class VariableExpression : Expression
	{
		public string Name { get; set; }

		public VariableExpression (string name)
		{
			Name = name;
		}

		public override string ToString ()
		{
			return Name;
		}
	}
}

