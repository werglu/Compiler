%using QUT.Gppg;
%using compiler;
%namespace GardensPoint

%{
	public int lineno = 1;
%}

Comment		\/\/[^\n\r]*
IntNumber   ("0"|([1-9][0-9]*))
RealNumber  (("0"|([1-9][0-9]*))\.[0-9]+)
Ident       [a-zA-Z]+[a-zA-Z0-9]*
String		\"([^\\\"\n]|\\.)*\"

%%

"program"     { return (int)Tokens.Program; }
"if"          { return (int)Tokens.If; }
"else"        { return (int)Tokens.Else; }
"while"       { return (int)Tokens.While; }
"read"        { return (int)Tokens.Read; }
"write"       { return (int)Tokens.Write; }
"return"      { return (int)Tokens.Return; }
"int"         { return (int)Tokens.Int; }
"double"      { return (int)Tokens.Double; }
"bool"        { return (int)Tokens.Bool; }
"true"        { return (int)Tokens.True; }
"false"       { return (int)Tokens.False; }
{Ident}       { yylval.val=yytext; return (int)Tokens.Ident; }
{IntNumber}   { yylval.val=yytext; return (int)Tokens.IntNumber; }
{RealNumber}  { yylval.val=yytext; return (int)Tokens.RealNumber; }
{String}	  { yylval.val=yytext; return (int)Tokens.String; } 
{Comment}     { }
"="           { return (int)Tokens.Assign; }
"||"          { return (int)Tokens.Or; }
"&&"          { return (int)Tokens.And; }
"|"           { return (int)Tokens.LogicalOr; }
"&"           { return (int)Tokens.LogicalAnd; }
"=="          { return (int)Tokens.Equal; }
"!="          { return (int)Tokens.NotEqual; }
">"           { return (int)Tokens.GreatherThan; }
">="          { return (int)Tokens.GreatherThanOrEqual; }
"<"           { return (int)Tokens.LessThan; }
"<="          { return (int)Tokens.LessThanOrEqual; }
"+"           { return (int)Tokens.Plus; }
"-"           { return (int)Tokens.Minus; }
"*"           { return (int)Tokens.Multiplies; }
"/"           { return (int)Tokens.Divides; }
"!"           { return (int)Tokens.LogicalNegation; }
"~"           { return (int)Tokens.BitwiseNegation; }
"("           { return (int)Tokens.OpenPar; }
")"           { return (int)Tokens.ClosePar; }
"{"           { return (int)Tokens.OpenBraces; }
"}"           { return (int)Tokens.CloseBraces; }
";"           { return (int)Tokens.Semicolon; }
" "           { }
"\t"          { }
"\r"          {  }
"\n"          { lineno++; }
<<EOF>>       { return (int)Tokens.Eof; }
.             { return (int)Tokens.Error; }


