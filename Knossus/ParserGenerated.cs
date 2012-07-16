// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "Parser.jay"
using System.Text;
using System.IO;
using System;
using System.Collections.Generic;

namespace Knossus.Language
{
	/// <summary>
	/// The Parser
	/// </summary>
	public partial class Parser
	{

		
#line default

  /** error output stream.
      It should be changeable.
    */
  public System.IO.TextWriter ErrorOutput = System.Console.Out;

  /** simplified error message.
      @see <a href="#yyerror(java.lang.String, java.lang.String[])">yyerror</a>
    */
  public void yyerror (string message) {
    yyerror(message, null);
  }

  /* An EOF token */
  public int eof_token;

  /** (syntax) error message.
      Can be overwritten to control message format.
      @param message text to be displayed.
      @param expected vector of acceptable tokens, if available.
    */
  public void yyerror (string message, string[] expected) {
    if ((yacc_verbose_flag > 0) && (expected != null) && (expected.Length  > 0)) {
      ErrorOutput.Write (message+", expecting");
      for (int n = 0; n < expected.Length; ++ n)
        ErrorOutput.Write (" "+expected[n]);
        ErrorOutput.WriteLine ();
    } else
      ErrorOutput.WriteLine (message);
  }

  /** debugging support, requires the package jay.yydebug.
      Set to null to suppress debugging messages.
    */
//t  internal yydebug.yyDebug debug;

  protected const int yyFinal = 9;
//t // Put this array into a separate class so it is only initialized if debugging is actually used
//t // Use MarshalByRefObject to disable inlining
//t class YYRules : MarshalByRefObject {
//t  public static readonly string [] yyRule = {
//t    "$accept : expression",
//t    "primary_expression : IDENTIFIER",
//t    "primary_expression : CONSTANT",
//t    "primary_expression : STRING_LITERAL",
//t    "primary_expression : '(' expression ')'",
//t    "postfix_expression : primary_expression",
//t    "postfix_expression : postfix_expression '(' ')'",
//t    "postfix_expression : postfix_expression '(' argument_expression_list ')'",
//t    "postfix_expression : postfix_expression '.' IDENTIFIER",
//t    "argument_expression_list : assignment_expression",
//t    "argument_expression_list : argument_expression_list ',' assignment_expression",
//t    "unary_expression : postfix_expression",
//t    "unary_expression : unary_operator cast_expression",
//t    "unary_operator : '+'",
//t    "unary_operator : '-'",
//t    "unary_operator : '!'",
//t    "cast_expression : unary_expression",
//t    "multiplicative_expression : cast_expression",
//t    "multiplicative_expression : multiplicative_expression '*' cast_expression",
//t    "multiplicative_expression : multiplicative_expression '/' cast_expression",
//t    "multiplicative_expression : multiplicative_expression '%' cast_expression",
//t    "additive_expression : multiplicative_expression",
//t    "additive_expression : additive_expression '+' multiplicative_expression",
//t    "additive_expression : additive_expression '-' multiplicative_expression",
//t    "shift_expression : additive_expression",
//t    "relational_expression : shift_expression",
//t    "relational_expression : relational_expression '<' shift_expression",
//t    "relational_expression : relational_expression '>' shift_expression",
//t    "relational_expression : relational_expression LE_OP shift_expression",
//t    "relational_expression : relational_expression GE_OP shift_expression",
//t    "equality_expression : relational_expression",
//t    "equality_expression : equality_expression EQ_OP relational_expression",
//t    "equality_expression : equality_expression NE_OP relational_expression",
//t    "and_expression : equality_expression",
//t    "exclusive_or_expression : and_expression",
//t    "inclusive_or_expression : exclusive_or_expression",
//t    "logical_and_expression : inclusive_or_expression",
//t    "logical_and_expression : logical_and_expression AND_OP inclusive_or_expression",
//t    "logical_or_expression : logical_and_expression",
//t    "logical_or_expression : logical_or_expression OR_OP logical_and_expression",
//t    "conditional_expression : logical_or_expression",
//t    "conditional_expression : logical_or_expression '?' expression ':' conditional_expression",
//t    "assignment_expression : conditional_expression",
//t    "assignment_expression : unary_expression assignment_operator assignment_expression",
//t    "assignment_operator : '='",
//t    "assignment_operator : MUL_ASSIGN",
//t    "assignment_operator : DIV_ASSIGN",
//t    "assignment_operator : MOD_ASSIGN",
//t    "assignment_operator : ADD_ASSIGN",
//t    "assignment_operator : SUB_ASSIGN",
//t    "select_expression : select_core",
//t    "select_expression : select_core select_orderby",
//t    "select_expression : select_core select_orderby select_limit",
//t    "select_expression : select_core select_limit",
//t    "select_core : SELECT result_column_list select_from",
//t    "select_core : SELECT result_column_list select_from select_where",
//t    "select_core : SELECT result_column_list select_from select_groupby",
//t    "select_core : SELECT result_column_list select_from select_where select_groupby",
//t    "result_column : conditional_expression",
//t    "result_column : conditional_expression AS IDENTIFIER",
//t    "result_column_list : result_column",
//t    "result_column_list : result_column_list ',' result_column",
//t    "select_from : FROM join_source",
//t    "join_source : single_source",
//t    "join_source : single_source join_list",
//t    "join : join_op single_source join_constraint",
//t    "join_op : LEFT JOIN",
//t    "join_op : INNER JOIN",
//t    "join_op : JOIN",
//t    "join_op : CROSS JOIN",
//t    "join_constraint : ON relational_expression EQ_OP relational_expression",
//t    "join_list : join",
//t    "join_list : join_list join",
//t    "single_source : IDENTIFIER",
//t    "single_source : IDENTIFIER '.' IDENTIFIER",
//t    "single_source : IDENTIFIER AS IDENTIFIER",
//t    "single_source : IDENTIFIER '.' IDENTIFIER AS IDENTIFIER",
//t    "single_source : IDENTIFIER IDENTIFIER",
//t    "single_source : IDENTIFIER '.' IDENTIFIER IDENTIFIER",
//t    "select_where : WHERE logical_or_expression",
//t    "select_groupby : GROUP BY shift_expression_list",
//t    "select_groupby : GROUP BY shift_expression_list HAVING logical_or_expression",
//t    "select_orderby : ORDER BY ordering_term_list",
//t    "ordering_term : shift_expression",
//t    "ordering_term : shift_expression ASC",
//t    "ordering_term : shift_expression DESC",
//t    "ordering_term_list : ordering_term",
//t    "ordering_term_list : ordering_term_list ',' ordering_term",
//t    "select_limit : LIMIT shift_expression",
//t    "select_limit : LIMIT shift_expression ',' shift_expression",
//t    "select_limit : LIMIT shift_expression OFFSET shift_expression",
//t    "shift_expression_list : shift_expression",
//t    "shift_expression_list : shift_expression_list ',' shift_expression",
//t    "expression : assignment_expression",
//t    "expression : select_expression",
//t  };
//t public static string getRule (int index) {
//t    return yyRule [index];
//t }
//t}
  protected static readonly string [] yyNames = {    
    "end-of-file",null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,"'!'",null,null,null,"'%'",null,
    null,"'('","')'","'*'","'+'","','","'-'","'.'","'/'",null,null,null,
    null,null,null,null,null,null,null,"':'",null,"'<'","'='","'>'","'?'",
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,"IDENTIFIER",
    "CONSTANT","STRING_LITERAL","SIZEOF","PTR_OP","INC_OP","DEC_OP",
    "LEFT_OP","RIGHT_OP","LE_OP","GE_OP","EQ_OP","NE_OP","AND_OP","OR_OP",
    "MUL_ASSIGN","DIV_ASSIGN","MOD_ASSIGN","ADD_ASSIGN","SUB_ASSIGN",
    "LEFT_ASSIGN","RIGHT_ASSIGN","AND_ASSIGN","XOR_ASSIGN","OR_ASSIGN",
    "TYPE_NAME","TYPEDEF","EXTERN","STATIC","AUTO","REGISTER","INLINE",
    "RESTRICT","CHAR","SHORT","INT","LONG","SIGNED","UNSIGNED","FLOAT",
    "DOUBLE","CONST","VOLATILE","VOID","BOOL","COMPLEX","IMAGINARY",
    "STRUCT","UNION","ENUM","ELLIPSIS","SELECT","AS","FROM","WHERE",
    "LIMIT","OFFSET","ORDER","ASC","DESC","GROUP","BY","HAVING","LEFT",
    "INNER","CROSS","JOIN","ON","CASE","DEFAULT","IF","ELSE","SWITCH",
    "WHILE","DO","FOR","GOTO","CONTINUE","BREAK","RETURN",
  };

