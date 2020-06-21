
// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace GardensPoint

%union
{
public string  val;
public char    type;
}

%token Program If Else While Read Write Return Int Double Bool True False Assign Or And LogicalOr LogicalAnd Equal NotEqual GreatherThan GreatherThanOrEqual LessThan LessThanOrEqual Plus Minus Multiplies Divides LogicalNegation BitwiseNegation OpenPar ClosePar OpenBraces CloseBraces Endl Eof Semicolon 
%token <val> Ident IntNumber RealNumber String Comment
%type <type> line 

%%

start     : line { ++lineno; }
          ;

line      : Program OpenBraces CloseBraces 
			{
               Console.WriteLine("Ok");
			}
          | Program OpenBraces statement CloseBraces	   
          ;

statement : typeSpecifier Ident Semicolon
		  | OpenBraces statement CloseBraces
		  | While OpenPar expresion ClosePar statement
		  | Return Semicolon
		  | Ident Assign expresion 
		  | Semicolon
		  | Write expresion Semicolon
		  | Write String Semicolon
		  | If OpenPar expresion ClosePar statement
		  | If OpenPar expresion ClosePar statement Else statement
		  | exp relationOperator exp
		  | statement statement
		  | expresion Semicolon
		  | Read Ident Semicolon
		  ;

typeSpecifier : Int
			  | Double
			  | Bool
			  ;


expresion : Ident Assign exp1
		  | exp1
		  ;

exp1      : exp1 Or exp2
		  | exp1 And exp2
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

exp3      : exp Plus term
          | exp Minus term
          | term
          ;

term      : term Multiplies bitwiseoperation
          | term Divides bitwiseoperation
          | bitwiseoperation
          ;

bitwiseoperation : bitwiseoperation LogicalOr operation
                 | bitwiseoperation LogicalAnd operation
				 | operation
                 ;

operation  : OpenPar expresion ClosePar
		  | simpleoperation operation
          | IntNumber
          | RealNumber
          | Ident
		  ;

simpleoperation  : LogicalNegation
           | BitwiseNegation
		   | Minus
		   ;

%%

int lineno=1;

public Parser(Scanner scanner) : base(scanner) { }

