using System;
using NUnit.Framework;
using Knossus.Language;

namespace Knossus.Test
{
	[TestFixture]
	public class LanaguageParser
	{
		[Test]
		public void Add ()
		{
			var expr = Parser.ParseExpression ("2 + 3");
			Assert.IsInstanceOf <BinaryExpression> (expr);
		}

		[Test]
		public void Where ()
		{
			var expr = Parser.ParseExpression ("select bob from foo where bar == baz");
			Assert.IsInstanceOf <SelectExpression> (expr);
		}

		[Test]
		public void Join ()
		{
			var expr = Parser.ParseExpression ("select b.bob from foo as f inner join bar as b on f.id == b.id");
			Assert.IsInstanceOf <SelectExpression> (expr);
		}

		[Test]
		public void Join2 ()
		{
			var expr = Parser.ParseExpression ("select b.bob from foo as f inner join bar as b on f.id == b.id" +
				"left join baz on baz.id == f.id");
			Assert.IsInstanceOf <SelectExpression> (expr);
			var sel = (SelectExpression)expr;
			Assert.AreEqual (2, sel.From.Joins.Count);
		}

		[Test]
		public void OrderBy ()
		{
			var expr = Parser.ParseExpression ("select f.bar from foo order by f.baz");
			Assert.IsInstanceOf <SelectExpression> (expr);
			var sel = (SelectExpression)expr;
			Assert.AreEqual (1, sel.OrderBys.Count);
		}

		[Test]
		public void WhereGroupBy ()
		{
			var expr = Parser.ParseExpression ("select f.bar from foo where f.no == 89 group by f.baz");
			Assert.IsInstanceOf <SelectExpression> (expr);
			var sel = (SelectExpression)expr;
			Assert.AreEqual (1, sel.GroupBy.Expressions.Count);
			Assert.NotNull (sel.Where);
		}

		[Test]
		public void GroupBy ()
		{
			var expr = Parser.ParseExpression ("select f.bar from foo group by f.baz");
			Assert.IsInstanceOf <SelectExpression> (expr);
			var sel = (SelectExpression)expr;
			Assert.AreEqual (1, sel.GroupBy.Expressions.Count);
		}

		[Test]
		public void GroupByHaving ()
		{
			var expr = Parser.ParseExpression ("select f.bar from foo group by f.baz having f.no");
			Assert.IsInstanceOf <SelectExpression> (expr);
			var sel = (SelectExpression)expr;
			Assert.AreEqual (1, sel.GroupBy.Expressions.Count);
			Assert.NotNull (sel.GroupBy.Having);
		}
	}
}