  /** index-checked interface to yyNames[].
      @param token single character or %token value.
      @return token name or [illegal] or [unknown].
    */
//t  public static string yyname (int token) {
//t    if ((token < 0) || (token > yyNames.Length)) return "[illegal]";
//t    string name;
//t    if ((name = yyNames[token]) != null) return name;
//t    return "[unknown]";
//t  }

  int yyExpectingState;
  /** computes list of expected tokens on error by tracing the tables.
      @param state for which to compute the list.
      @return list of token names.
    */
  protected int [] yyExpectingTokens (int state){
    int token, n, len = 0;
    bool[] ok = new bool[yyNames.Length];
    if ((n = yySindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyNames.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyNames[token] != null) {
          ++ len;
          ok[token] = true;
        }
    if ((n = yyRindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyNames.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyNames[token] != null) {
          ++ len;
          ok[token] = true;
        }
    int [] result = new int [len];
    for (n = token = 0; n < len;  ++ token)
      if (ok[token]) result[n++] = token;
    return result;
  }
  protected string[] yyExpecting (int state) {
    int [] tokens = yyExpectingTokens (state);
    string [] result = new string[tokens.Length];
    for (int n = 0; n < tokens.Length;  n++)
      result[n++] = yyNames[tokens [n]];
    return result;
  }

  /** the generated parser, with debugging messages.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @param yydebug debug message writer implementing yyDebug, or null.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  internal Object yyparse (yyParser.yyInput yyLex, Object yyd)
				 {
//t    this.debug = (yydebug.yyDebug)yyd;
    return yyparse(yyLex);
  }

  /** initial size and increment of the state/value stack [default 256].
      This is not final so that it can be overwritten outside of invocations
      of yyparse().
    */
  protected int yyMax;

  /** executed at the beginning of a reduce action.
      Used as $$ = yyDefault($1), prior to the user-specified action, if any.
      Can be overwritten to provide deep copy, etc.
      @param first value for $1, or null.
      @return first.
    */
  protected Object yyDefault (Object first) {
    return first;
  }

	static int[] global_yyStates;
	static object[] global_yyVals;
	protected bool use_global_stacks;
	object[] yyVals;					// value stack
	object yyVal;						// value stack ptr
	int yyToken;						// current input
	int yyTop;

  /** the generated parser.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  internal Object yyparse (yyParser.yyInput yyLex)
  {
    if (yyMax <= 0) yyMax = 256;		// initial size
    int yyState = 0;                   // state stack ptr
    int [] yyStates;               	// state stack 
    yyVal = null;
    yyToken = -1;
    int yyErrorFlag = 0;				// #tks to shift
	if (use_global_stacks && global_yyStates != null) {
		yyVals = global_yyVals;
		yyStates = global_yyStates;
   } else {
		yyVals = new object [yyMax];
		yyStates = new int [yyMax];
		if (use_global_stacks) {
			global_yyVals = yyVals;
			global_yyStates = yyStates;
		}
	}

    /*yyLoop:*/ for (yyTop = 0;; ++ yyTop) {
      if (yyTop >= yyStates.Length) {			// dynamically increase
        global::System.Array.Resize (ref yyStates, yyStates.Length+yyMax);
        global::System.Array.Resize (ref yyVals, yyVals.Length+yyMax);
      }
      yyStates[yyTop] = yyState;
      yyVals[yyTop] = yyVal;
//t      if (debug != null) debug.push(yyState, yyVal);

      /*yyDiscarded:*/ while (true) {	// discarding a token does not change stack
        int yyN;
        if ((yyN = yyDefRed[yyState]) == 0) {	// else [default] reduce (yyN)
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
//t            if (debug != null)
//t              debug.lex(yyState, yyToken, yyname(yyToken), yyLex.value());
          }
          if ((yyN = yySindex[yyState]) != 0 && ((yyN += yyToken) >= 0)
              && (yyN < yyTable.Length) && (yyCheck[yyN] == yyToken)) {
//t            if (debug != null)
//t              debug.shift(yyState, yyTable[yyN], yyErrorFlag-1);
            yyState = yyTable[yyN];		// shift to yyN
            yyVal = yyLex.value();
            yyToken = -1;
            if (yyErrorFlag > 0) -- yyErrorFlag;
            goto continue_yyLoop;
          }
          if ((yyN = yyRindex[yyState]) != 0 && (yyN += yyToken) >= 0
              && yyN < yyTable.Length && yyCheck[yyN] == yyToken)
            yyN = yyTable[yyN];			// reduce (yyN)
          else
            switch (yyErrorFlag) {
  
            case 0:
              yyExpectingState = yyState;
              // yyerror(String.Format ("syntax error, got token `{0}'", yyname (yyToken)), yyExpecting(yyState));
//t              if (debug != null) debug.error("syntax error");
              if (yyToken == 0 /*eof*/ || yyToken == eof_token) throw new yyParser.yyUnexpectedEof ();
              goto case 1;
            case 1: case 2:
              yyErrorFlag = 3;
              do {
                if ((yyN = yySindex[yyStates[yyTop]]) != 0
                    && (yyN += Token.yyErrorCode) >= 0 && yyN < yyTable.Length
                    && yyCheck[yyN] == Token.yyErrorCode) {
//t                  if (debug != null)
//t                    debug.shift(yyStates[yyTop], yyTable[yyN], 3);
                  yyState = yyTable[yyN];
                  yyVal = yyLex.value();
                  goto continue_yyLoop;
                }
//t                if (debug != null) debug.pop(yyStates[yyTop]);
              } while (-- yyTop >= 0);
//t              if (debug != null) debug.reject();
              throw new yyParser.yyException("irrecoverable syntax error");
  
            case 3:
              if (yyToken == 0) {
//t                if (debug != null) debug.reject();
                throw new yyParser.yyException("irrecoverable syntax error at end-of-file");
              }
//t              if (debug != null)
//t                debug.discard(yyState, yyToken, yyname(yyToken),
//t  							yyLex.value());
              yyToken = -1;
              goto continue_yyDiscarded;		// leave stack alone
            }
        }
        int yyV = yyTop + 1-yyLen[yyN];
//t        if (debug != null)
//t          debug.reduce(yyState, yyStates[yyV-1], yyN, YYRules.getRule (yyN), yyLen[yyN]);
        yyVal = yyV > yyTop ? null : yyVals[yyV]; // yyVal = yyDefault(yyV > yyTop ? null : yyVals[yyV]);
        switch (yyN) {
case 1:
#line 37 "Parser.jay"
  { yyVal = new VariableExpression((yyVals[0+yyTop]).ToString()); }
  break;
case 2:
#line 38 "Parser.jay"
  { yyVal = new ConstantExpression(yyVals[0+yyTop]); }
  break;
case 3:
#line 39 "Parser.jay"
  { yyVal = new ConstantExpression(yyVals[0+yyTop]); }
  break;
case 4:
#line 40 "Parser.jay"
  { yyVal = yyVals[-2+yyTop]; }
  break;
case 6:
#line 48 "Parser.jay"
  {
		yyVal = new FuncallExpression((Expression)yyVals[-2+yyTop], new List<Expression> ());
	}
  break;
case 7:
#line 52 "Parser.jay"
  {
		yyVal = new FuncallExpression((Expression)yyVals[-3+yyTop], (List<Expression>)yyVals[-1+yyTop]);
	}
  break;
case 8:
#line 56 "Parser.jay"
  {
		yyVal = new MemberExpression((Expression)yyVals[-2+yyTop], (yyVals[0+yyTop]).ToString());
	}
  break;
case 9:
  case_9();
  break;
case 10:
  case_10();
  break;
case 12:
#line 79 "Parser.jay"
  {
		yyVal = new UnaryExpression((Unop)yyVals[-1+yyTop], (Expression)yyVals[0+yyTop]);
	}
  break;
case 13:
#line 83 "Parser.jay"
  { yyVal = Unop.Add; }
  break;
case 14:
#line 84 "Parser.jay"
  { yyVal = Unop.Subtract; }
  break;
case 15:
#line 85 "Parser.jay"
  { yyVal = Unop.Not; }
  break;
case 18:
#line 97 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.Multiply, (Expression)yyVals[0+yyTop]);
	}
  break;
case 19:
#line 101 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.Divide, (Expression)yyVals[0+yyTop]);
	}
  break;
case 20:
#line 105 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.Mod, (Expression)yyVals[0+yyTop]);
	}
  break;
case 22:
#line 113 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.Add, (Expression)yyVals[0+yyTop]);
	}
  break;
case 23:
#line 117 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.Subtract, (Expression)yyVals[0+yyTop]);
	}
  break;
case 26:
#line 129 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.LessThan, (Expression)yyVals[0+yyTop]);
	}
  break;
