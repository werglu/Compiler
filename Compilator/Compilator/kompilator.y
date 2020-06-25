
// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex
%using compiler;
%namespace GardensPoint

%{
	public Program MyProgram {get; set;}	
%}

%union
{
public string  val;
public char    type;
public VarType varType; 
public List<Declarations> list;
public Declarations listNode;
public Number num;
public Expression expr;
public Statement stat;
public List<Statement> statementsList;
}

%token Program If Else While Read Write Return Int Double Bool True False Assign Or And LogicalOr LogicalAnd Equal NotEqual GreatherThan GreatherThanOrEqual LessThan LessThanOrEqual Plus Minus Multiplies Divides LogicalNegation BitwiseNegation OpenPar ClosePar OpenBraces CloseBraces Eof Semicolon Error

%token <val> Ident IntNumber RealNumber String Comment

%type <list> declarations
%type <listNode> declaration
%type <num> number
%type <expr> operation bitwiseoperation expresion exp exp2 exp3 term
%type <val> simpleoperation
%type <stat> statement1
%type <statementsList> statement

%%

start     : line { Console.WriteLine("start");  YYACCEPT; }
          ;

line      : Program OpenBraces end { MyProgram = new Program(new List<Declarations>());  }
          | Program OpenBraces declarations end	{ MyProgram = new Program($3);  }
		  | Program OpenBraces declarations statement end { MyProgram = new Program($3, $4);  }
		  | Program OpenBraces statement end { MyProgram = new Program($3); }
		  | error 
			{ 
				Console.WriteLine("  line {0,3}:  syntax error",sc.lineno);
				Settings.errors++;
				yyerrok(); 
				YYABORT;
			}
          ;

end   : CloseBraces Eof {  YYACCEPT;  }
      | CloseBraces Error
	  {
	  	Console.WriteLine("  line {0,3}:  syntax error",sc.lineno);
		Settings.errors++;
		yyerrok(); 
	  }
	  | Error 
	  { 
	  	Console.WriteLine("  line {0,3}:  syntax error",sc.lineno);
		Settings.errors++;
		yyerrok(); 
	  }
	  ;

declarations  : declaration declarations { $2.Add($1); $$ = $2; }
              | declaration { $$ = new List<Declarations>{$1};  }
			  ;

declaration : Bool Ident Semicolon { $$ = new Declarations($2, 2); }	
			| Int Ident Semicolon { $$ = new Declarations($2, 0); }
			| Double Ident Semicolon { $$ = new Declarations($2, 1); }
			;

statement : statement1 statement {  $2.Add($1); $$ = $2; }
		  | statement1 { $$ = new List<Statement>{$1};  }
		  ;

statement1 : OpenBraces statement1 CloseBraces { $$ = new StatementStatement($2);  }
		  | OpenBraces CloseBraces  { $$ = new EmptyStatement(); }
		  | While OpenPar expresion ClosePar statement1 { $$ = new WhileStatement($3, $5);  }
		  | Return Semicolon  { $$ = new EmptyStatement(); }
		  | Semicolon { $$ = new EmptyStatement(); }
		  | Write expresion Semicolon {  $$ = new WriteStatement($2);  } 
		  | Write String Semicolon {  $$ = new WriteStatement($2);  }
		  | If OpenPar expresion ClosePar statement1 { $$ = new IfStatement($3, $5);  }
		  | If OpenPar expresion ClosePar statement1 Else statement1 {  $$ = new IfElseStatement($3, $5, $7);  }
		  | expresion Semicolon { $$ = new StatementStatement($1); }
		  | Read Ident Semicolon { $$ = new ReadStatement(new Number($2)); }
		  ;


expresion : Ident Assign exp { $$ = new ExpresionOperation(new Number($1), $3, "Assign"); }
		  | exp { $$ = $1; }
		  ;

exp       : exp Or exp2 { $$ = new ExpresionOperation($1, $3, "Or"); }
		  | exp And exp2 { $$ = new ExpresionOperation($1, $3, "And"); }
		  | exp2 { $$ = $1; }
		  ;

exp2      : exp2 Equal exp3 { $$ = new ExpresionOperation($1, $3, "Equal"); }
          | exp2 NotEqual exp3 { $$ = new ExpresionOperation($1, $3, "NotEqual"); }
		  | exp2 GreatherThan exp3 { $$ = new ExpresionOperation($1, $3, "GreatherThan"); }
		  | exp2 GreatherThanOrEqual exp3 { $$ = new ExpresionOperation($1, $3, "GreatherThanOrEqual"); }
		  | exp2 LessThan exp3 { $$ = new ExpresionOperation($1, $3, "LessThan"); }
		  | exp2 LessThanOrEqual exp3 { $$ = new ExpresionOperation($1, $3, "LessThanOrEqual"); }
		  | exp3 { $$ = $1; }
          ;

exp3      : exp3 Plus term { $$ = new ExpresionOperation($1, $3, "Plus"); }
          | exp3 Minus term  { $$ = new ExpresionOperation($1, $3, "Minus"); }
          | term { $$ = $1; }
          ;


term      : term Multiplies bitwiseoperation { $$ = new ExpresionOperation($1, $3, "Multiplies"); }
          | term Divides bitwiseoperation { $$ = new ExpresionOperation($1, $3, "Divides"); }
          | bitwiseoperation { $$ = $1; }
          ;

bitwiseoperation : bitwiseoperation LogicalOr operation { $$ = new ExpresionOperation($1, $3, "LogicalOr"); }
                 | bitwiseoperation LogicalAnd operation { $$ = new ExpresionOperation($1, $3, "LogicalAnd"); }
				 | operation { $$ = $1; }
                 ;


operation  : OpenPar exp ClosePar {  }
		   | simpleoperation operation {  $$ = new ExpresionOperation(null, $2, $1); }
           | number { $$ = $1; }
		   ;

number     : IntNumber { $$ = new Number("0", $1); }
           | RealNumber { $$ = new Number("1", $1); }
           | Ident { $$ = new Number($1); }
		   | True { $$ = new Number("2", "1"); }
		   | False { $$ = new Number("2", "0"); }
		   ;

simpleoperation  : LogicalNegation { $$ = "LogicalNegation";  }
                 | BitwiseNegation { $$ = "BitwiseNegation"; }
				 | Minus { $$ = "Minus"; }
				 | OpenPar Bool ClosePar { $$ = "BoolConversion"; }
				 | OpenPar Int ClosePar { $$ = "IntConversion"; }
				 | OpenPar Double ClosePar { $$ = "DoubleConversion"; }
		         ;

err    : EOF { YYABORT; };

%%

public Scanner sc;

public Parser(Scanner scanner) : base(scanner) 
{ 
	sc = scanner;
}
