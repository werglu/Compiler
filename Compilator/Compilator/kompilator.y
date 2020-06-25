
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
}

%token Program If Else While Read Write Return Int Double Bool True False Assign Or And LogicalOr LogicalAnd Equal NotEqual GreatherThan GreatherThanOrEqual LessThan LessThanOrEqual Plus Minus Multiplies Divides LogicalNegation BitwiseNegation OpenPar ClosePar OpenBraces CloseBraces Eof Semicolon Error

%token <val> Ident IntNumber RealNumber String Comment

%type <list> declarations
%type <listNode> declaration
%type <num> number
%type <expr> operation
%type <val> simpleoperation


%%

start     : line { Console.WriteLine("start");  YYACCEPT; }
          ;

line      : Program OpenBraces end { MyProgram = new Program(new List<Declarations>());  }
          | Program OpenBraces declarations end	{ MyProgram = new Program($3);  }
		  | Program OpenBraces declarations statement end { MyProgram = new Program($3);  }
		  | Program OpenBraces statement end { MyProgram = new Program(new List<Declarations>()); }
		  | error 
			{ 
				Console.WriteLine("  line {0,3}:  syntax error",sc.linen);
				Settings.errors++;
				yyerrok(); 
				YYABORT;
			}
          ;

end   : CloseBraces Eof {  YYACCEPT;  }
      | CloseBraces Error
	  {
	  	Console.WriteLine("  line {0,3}:  syntax error",sc.linen);
		Settings.errors++;
		yyerrok(); 
		YYABORT;
	  }
	  | Error 
	  { 
	  	Console.WriteLine("  line {0,3}:  syntax error",sc.linen);
		Settings.errors++;
		yyerrok(); 
		YYABORT;
	  }
	  ;

declarations  : declaration declarations { $2.Add($1); $$ = $2; }
              | declaration { $$ = new List<Declarations>{$1};  }
			  ;

declaration : Bool Ident Semicolon { $$ = new Declarations($2, 2); }	
			| Int Ident Semicolon { $$ = new Declarations($2, 0); }
			| Double Ident Semicolon { $$ = new Declarations($2, 1); }
			;

statement : statement1 statement
		  | statement1 
		  ;

statement1 : OpenBraces statement1 CloseBraces
		  | OpenBraces CloseBraces
		  | While OpenPar expresion ClosePar statement1
		  | Return Semicolon
		  | Semicolon
		  | Write expresion Semicolon
		  | Write String Semicolon { }
		  | If OpenPar expresion ClosePar statement1
		  | If OpenPar expresion ClosePar statement1 Else statement1
		  | expresion Semicolon
		  | Read Ident Semicolon
		  ;


expresion : Ident Assign exp 
		  | exp
		  ;

exp       : exp Or exp2
		  | exp And exp2
		  | exp2
		  ;

exp2      : exp2 Equal exp3
          | exp2 NotEqual exp3
		  | exp2 GreatherThan exp3
		  | exp2 GreatherThanOrEqual exp3
		  | exp2 LessThan exp3
		  | exp2 LessThanOrEqual exp3
		  | exp3
          ;

exp3      : exp3 Plus term
          | exp3 Minus term
          | term
          ;


term      : term Multiplies bitwiseoperation
          | term Divides bitwiseoperation
          | bitwiseoperation
          ;

bitwiseoperation : bitwiseoperation LogicalOr operation { }
                 | bitwiseoperation LogicalAnd operation
				 | operation
                 ;


operation  : OpenPar exp ClosePar {  }
		   | simpleoperation operation { }
           | number {}
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
 int lineno=1;

public Scanner sc;

public Parser(Scanner scanner) : base(scanner) 
{ 
	sc = scanner;
}