case 27:
#line 133 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.GreaterThan, (Expression)yyVals[0+yyTop]);
	}
  break;
case 28:
#line 137 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.LessThanOrEqual, (Expression)yyVals[0+yyTop]);
	}
  break;
case 29:
#line 141 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.GreaterThanOrEqual, (Expression)yyVals[0+yyTop]);
	}
  break;
case 31:
#line 149 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.Equals, (Expression)yyVals[0+yyTop]);
	}
  break;
case 32:
#line 153 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.NotEquals, (Expression)yyVals[0+yyTop]);
	}
  break;
case 37:
#line 173 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.LogicalAnd, (Expression)yyVals[0+yyTop]);
	}
  break;
case 39:
#line 181 "Parser.jay"
  {
		yyVal = new BinaryExpression((Expression)yyVals[-2+yyTop], Binop.LogicalOr, (Expression)yyVals[0+yyTop]);
	}
  break;
case 43:
  case_43();
  break;
case 44:
#line 205 "Parser.jay"
  { yyVal = Binop.Equals; }
  break;
case 45:
#line 206 "Parser.jay"
  { yyVal = Binop.Multiply; }
  break;
case 46:
#line 207 "Parser.jay"
  { yyVal = Binop.Divide; }
  break;
case 47:
#line 208 "Parser.jay"
  { yyVal = Binop.Mod; }
  break;
case 48:
#line 209 "Parser.jay"
  { yyVal = Binop.Add; }
  break;
case 49:
#line 210 "Parser.jay"
  { yyVal = Binop.Subtract; }
  break;
case 50:
  case_50();
  break;
case 51:
  case_51();
  break;
case 52:
  case_52();
  break;
case 53:
  case_53();
  break;
case 54:
  case_54();
  break;
case 55:
  case_55();
  break;
case 56:
  case_56();
  break;
case 57:
  case_57();
  break;
case 58:
  case_58();
  break;
case 59:
  case_59();
  break;
case 60:
#line 298 "Parser.jay"
  {
		yyVal = new List<SelectExpression.ResultColumn> { (SelectExpression.ResultColumn)yyVals[0+yyTop], };
	}
  break;
case 61:
  case_61();
  break;
