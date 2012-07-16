using System;
using System.Collections.Generic;

namespace Knossus.Language
{
	public class SelectExpression : Expression
	{
		public class SingleSource
		{
			public string DatabaseName { get; set; }
			public string TableName { get; set; }
			public string TableAlias { get; set; }

			public override string ToString ()
			{
				return string.Format ("{0}.{1} AS {2}", DatabaseName, TableName, TableAlias);
			}
		}

		public enum JoinOp
		{
			Left,
			Inner,
			Cross,
		}

		public class Join
		{
			public JoinOp Op { get; set; }
			public SingleSource Source { get; set; }
			public JoinConstraint Constraint { get; set; }

			public override string ToString ()
			{
				return string.Format ("{0} {1} {2}", Op, Source, Constraint);
			}
		}

		public abstract class JoinConstraint
		{
		}

		public class OnJoinConstraint : JoinConstraint
		{
			public Expression Left { get; set; }
			public Expression Right { get; set; }

			public override string ToString ()
			{
				return string.Format ("ON {0} = {1}", Left, Right);
			}
		}

		public class JoinSource
		{
			public SingleSource From { get; set; }
			public List<Join> Joins { get; set; }

			public override string ToString ()
			{
				return string.Format ("{0}", From);
			}
		}

		public class OrderingTerm
		{
			public Expression Expr { get; set; }
			public bool Ascending { get; set; }

			public override string ToString ()
			{
				return string.Format ("{0} ASC={1}", Expr, Ascending);
			}
		}

		public class ResultColumn
		{
			public Expression Expr { get; set; }
			public string Alias { get; set; }

			public override string ToString ()
			{
				return string.Format ("{0} AS {1}", Expr, Alias);
			}
		}

		public class GroupByInfo
		{
			public List<Expression> Expressions { get; set; }
			public Expression Having { get; set; }
		}

		public List<ResultColumn> ResultColumns { get; set; }

		public JoinSource From { get; set; }

		public Expression Where { get; set; }

		public List<OrderingTerm> OrderBys { get; set; }

		public GroupByInfo GroupBy { get; set; }

		public SelectExpression ()
		{
		}
	}
}