case 62:
#line 311 "Parser.jay"
  {
		yyVal = yyVals[0+yyTop];
	}
  break;
case 63:
  case_63();
  break;
case 64:
  case_64();
  break;
case 65:
  case_65();
  break;
case 66:
#line 345 "Parser.jay"
  {
		yyVal = SelectExpression.JoinOp.Left;
	}
  break;
case 67:
#line 349 "Parser.jay"
  {
		yyVal = SelectExpression.JoinOp.Inner;
	}
  break;
case 68:
#line 353 "Parser.jay"
  {
		yyVal = SelectExpression.JoinOp.Inner;
	}
  break;
case 69:
#line 357 "Parser.jay"
  {
		yyVal = SelectExpression.JoinOp.Cross;
	}
  break;
case 70:
#line 364 "Parser.jay"
  {
		yyVal = new SelectExpression.OnJoinConstraint { Left = (Expression)yyVals[-2+yyTop], Right = (Expression)yyVals[0+yyTop], };
	}
  break;
case 71:
#line 371 "Parser.jay"
  {
		yyVal = new List<SelectExpression.Join> { (SelectExpression.Join)yyVals[0+yyTop] };
	}
  break;
case 72:
  case_72();
  break;
case 73:
  case_73();
  break;
case 74:
  case_74();
  break;
case 75:
  case_75();
  break;
case 76:
  case_76();
  break;
case 77:
  case_77();
  break;
case 78:
  case_78();
  break;
case 79:
#line 430 "Parser.jay"
  {
		yyVal = yyVals[0+yyTop];
	}
  break;
case 80:
#line 437 "Parser.jay"
  {
		yyVal = new SelectExpression.GroupByInfo { Expressions = (List<Expression>)yyVals[0+yyTop], };
	}
  break;
case 81:
#line 441 "Parser.jay"
  {
		yyVal = new SelectExpression.GroupByInfo { Expressions = (List<Expression>)yyVals[-2+yyTop], Having = (Expression)yyVals[0+yyTop], };
	}
  break;
case 82:
#line 448 "Parser.jay"
  {
		yyVal = yyVals[0+yyTop];
	}
  break;
case 83:
  case_83();
  break;
case 84:
  case_84();
  break;
case 85:
  case_85();
  break;
case 86:
#line 479 "Parser.jay"
  {
		yyVal = new List<SelectExpression.OrderingTerm> { (SelectExpression.OrderingTerm)yyVals[0+yyTop], };
	}
  break;
case 87:
  case_87();
  break;
case 91:
#line 498 "Parser.jay"
  {
		yyVal = new List<Expression> { (Expression)yyVals[0+yyTop], };
	}
  break;
case 92:
  case_92();
  break;
#line default
        }
        yyTop -= yyLen[yyN];
        yyState = yyStates[yyTop];
        int yyM = yyLhs[yyN];
        if (yyState == 0 && yyM == 0) {
//t          if (debug != null) debug.shift(0, yyFinal);
          yyState = yyFinal;
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
//t            if (debug != null)
//t               debug.lex(yyState, yyToken,yyname(yyToken), yyLex.value());
          }
          if (yyToken == 0) {
//t            if (debug != null) debug.accept(yyVal);
            return yyVal;
          }
          goto continue_yyLoop;
        }
        if (((yyN = yyGindex[yyM]) != 0) && ((yyN += yyState) >= 0)
            && (yyN < yyTable.Length) && (yyCheck[yyN] == yyState))
          yyState = yyTable[yyN];
        else
          yyState = yyDgoto[yyM];
//t        if (debug != null) debug.shift(yyStates[yyTop], yyState);
	 goto continue_yyLoop;
      continue_yyDiscarded: ;	// implements the named-loop continue: 'continue yyDiscarded'
      }
    continue_yyLoop: ;		// implements the named-loop continue: 'continue yyLoop'
    }
  }

/*
 All more than 3 lines long rules are wrapped into a method
*/
void case_9()
#line 61 "Parser.jay"
{
		var l = new List<Expression>();
		l.Add((Expression)yyVals[0+yyTop]);
		yyVal = l;
	}

void case_10()
#line 67 "Parser.jay"
{
		var l = (List<Expression>)yyVals[-2+yyTop];
		l.Add((Expression)yyVals[0+yyTop]);
		yyVal = l;
	}

void case_43()
#line 192 "Parser.jay"
{
		var op = (Binop)yyVals[-1+yyTop];
		if (op == Binop.Equals) {
			yyVal = new AssignExpression((Expression)yyVals[-2+yyTop], (Expression)yyVals[0+yyTop]);
		}
		else {
			var left = (Expression)yyVals[-2+yyTop]; 
			yyVal = new AssignExpression(left, new BinaryExpression (left, op, (Expression)yyVals[0+yyTop]));
		}
	}

void case_50()
#line 215 "Parser.jay"
{
		var sel = (SelectExpression)yyVals[0+yyTop];
		yyVal = sel;
	}

void case_51()
#line 220 "Parser.jay"
{
		var sel = (SelectExpression)yyVals[-1+yyTop];
		sel.OrderBys = (List<SelectExpression.OrderingTerm>)yyVals[0+yyTop];
		yyVal = sel;
	}

void case_52()
#line 226 "Parser.jay"
{
		var sel = (SelectExpression)yyVals[-2+yyTop];
		throw new NotImplementedException ();
		yyVal = sel;
	}

void case_53()
#line 232 "Parser.jay"
{
		var sel = (SelectExpression)yyVals[-1+yyTop];
		throw new NotImplementedException ();
		yyVal = sel;
	}

void case_54()
#line 241 "Parser.jay"
{
		var sel = new SelectExpression {
			ResultColumns = (List<SelectExpression.ResultColumn>)yyVals[-1+yyTop],
			From = (SelectExpression.JoinSource)yyVals[0+yyTop],
		};
		yyVal = sel;
	}

void case_55()
#line 249 "Parser.jay"
{
		var sel = new SelectExpression {
			ResultColumns = (List<SelectExpression.ResultColumn>)yyVals[-2+yyTop],
			From = (SelectExpression.JoinSource)yyVals[-1+yyTop],
			Where = (Expression)yyVals[0+yyTop],
		};
		yyVal = sel;
	}

void case_56()
#line 258 "Parser.jay"
{
		var sel = new SelectExpression {
			ResultColumns = (List<SelectExpression.ResultColumn>)yyVals[-2+yyTop],
			From = (SelectExpression.JoinSource)yyVals[-1+yyTop],
			GroupBy = (SelectExpression.GroupByInfo)yyVals[0+yyTop],
		};
		yyVal = sel;
	}

void case_57()
#line 267 "Parser.jay"
{
		var sel = new SelectExpression {
			ResultColumns = (List<SelectExpression.ResultColumn>)yyVals[-3+yyTop],
			From = (SelectExpression.JoinSource)yyVals[-2+yyTop],
			Where = (Expression)yyVals[-1+yyTop],
			GroupBy = (SelectExpression.GroupByInfo)yyVals[0+yyTop],
		};
		yyVal = sel;
	}

void case_58()
#line 280 "Parser.jay"
{
		yyVal = new SelectExpression.ResultColumn {
			Expr = (Expression)yyVals[0+yyTop],
		};
	}

void case_59()
#line 286 "Parser.jay"
{
		yyVal = new SelectExpression.ResultColumn {
			Expr = (Expression)yyVals[-2+yyTop],
			Alias = (yyVals[0+yyTop]).ToString (),
		};
	}

void case_61()
#line 300 "Parser.jay"
{
		var list = (List<SelectExpression.ResultColumn>)yyVals[-2+yyTop];
		list.Add ((SelectExpression.ResultColumn)yyVals[0+yyTop]);
		yyVal = list;
	}

void case_63()
#line 316 "Parser.jay"
{
		yyVal = new SelectExpression.JoinSource {
			From = (SelectExpression.SingleSource)yyVals[0+yyTop],
		};
	}

void case_64()
#line 322 "Parser.jay"
{
		yyVal = new SelectExpression.JoinSource {
			From = (SelectExpression.SingleSource)yyVals[-1+yyTop],
			Joins = (List<SelectExpression.Join>)yyVals[0+yyTop],
		};
	}

void case_65()
#line 332 "Parser.jay"
{
		yyVal = new SelectExpression.Join {
			Op = (SelectExpression.JoinOp)yyVals[-2+yyTop],
			Source = (SelectExpression.SingleSource)yyVals[-1+yyTop],
			Constraint = (SelectExpression.JoinConstraint)yyVals[0+yyTop],
		};
	}

void case_72()
#line 373 "Parser.jay"
{
		var list = (List<SelectExpression.Join>)yyVals[-1+yyTop];
		list.Add ((SelectExpression.Join)yyVals[0+yyTop]);
		yyVal = list;
	}

void case_73()
#line 382 "Parser.jay"
{
		yyVal = new SelectExpression.SingleSource {
			TableName = yyVals[0+yyTop].ToString (),
		};
	}

void case_74()
#line 388 "Parser.jay"
{
		yyVal = new SelectExpression.SingleSource {
			DatabaseName = yyVals[-2+yyTop].ToString (),
			TableName = yyVals[0+yyTop].ToString (),
		};
	}

void case_75()
#line 395 "Parser.jay"
{
		yyVal = new SelectExpression.SingleSource {
			TableName = yyVals[-2+yyTop].ToString (),
			TableAlias = yyVals[0+yyTop].ToString (),
		};
	}

void case_76()
#line 402 "Parser.jay"
{
		yyVal = new SelectExpression.SingleSource {
			DatabaseName = yyVals[-4+yyTop].ToString (),
			TableName = yyVals[-2+yyTop].ToString (),
			TableAlias = yyVals[0+yyTop].ToString (),
		};
	}

void case_77()
#line 410 "Parser.jay"
{
		yyVal = new SelectExpression.SingleSource {
			TableName = yyVals[-1+yyTop].ToString (),
			TableAlias = yyVals[0+yyTop].ToString (),
		};
	}

void case_78()
#line 417 "Parser.jay"
{
		yyVal = new SelectExpression.SingleSource {
			DatabaseName = yyVals[-3+yyTop].ToString (),
			TableName = yyVals[-1+yyTop].ToString (),
			TableAlias = yyVals[0+yyTop].ToString (),
		};
	}

void case_83()
#line 453 "Parser.jay"
{
		yyVal = new SelectExpression.OrderingTerm {
			Expr = (Expression)yyVals[0+yyTop],
			Ascending = true,
		};
	}

void case_84()
#line 460 "Parser.jay"
{
		yyVal = new SelectExpression.OrderingTerm {
			Expr = (Expression)yyVals[-1+yyTop],
			Ascending = true,
		};
	}

void case_85()
#line 467 "Parser.jay"
{
		yyVal = new SelectExpression.OrderingTerm {
			Expr = (Expression)yyVals[-1+yyTop],
			Ascending = false,
		};
	}

void case_87()
#line 481 "Parser.jay"
{
		var list = (List<SelectExpression.OrderingTerm>)yyVals[-2+yyTop];
		list.Add ((SelectExpression.OrderingTerm)yyVals[0+yyTop]);
		yyVal = list;
	}

void case_92()
#line 500 "Parser.jay"
{
		var list = (List<Expression>)yyVals[-2+yyTop];
		list.Add ((Expression)yyVals[0+yyTop]);
		yyVal = list;
	}

#line default
   static readonly short [] yyLhs  = {              -1,
    1,    1,    1,    1,    2,    2,    2,    2,    3,    3,
    5,    5,    6,    6,    6,    7,    8,    8,    8,    8,
    9,    9,    9,   10,   11,   11,   11,   11,   11,   12,
   12,   12,   13,   14,   15,   16,   16,   17,   17,   18,
   18,    4,    4,   19,   19,   19,   19,   19,   19,   20,
   20,   20,   20,   21,   21,   21,   21,   28,   28,   24,
   24,   25,   29,   29,   32,   33,   33,   33,   33,   34,
   31,   31,   30,   30,   30,   30,   30,   30,   26,   27,
   27,   22,   37,   37,   37,   36,   36,   23,   23,   23,
   35,   35,    0,    0,
  };
   static readonly short [] yyLen = {           2,
    1,    1,    1,    3,    1,    3,    4,    3,    1,    3,
    1,    2,    1,    1,    1,    1,    1,    3,    3,    3,
    1,    3,    3,    1,    1,    3,    3,    3,    3,    1,
    3,    3,    1,    1,    1,    1,    3,    1,    3,    1,
    5,    1,    3,    1,    1,    1,    1,    1,    1,    1,
    2,    3,    2,    3,    4,    4,    5,    1,    3,    1,
    3,    2,    1,    2,    3,    2,    2,    1,    2,    4,
    1,    2,    1,    3,    3,    5,    2,    4,    2,    3,
    5,    3,    1,    2,    2,    1,    3,    2,    4,    4,
    1,    3,    1,    1,
  };
   static readonly short [] yyDefRed = {            0,
    1,    2,    3,    0,    0,   13,   14,   15,    0,    5,
    0,   93,    0,    0,   17,    0,    0,   25,    0,    0,
   34,   35,   36,    0,    0,   42,   94,    0,   16,    0,
    0,   60,    0,    0,    0,   45,   46,   47,   48,   49,
   44,    0,   12,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   53,    0,    0,    0,    0,    4,    6,    0,    9,    8,
   43,   18,   19,   20,    0,    0,   28,   29,   26,   27,
    0,    0,   37,    0,    0,    0,    0,   52,   59,    0,
   62,    0,   61,    0,    0,    0,   56,    7,    0,    0,
    0,    0,    0,    0,   86,   77,    0,    0,    0,    0,
    0,   68,    0,   71,    0,    0,    0,   57,   10,   41,
   90,   89,   84,   85,    0,   75,    0,   66,   67,   69,
   72,    0,   91,    0,   87,   78,    0,    0,   65,    0,
    0,   76,    0,    0,   92,    0,    0,
  };
  protected static readonly short [] yyDgoto  = {             9,
   10,   11,   68,   12,   29,   14,   15,   16,   17,   18,
   19,   20,   21,   22,   23,   24,   25,   26,   42,   27,
   28,   60,   61,   31,   65,   96,   97,   32,   91,   92,
  113,  114,  115,  139,  134,  104,  105,
  };
  protected static readonly short [] yySindex = {          223,
    0,    0,    0,   -4,  223,    0,    0,    0,    0,    0,
   -2,    0,  -49,   -4,    0,   -7,  -11,    0,   -9, -221,
    0,    0,    0, -264,  -59,    0,    0, -214,    0, -291,
  -39,    0,   16,  -19, -220,    0,    0,    0,    0,    0,
    0,   -4,    0,   -4,   -4,   -4,   -4,   -4,   -4,   -4,
   -4,   -4,   -4,   -4,   -4,   -4,  223,   -4, -233, -222,
    0, -160, -156,   -4, -256,    0,    0,  -21,    0,    0,
    0,    0,    0,    0,   -7,   -7,    0,    0,    0,    0,
   -9,   -9,    0, -264,   45,  -44,   -4,    0,    0,  -35,
    0, -212,    0,   -4, -204, -211,    0,    0,   -4,   -4,
   -4,   -4, -203,   73,    0,    0, -128, -126, -191, -189,
 -180,    0, -212,    0, -156, -127,   -4,    0,    0,    0,
    0,    0,    0,    0,   -4,    0, -247,    0,    0,    0,
    0, -178,    0,  -41,    0,    0, -110,   -4,    0,   -4,
   -4,    0,   19, -127,    0,   -4,   -9,
  };
  protected static readonly short [] yyRindex = {            0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   33,    0,  514,    0,    0,   92,  174,    0,  207,   63,
    0,    0,    0,  139,   58,    0,    0,    9,    0,  -37,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   13,
    0,    0,    0,    0,   64,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  115,  151,    0,    0,    0,    0,
  330,  479,    0,  231,    0,   31,    0,    0,    0,    1,
    0,   83,    0,    0,    0,   84,    0,    0,    0,    0,
    0,    0,    8,   28,    0,    0,    0,    0,    0,    0,
    0,    0,  201,    0,    0,    2,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   15,    0,    0,    0,
    0,    0,    0,  140,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  145,    0,    0,  187,
  };
  protected static readonly short [] yyGindex = {           11,
    0,    0,    0,  -17,  148,    0,   74,   80,    0,  477,
   -8,    0,    0,    0,   94,  101,  -75,   23,    0,    0,
    0,    0,  102,    0,    0,    0,   65,   99,    0,   49,
    0,   52,    0,    0,    0,    0,   41,
  };
  protected static readonly short [] yyTable = {           102,
   73,   79,  141,   57,   64,   55,   58,   83,   50,  136,
  108,   41,   51,    8,   74,   33,   69,   62,  116,   98,
    5,   67,   99,    6,   71,    7,   30,   82,    8,   46,
   88,   47,   11,   48,   44,    5,   70,   34,    6,   45,
    7,   73,   79,   35,   81,   82,   53,   54,   83,   50,
   51,   83,   52,   51,   94,   74,   66,   40,   73,   79,
   95,  137,   33,   54,  144,   83,   50,   85,   82,   11,
   51,   88,   74,   11,   11,   11,   11,   11,   51,   11,
   52,  119,   63,   55,   87,   82,   30,   43,   88,   58,
   11,   21,   11,   11,   11,   11,   89,   58,   40,   59,
   90,   40,  100,   33,   54,   95,   33,  109,  110,  111,
  112,  123,  124,  117,   22,   40,  125,   72,   73,   74,
   33,   54,  120,   63,   55,   33,   75,   76,  126,  143,
  127,  128,   21,  129,   21,   21,   21,  147,   38,   80,
   63,   55,  130,   56,   81,  138,  142,   13,   83,   21,
   23,   21,   13,   21,   21,   22,   84,   22,   22,   22,
  118,   88,   93,  132,  131,  135,    0,    0,    0,    0,
    0,    0,   22,   24,   22,    0,   22,   22,    0,   38,
   80,   13,   38,    0,    0,   81,   70,    0,    0,   13,
    0,   23,    0,   23,   23,   23,   38,   80,    0,    0,
   64,   38,   81,    0,   13,    0,   30,    0,   23,    0,
   23,   56,   23,   23,   24,    0,    0,   24,    0,    0,
    0,  106,   36,   37,   38,   39,   40,   70,    0,    0,
   39,   24,    0,   24,    0,   24,   24,    1,    2,    3,
    0,   64,    0,    0,   70,    0,   13,   30,    0,    0,
   30,    0,    1,    2,    3,    8,   49,   50,   64,    0,
    0,    0,    5,    0,   30,    6,    0,    7,  101,   30,
   63,   39,   58,  107,   39,    0,    0,  140,    0,    0,
    0,    0,    0,    0,   49,   50,  146,    0,   39,    0,
    0,    0,    0,   39,    0,    0,    0,    0,   11,   11,
   11,   11,   11,   11,   11,   11,   11,   11,   11,    0,
    0,   73,   73,   79,   73,   79,    0,   73,   79,   83,
   73,   73,   73,   73,   73,   74,   74,    0,   74,   31,
    0,   74,   33,   33,   74,   74,   74,   74,   74,   82,
    0,   11,   11,   11,   11,   11,   11,   11,   11,   11,
    0,   11,   11,   11,   11,   11,    0,   21,   21,   21,
   21,   21,   21,    0,    0,    0,   40,   40,    0,    0,
   31,   33,   33,   31,   33,   54,   33,   54,    0,   33,
   22,   22,   22,   22,   22,   22,    0,   31,    0,    0,
    0,    0,   31,   63,   63,   55,   63,   55,    0,   63,
   21,   21,   21,   21,   21,   21,   21,   21,   21,   38,
   21,   21,   21,   21,   21,    0,   23,   23,   23,   23,
   23,   23,    0,   22,   22,   22,   22,   22,   22,   22,
   22,   22,    0,   22,   22,   22,   22,   22,    0,   24,
   24,   24,   24,   24,   24,    0,    0,   38,   38,    0,
   38,   80,   38,   80,    0,   38,   81,    0,   81,   23,
   23,   23,   23,   23,   23,   23,   23,   23,    0,   23,
   23,   23,   23,   23,   30,   30,   30,   30,   32,    1,
    2,    3,   24,   24,   24,   24,   24,   24,   24,   24,
   24,    0,   24,   24,   24,   24,   24,   70,   70,    0,
   70,   39,    0,   70,    0,    0,   70,   70,   70,   70,
    0,   64,   64,   16,   64,   30,   30,   64,   30,   32,
   30,    0,   32,   30,    0,   77,   78,   79,   80,    0,
    4,    0,    0,    0,   86,    0,   32,    0,    0,   39,
   39,   32,   39,    0,   39,    0,    0,   39,    0,    0,
   16,    0,    0,    0,   16,   16,   16,   16,   16,    0,
   16,    0,    0,  103,    0,    0,    0,    0,    0,    0,
    0,   16,    0,   16,    0,   16,   16,  121,  122,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  133,    0,    0,    0,   31,   31,   31,
   31,  103,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  145,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   31,   31,
    0,   31,    0,   31,    0,    0,   31,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   32,   32,   32,   32,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   16,
   16,   16,   16,   16,   16,    0,    0,   32,   32,    0,
   32,    0,   32,    0,    0,   32,
  };
  protected static readonly short [] yyCheck = {            44,
    0,    0,   44,   63,   44,  270,   44,    0,    0,  257,
   46,   61,    0,   33,    0,    5,   34,  309,   94,   41,
   40,   41,   44,   43,   42,   45,    4,    0,   33,   37,
    0,   43,    0,   45,   42,   40,  257,   40,   43,   47,
   45,   41,   41,   46,   53,   54,  268,  269,   41,   41,
   60,   44,   62,   41,  311,   41,   41,    0,   58,   58,
  317,  309,    0,    0,  140,   58,   58,   57,   41,   37,
   58,   41,   58,   41,   42,   43,   44,   45,   60,   47,
   62,   99,    0,    0,  318,   58,   64,   14,   58,  312,
   58,    0,   60,   61,   62,   63,  257,  312,   41,  314,
  257,   44,   58,   41,   41,  317,   44,  320,  321,  322,
  323,  315,  316,  318,    0,   58,   44,   44,   45,   46,
   58,   58,  100,   41,   41,   63,   47,   48,  257,  138,
  257,  323,   41,  323,   43,   44,   45,  146,    0,    0,
   58,   58,  323,  271,    0,  324,  257,    0,   55,   58,
    0,   60,    5,   62,   63,   41,   56,   43,   44,   45,
   96,   60,   64,  115,  113,  125,   -1,   -1,   -1,   -1,
   -1,   -1,   58,    0,   60,   -1,   62,   63,   -1,   41,
   41,   34,   44,   -1,   -1,   41,    0,   -1,   -1,   42,
   -1,   41,   -1,   43,   44,   45,   58,   58,   -1,   -1,
    0,   63,   58,   -1,   57,   -1,    0,   -1,   58,   -1,
   60,  271,   62,   63,   41,   -1,   -1,   44,   -1,   -1,
   -1,  257,  272,  273,  274,  275,  276,   41,   -1,   -1,
    0,   58,   -1,   60,   -1,   62,   63,  257,  258,  259,
   -1,   41,   -1,   -1,   58,   -1,   99,   41,   -1,   -1,
   44,   -1,  257,  258,  259,   33,  266,  267,   58,   -1,
   -1,   -1,   40,   -1,   58,   43,   -1,   45,  313,   63,
  310,   41,  310,  309,   44,   -1,   -1,  319,   -1,   -1,
   -1,   -1,   -1,   -1,  266,  267,  268,   -1,   58,   -1,
   -1,   -1,   -1,   63,   -1,   -1,   -1,   -1,  266,  267,
  268,  269,  270,  271,  272,  273,  274,  275,  276,   -1,
   -1,  311,  312,  312,  314,  314,   -1,  317,  317,  312,
  320,  321,  322,  323,  324,  311,  312,   -1,  314,    0,
   -1,  317,  270,  271,  320,  321,  322,  323,  324,  312,
   -1,  309,  310,  311,  312,  313,  314,  315,  316,  317,
   -1,  319,  320,  321,  322,  323,   -1,  266,  267,  268,
  269,  270,  271,   -1,   -1,   -1,  309,  310,   -1,   -1,
   41,  309,  310,   44,  312,  312,  314,  314,   -1,  317,
  266,  267,  268,  269,  270,  271,   -1,   58,   -1,   -1,
   -1,   -1,   63,  311,  312,  312,  314,  314,   -1,  317,
  309,  310,  311,  312,  313,  314,  315,  316,  317,  271,
  319,  320,  321,  322,  323,   -1,  266,  267,  268,  269,
  270,  271,   -1,  309,  310,  311,  312,  313,  314,  315,
  316,  317,   -1,  319,  320,  321,  322,  323,   -1,  266,
  267,  268,  269,  270,  271,   -1,   -1,  309,  310,   -1,
  312,  312,  314,  314,   -1,  317,  312,   -1,  314,  309,
  310,  311,  312,  313,  314,  315,  316,  317,   -1,  319,
  320,  321,  322,  323,  268,  269,  270,  271,    0,  257,
  258,  259,  309,  310,  311,  312,  313,  314,  315,  316,
  317,   -1,  319,  320,  321,  322,  323,  311,  312,   -1,
  314,  271,   -1,  317,   -1,   -1,  320,  321,  322,  323,
   -1,  311,  312,    0,  314,  309,  310,  317,  312,   41,
  314,   -1,   44,  317,   -1,   49,   50,   51,   52,   -1,
  308,   -1,   -1,   -1,   58,   -1,   58,   -1,   -1,  309,
  310,   63,  312,   -1,  314,   -1,   -1,  317,   -1,   -1,
   37,   -1,   -1,   -1,   41,   42,   43,   44,   45,   -1,
   47,   -1,   -1,   87,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   58,   -1,   60,   -1,   62,   63,  101,  102,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  117,   -1,   -1,   -1,  268,  269,  270,
  271,  125,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  141,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  309,  310,
   -1,  312,   -1,  314,   -1,   -1,  317,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  268,  269,  270,  271,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  266,
  267,  268,  269,  270,  271,   -1,   -1,  309,  310,   -1,
  312,   -1,  314,   -1,   -1,  317,
  };

#line 513 "Parser.jay"

}

#line default
namespace yydebug {
        using System;
	 internal interface yyDebug {
		 void push (int state, Object value);
		 void lex (int state, int token, string name, Object value);
		 void shift (int from, int to, int errorFlag);
		 void pop (int state);
		 void discard (int state, int token, string name, Object value);
		 void reduce (int from, int to, int rule, string text, int len);
		 void shift (int from, int to);
		 void accept (Object value);
		 void error (string message);
		 void reject ();
	 }
	 
	 class yyDebugSimple : yyDebug {
		 void println (string s){
			 Console.Error.WriteLine (s);
		 }
		 
		 public void push (int state, Object value) {
			 println ("push\tstate "+state+"\tvalue "+value);
		 }
		 
		 public void lex (int state, int token, string name, Object value) {
			 println("lex\tstate "+state+"\treading "+name+"\tvalue "+value);
		 }
		 
		 public void shift (int from, int to, int errorFlag) {
			 switch (errorFlag) {
			 default:				// normally
				 println("shift\tfrom state "+from+" to "+to);
				 break;
			 case 0: case 1: case 2:		// in error recovery
				 println("shift\tfrom state "+from+" to "+to
					     +"\t"+errorFlag+" left to recover");
				 break;
			 case 3:				// normally
				 println("shift\tfrom state "+from+" to "+to+"\ton error");
				 break;
			 }
		 }
		 
		 public void pop (int state) {
			 println("pop\tstate "+state+"\ton error");
		 }
		 
		 public void discard (int state, int token, string name, Object value) {
			 println("discard\tstate "+state+"\ttoken "+name+"\tvalue "+value);
		 }
		 
		 public void reduce (int from, int to, int rule, string text, int len) {
			 println("reduce\tstate "+from+"\tuncover "+to
				     +"\trule ("+rule+") "+text);
		 }
		 
		 public void shift (int from, int to) {
			 println("goto\tfrom state "+from+" to "+to);
		 }
		 
		 public void accept (Object value) {
			 println("accept\tvalue "+value);
		 }
		 
		 public void error (string message) {
			 println("error\t"+message);
		 }
		 
		 public void reject () {
			 println("reject");
		 }
		 
	 }
}
// %token constants
 class Token {
  public const int IDENTIFIER = 257;
  public const int CONSTANT = 258;
  public const int STRING_LITERAL = 259;
  public const int SIZEOF = 260;
  public const int PTR_OP = 261;
  public const int INC_OP = 262;
  public const int DEC_OP = 263;
  public const int LEFT_OP = 264;
  public const int RIGHT_OP = 265;
  public const int LE_OP = 266;
  public const int GE_OP = 267;
  public const int EQ_OP = 268;
  public const int NE_OP = 269;
  public const int AND_OP = 270;
  public const int OR_OP = 271;
  public const int MUL_ASSIGN = 272;
  public const int DIV_ASSIGN = 273;
  public const int MOD_ASSIGN = 274;
  public const int ADD_ASSIGN = 275;
  public const int SUB_ASSIGN = 276;
  public const int LEFT_ASSIGN = 277;
  public const int RIGHT_ASSIGN = 278;
  public const int AND_ASSIGN = 279;
  public const int XOR_ASSIGN = 280;
  public const int OR_ASSIGN = 281;
  public const int TYPE_NAME = 282;
  public const int TYPEDEF = 283;
  public const int EXTERN = 284;
  public const int STATIC = 285;
  public const int AUTO = 286;
  public const int REGISTER = 287;
  public const int INLINE = 288;
  public const int RESTRICT = 289;
  public const int CHAR = 290;
  public const int SHORT = 291;
  public const int INT = 292;
  public const int LONG = 293;
  public const int SIGNED = 294;
  public const int UNSIGNED = 295;
  public const int FLOAT = 296;
  public const int DOUBLE = 297;
  public const int CONST = 298;
  public const int VOLATILE = 299;
  public const int VOID = 300;
  public const int BOOL = 301;
  public const int COMPLEX = 302;
  public const int IMAGINARY = 303;
  public const int STRUCT = 304;
  public const int UNION = 305;
  public const int ENUM = 306;
  public const int ELLIPSIS = 307;
  public const int SELECT = 308;
  public const int AS = 309;
  public const int FROM = 310;
  public const int WHERE = 311;
  public const int LIMIT = 312;
  public const int OFFSET = 313;
  public const int ORDER = 314;
  public const int ASC = 315;
  public const int DESC = 316;
  public const int GROUP = 317;
  public const int BY = 318;
  public const int HAVING = 319;
  public const int LEFT = 320;
  public const int INNER = 321;
  public const int CROSS = 322;
  public const int JOIN = 323;
  public const int ON = 324;
  public const int CASE = 325;
  public const int DEFAULT = 326;
  public const int IF = 327;
  public const int ELSE = 328;
  public const int SWITCH = 329;
  public const int WHILE = 330;
  public const int DO = 331;
  public const int FOR = 332;
  public const int GOTO = 333;
  public const int CONTINUE = 334;
  public const int BREAK = 335;
  public const int RETURN = 336;
  public const int yyErrorCode = 256;
 }
 namespace yyParser {
  using System;
  /** thrown for irrecoverable syntax errors and stack overflow.
    */
  internal class yyException : System.Exception {
    public yyException (string message) : base (message) {
    }
  }
  internal class yyUnexpectedEof : yyException {
    public yyUnexpectedEof (string message) : base (message) {
    }
    public yyUnexpectedEof () : base ("") {
    }
  }

  /** must be implemented by a scanner object to supply input to the parser.
    */
  internal interface yyInput {
    /** move on to next token.
        @return false if positioned beyond tokens.
        @throws IOException on input error.
      */
    bool advance (); // throws java.io.IOException;
    /** classifies current token.
        Should not be called if advance() returned false.
        @return current %token or single character.
      */
    int token ();
    /** associated with current token.
        Should not be called if advance() returned false.
        @return value for token().
      */
    Object value ();
  }
 }
} // close outermost namespace, that MUST HAVE BEEN opened in the prolog
