// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  WG
// DateTime: 30.06.2020 14:15:29
// UserName: HP
// Input file <E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y - 29.06.2020 11:13:58>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using compiler;

namespace GardensPoint
{
public enum Tokens {error=2,EOF=3,Program=4,If=5,Else=6,
    While=7,Read=8,Write=9,Return=10,Int=11,Double=12,
    Bool=13,True=14,False=15,Assign=16,Or=17,And=18,
    LogicalOr=19,LogicalAnd=20,Equal=21,NotEqual=22,GreatherThan=23,GreatherThanOrEqual=24,
    LessThan=25,LessThanOrEqual=26,Plus=27,Minus=28,Multiplies=29,Divides=30,
    LogicalNegation=31,BitwiseNegation=32,OpenPar=33,ClosePar=34,OpenBraces=35,CloseBraces=36,
    Eof=37,Semicolon=38,Error=39,Ident=40,IntNumber=41,RealNumber=42,
    String=43,Comment=44};

public struct ValueType
#line 11 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
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
#line default
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y - 29.06.2020 11:13:58
#line 7 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
	public Program MyProgram {get; set;}	
#line default
  // End verbatim content from E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y - 29.06.2020 11:13:58

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[64];
  private static State[] states = new State[115];
  private static string[] nonTerms = new string[] {
      "declarations", "declaration", "number", "operation", "bitwiseoperation", 
      "expresion", "exp", "exp2", "exp3", "term", "simpleoperation", "statement1", 
      "statement", "start", "$accept", "line", "end", "err", };

  static Parser() {
    states[0] = new State(new int[]{4,4,2,114},new int[]{-14,1,-16,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{35,5});
    states[5] = new State(new int[]{36,11,39,14,13,105,11,108,12,111,35,17,7,21,10,26,9,28,5,89,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74,8,98},new int[]{-17,6,-1,7,-13,101,-2,103,-12,15,-6,96,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[6] = new State(-3);
    states[7] = new State(new int[]{36,11,39,14,35,17,7,21,10,26,9,28,5,89,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74,8,98},new int[]{-17,8,-13,9,-12,15,-6,96,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[8] = new State(-4);
    states[9] = new State(new int[]{36,11,39,14},new int[]{-17,10});
    states[10] = new State(-5);
    states[11] = new State(new int[]{37,12,39,13});
    states[12] = new State(-8);
    states[13] = new State(-9);
    states[14] = new State(-10);
    states[15] = new State(new int[]{35,17,7,21,10,26,9,28,5,89,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74,8,98,36,-17,39,-17},new int[]{-13,16,-12,15,-6,96,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[16] = new State(-16);
    states[17] = new State(new int[]{36,20,35,17,7,21,10,26,9,28,5,89,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74,8,98},new int[]{-13,18,-12,15,-6,96,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[18] = new State(new int[]{36,19});
    states[19] = new State(-18);
    states[20] = new State(-19);
    states[21] = new State(new int[]{33,22});
    states[22] = new State(new int[]{40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74},new int[]{-6,23,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[23] = new State(new int[]{34,24});
    states[24] = new State(new int[]{35,17,7,21,10,26,9,28,5,89,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74,8,98},new int[]{-12,25,-6,96,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[25] = new State(-20);
    states[26] = new State(new int[]{38,27});
    states[27] = new State(-21);
    states[28] = new State(new int[]{43,31,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74},new int[]{-6,29,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[29] = new State(new int[]{38,30});
    states[30] = new State(-22);
    states[31] = new State(new int[]{38,32});
    states[32] = new State(-23);
    states[33] = new State(new int[]{16,34,19,-54,20,-54,29,-54,30,-54,27,-54,28,-54,21,-54,22,-54,23,-54,24,-54,25,-54,26,-54,17,-54,18,-54,38,-54,34,-54});
    states[34] = new State(new int[]{40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74},new int[]{-6,35,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[35] = new State(-28);
    states[36] = new State(new int[]{17,37,18,87,38,-29,34,-29});
    states[37] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-8,38,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[38] = new State(new int[]{21,39,22,55,23,78,24,80,25,82,26,84,17,-30,18,-30,38,-30,34,-30});
    states[39] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-9,40,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[40] = new State(new int[]{27,41,28,57,21,-33,22,-33,23,-33,24,-33,25,-33,26,-33,17,-33,18,-33,38,-33,34,-33});
    states[41] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-10,42,-5,76,-4,75,-11,63,-3,69});
    states[42] = new State(new int[]{29,43,30,59,27,-40,28,-40,21,-40,22,-40,23,-40,24,-40,25,-40,26,-40,17,-40,18,-40,38,-40,34,-40});
    states[43] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-5,44,-4,75,-11,63,-3,69});
    states[44] = new State(new int[]{19,45,20,61,29,-43,30,-43,27,-43,28,-43,21,-43,22,-43,23,-43,24,-43,25,-43,26,-43,17,-43,18,-43,38,-43,34,-43});
    states[45] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-4,46,-11,63,-3,69});
    states[46] = new State(-46);
    states[47] = new State(new int[]{11,50,12,52,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74},new int[]{-6,48,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[48] = new State(new int[]{34,49});
    states[49] = new State(-49);
    states[50] = new State(new int[]{34,51});
    states[51] = new State(-60);
    states[52] = new State(new int[]{34,53});
    states[53] = new State(-61);
    states[54] = new State(new int[]{21,39,22,55,23,78,24,80,25,82,26,84,17,-32,18,-32,38,-32,34,-32});
    states[55] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-9,56,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[56] = new State(new int[]{27,41,28,57,21,-34,22,-34,23,-34,24,-34,25,-34,26,-34,17,-34,18,-34,38,-34,34,-34});
    states[57] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-10,58,-5,76,-4,75,-11,63,-3,69});
    states[58] = new State(new int[]{29,43,30,59,27,-41,28,-41,21,-41,22,-41,23,-41,24,-41,25,-41,26,-41,17,-41,18,-41,38,-41,34,-41});
    states[59] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-5,60,-4,75,-11,63,-3,69});
    states[60] = new State(new int[]{19,45,20,61,29,-44,30,-44,27,-44,28,-44,21,-44,22,-44,23,-44,24,-44,25,-44,26,-44,17,-44,18,-44,38,-44,34,-44});
    states[61] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-4,62,-11,63,-3,69});
    states[62] = new State(-47);
    states[63] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-4,64,-11,63,-3,69});
    states[64] = new State(-50);
    states[65] = new State(-57);
    states[66] = new State(-58);
    states[67] = new State(-59);
    states[68] = new State(-62);
    states[69] = new State(-51);
    states[70] = new State(-52);
    states[71] = new State(-53);
    states[72] = new State(-54);
    states[73] = new State(-55);
    states[74] = new State(-56);
    states[75] = new State(-48);
    states[76] = new State(new int[]{19,45,20,61,29,-45,30,-45,27,-45,28,-45,21,-45,22,-45,23,-45,24,-45,25,-45,26,-45,17,-45,18,-45,38,-45,34,-45});
    states[77] = new State(new int[]{29,43,30,59,27,-42,28,-42,21,-42,22,-42,23,-42,24,-42,25,-42,26,-42,17,-42,18,-42,38,-42,34,-42});
    states[78] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-9,79,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[79] = new State(new int[]{27,41,28,57,21,-35,22,-35,23,-35,24,-35,25,-35,26,-35,17,-35,18,-35,38,-35,34,-35});
    states[80] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-9,81,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[81] = new State(new int[]{27,41,28,57,21,-36,22,-36,23,-36,24,-36,25,-36,26,-36,17,-36,18,-36,38,-36,34,-36});
    states[82] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-9,83,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[83] = new State(new int[]{27,41,28,57,21,-37,22,-37,23,-37,24,-37,25,-37,26,-37,17,-37,18,-37,38,-37,34,-37});
    states[84] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-9,85,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[85] = new State(new int[]{27,41,28,57,21,-38,22,-38,23,-38,24,-38,25,-38,26,-38,17,-38,18,-38,38,-38,34,-38});
    states[86] = new State(new int[]{27,41,28,57,21,-39,22,-39,23,-39,24,-39,25,-39,26,-39,17,-39,18,-39,38,-39,34,-39});
    states[87] = new State(new int[]{33,47,31,65,32,66,28,67,2,68,41,70,42,71,40,72,14,73,15,74},new int[]{-8,88,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[88] = new State(new int[]{21,39,22,55,23,78,24,80,25,82,26,84,17,-31,18,-31,38,-31,34,-31});
    states[89] = new State(new int[]{33,90});
    states[90] = new State(new int[]{40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74},new int[]{-6,91,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[91] = new State(new int[]{34,92});
    states[92] = new State(new int[]{35,17,7,21,10,26,9,28,5,89,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74,8,98},new int[]{-12,93,-6,96,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[93] = new State(new int[]{6,94,35,-24,7,-24,10,-24,9,-24,5,-24,40,-24,33,-24,31,-24,32,-24,28,-24,2,-24,41,-24,42,-24,14,-24,15,-24,8,-24,36,-24,39,-24});
    states[94] = new State(new int[]{35,17,7,21,10,26,9,28,5,89,40,33,33,47,31,65,32,66,28,67,2,68,41,70,42,71,14,73,15,74,8,98},new int[]{-12,95,-6,96,-7,36,-8,54,-9,86,-10,77,-5,76,-4,75,-11,63,-3,69});
    states[95] = new State(-25);
    states[96] = new State(new int[]{38,97});
    states[97] = new State(-26);
    states[98] = new State(new int[]{40,99});
    states[99] = new State(new int[]{38,100});
    states[100] = new State(-27);
    states[101] = new State(new int[]{36,11,39,14},new int[]{-17,102});
    states[102] = new State(-6);
    states[103] = new State(new int[]{13,105,11,108,12,111,36,-12,39,-12,35,-12,7,-12,10,-12,9,-12,5,-12,40,-12,33,-12,31,-12,32,-12,28,-12,2,-12,41,-12,42,-12,14,-12,15,-12,8,-12},new int[]{-1,104,-2,103});
    states[104] = new State(-11);
    states[105] = new State(new int[]{40,106});
    states[106] = new State(new int[]{38,107});
    states[107] = new State(-13);
    states[108] = new State(new int[]{40,109});
    states[109] = new State(new int[]{38,110});
    states[110] = new State(-14);
    states[111] = new State(new int[]{40,112});
    states[112] = new State(new int[]{38,113});
    states[113] = new State(-15);
    states[114] = new State(-7);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-15, new int[]{-14,3});
    rules[2] = new Rule(-14, new int[]{-16});
    rules[3] = new Rule(-16, new int[]{4,35,-17});
    rules[4] = new Rule(-16, new int[]{4,35,-1,-17});
    rules[5] = new Rule(-16, new int[]{4,35,-1,-13,-17});
    rules[6] = new Rule(-16, new int[]{4,35,-13,-17});
    rules[7] = new Rule(-16, new int[]{2});
    rules[8] = new Rule(-17, new int[]{36,37});
    rules[9] = new Rule(-17, new int[]{36,39});
    rules[10] = new Rule(-17, new int[]{39});
    rules[11] = new Rule(-1, new int[]{-2,-1});
    rules[12] = new Rule(-1, new int[]{-2});
    rules[13] = new Rule(-2, new int[]{13,40,38});
    rules[14] = new Rule(-2, new int[]{11,40,38});
    rules[15] = new Rule(-2, new int[]{12,40,38});
    rules[16] = new Rule(-13, new int[]{-12,-13});
    rules[17] = new Rule(-13, new int[]{-12});
    rules[18] = new Rule(-12, new int[]{35,-13,36});
    rules[19] = new Rule(-12, new int[]{35,36});
    rules[20] = new Rule(-12, new int[]{7,33,-6,34,-12});
    rules[21] = new Rule(-12, new int[]{10,38});
    rules[22] = new Rule(-12, new int[]{9,-6,38});
    rules[23] = new Rule(-12, new int[]{9,43,38});
    rules[24] = new Rule(-12, new int[]{5,33,-6,34,-12});
    rules[25] = new Rule(-12, new int[]{5,33,-6,34,-12,6,-12});
    rules[26] = new Rule(-12, new int[]{-6,38});
    rules[27] = new Rule(-12, new int[]{8,40,38});
    rules[28] = new Rule(-6, new int[]{40,16,-6});
    rules[29] = new Rule(-6, new int[]{-7});
    rules[30] = new Rule(-7, new int[]{-7,17,-8});
    rules[31] = new Rule(-7, new int[]{-7,18,-8});
    rules[32] = new Rule(-7, new int[]{-8});
    rules[33] = new Rule(-8, new int[]{-8,21,-9});
    rules[34] = new Rule(-8, new int[]{-8,22,-9});
    rules[35] = new Rule(-8, new int[]{-8,23,-9});
    rules[36] = new Rule(-8, new int[]{-8,24,-9});
    rules[37] = new Rule(-8, new int[]{-8,25,-9});
    rules[38] = new Rule(-8, new int[]{-8,26,-9});
    rules[39] = new Rule(-8, new int[]{-9});
    rules[40] = new Rule(-9, new int[]{-9,27,-10});
    rules[41] = new Rule(-9, new int[]{-9,28,-10});
    rules[42] = new Rule(-9, new int[]{-10});
    rules[43] = new Rule(-10, new int[]{-10,29,-5});
    rules[44] = new Rule(-10, new int[]{-10,30,-5});
    rules[45] = new Rule(-10, new int[]{-5});
    rules[46] = new Rule(-5, new int[]{-5,19,-4});
    rules[47] = new Rule(-5, new int[]{-5,20,-4});
    rules[48] = new Rule(-5, new int[]{-4});
    rules[49] = new Rule(-4, new int[]{33,-6,34});
    rules[50] = new Rule(-4, new int[]{-11,-4});
    rules[51] = new Rule(-4, new int[]{-3});
    rules[52] = new Rule(-3, new int[]{41});
    rules[53] = new Rule(-3, new int[]{42});
    rules[54] = new Rule(-3, new int[]{40});
    rules[55] = new Rule(-3, new int[]{14});
    rules[56] = new Rule(-3, new int[]{15});
    rules[57] = new Rule(-11, new int[]{31});
    rules[58] = new Rule(-11, new int[]{32});
    rules[59] = new Rule(-11, new int[]{28});
    rules[60] = new Rule(-11, new int[]{33,11,34});
    rules[61] = new Rule(-11, new int[]{33,12,34});
    rules[62] = new Rule(-11, new int[]{2});
    rules[63] = new Rule(-18, new int[]{3});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // start -> line
#line 37 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                 { Console.WriteLine("start");  YYAccept(); }
#line default
        break;
      case 3: // line -> Program, OpenBraces, end
#line 40 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { MyProgram = new Program(new List<Declarations>()); YYAccept(); }
#line default
        break;
      case 4: // line -> Program, OpenBraces, declarations, end
#line 41 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                { MyProgram = new Program(ValueStack[ValueStack.Depth-2].list); YYAccept();}
#line default
        break;
      case 5: // line -> Program, OpenBraces, declarations, statement, end
#line 42 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                    { MyProgram = new Program(ValueStack[ValueStack.Depth-3].list, ValueStack[ValueStack.Depth-2].statementsList); YYAccept();}
#line default
        break;
      case 6: // line -> Program, OpenBraces, statement, end
#line 43 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                       { MyProgram = new Program(ValueStack[ValueStack.Depth-2].statementsList); YYAccept();}
#line default
        break;
      case 7: // line -> error
#line 45 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
   { 
				Console.WriteLine("  line {0,3}:  syntax error", sc.lineno);
				Settings.errors++;
				yyerrok(); 
				YYAbort();
			}
#line default
        break;
      case 8: // end -> CloseBraces, Eof
#line 53 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                        {   }
#line default
        break;
      case 9: // end -> CloseBraces, Error
#line 55 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
   {
	  	Console.WriteLine("  line {0,3}:  syntax error", sc.lineno);
		Settings.errors++;
		yyerrok(); 
	  }
#line default
        break;
      case 10: // end -> Error
#line 61 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
   { 
	  	Console.WriteLine("  line {0,3}:  syntax error", sc.lineno);
		Settings.errors++;
		yyerrok(); 
	  }
#line default
        break;
      case 11: // declarations -> declaration, declarations
#line 68 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                         { ValueStack[ValueStack.Depth-1].list.Add(ValueStack[ValueStack.Depth-2].listNode); CurrentSemanticValue.list = ValueStack[ValueStack.Depth-1].list; }
#line default
        break;
      case 12: // declarations -> declaration
#line 69 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                            { CurrentSemanticValue.list = new List<Declarations>{ValueStack[ValueStack.Depth-1].listNode};  }
#line default
        break;
      case 13: // declaration -> Bool, Ident, Semicolon
#line 72 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { CurrentSemanticValue.listNode = new Declarations(ValueStack[ValueStack.Depth-2].val, 2, sc.lineno); }
#line default
        break;
      case 14: // declaration -> Int, Ident, Semicolon
#line 73 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                         { CurrentSemanticValue.listNode = new Declarations(ValueStack[ValueStack.Depth-2].val, 0, sc.lineno); }
#line default
        break;
      case 15: // declaration -> Double, Ident, Semicolon
#line 74 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                            { CurrentSemanticValue.listNode = new Declarations(ValueStack[ValueStack.Depth-2].val, 1, sc.lineno); }
#line default
        break;
      case 16: // statement -> statement1, statement
#line 77 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                 {  ValueStack[ValueStack.Depth-1].statementsList.Add(ValueStack[ValueStack.Depth-2].stat); CurrentSemanticValue.statementsList = ValueStack[ValueStack.Depth-1].statementsList; }
#line default
        break;
      case 17: // statement -> statement1
#line 78 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                 { CurrentSemanticValue.statementsList = new List<Statement>{ValueStack[ValueStack.Depth-1].stat};  }
#line default
        break;
      case 18: // statement1 -> OpenBraces, statement, CloseBraces
#line 81 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                              { CurrentSemanticValue.stat = new ListStatement(ValueStack[ValueStack.Depth-2].statementsList);  }
#line default
        break;
      case 19: // statement1 -> OpenBraces, CloseBraces
#line 82 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                              { CurrentSemanticValue.stat = new EmptyStatement(); }
#line default
        break;
      case 20: // statement1 -> While, OpenPar, expresion, ClosePar, statement1
#line 83 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                  { CurrentSemanticValue.stat = new WhileStatement(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].stat);  }
#line default
        break;
      case 21: // statement1 -> Return, Semicolon
#line 84 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                        { CurrentSemanticValue.stat = new ReturnStatement(); }
#line default
        break;
      case 22: // statement1 -> Write, expresion, Semicolon
#line 85 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                {  CurrentSemanticValue.stat = new WriteStatement(ValueStack[ValueStack.Depth-2].expr);  }
#line default
        break;
      case 23: // statement1 -> Write, String, Semicolon
#line 86 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             {  CurrentSemanticValue.stat = new WriteStatement(ValueStack[ValueStack.Depth-2].val);  }
#line default
        break;
      case 24: // statement1 -> If, OpenPar, expresion, ClosePar, statement1
#line 87 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                               { CurrentSemanticValue.stat = new IfStatement(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].stat);  }
#line default
        break;
      case 25: // statement1 -> If, OpenPar, expresion, ClosePar, statement1, Else, statement1
#line 88 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                               {  CurrentSemanticValue.stat = new IfElseStatement(ValueStack[ValueStack.Depth-5].expr, ValueStack[ValueStack.Depth-3].stat, ValueStack[ValueStack.Depth-1].stat);  }
#line default
        break;
      case 26: // statement1 -> expresion, Semicolon
#line 89 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                          { CurrentSemanticValue.stat = new StatementStatement(ValueStack[ValueStack.Depth-2].expr); }
#line default
        break;
      case 27: // statement1 -> Read, Ident, Semicolon
#line 90 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                           { CurrentSemanticValue.stat = new ReadStatement(new Number(ValueStack[ValueStack.Depth-2].val, sc.lineno)); }
#line default
        break;
      case 28: // expresion -> Ident, Assign, expresion
#line 94 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { CurrentSemanticValue.expr = new ExpresionOperation(new Number(ValueStack[ValueStack.Depth-3].val, sc.lineno), ValueStack[ValueStack.Depth-1].expr, "Assign", sc.lineno); }
#line default
        break;
      case 29: // expresion -> exp
#line 95 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
          { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 30: // exp -> exp, Or, exp2
#line 98 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                        { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Or", sc.lineno); }
#line default
        break;
      case 31: // exp -> exp, And, exp2
#line 99 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                   { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "And", sc.lineno); }
#line default
        break;
      case 32: // exp -> exp2
#line 100 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
           { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 33: // exp2 -> exp2, Equal, exp3
#line 103 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                            { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Equal", sc.lineno); }
#line default
        break;
      case 34: // exp2 -> exp2, NotEqual, exp3
#line 104 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                               { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "NotEqual", sc.lineno); }
#line default
        break;
      case 35: // exp2 -> exp2, GreatherThan, exp3
#line 105 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "GreatherThan", sc.lineno); }
#line default
        break;
      case 36: // exp2 -> exp2, GreatherThanOrEqual, exp3
#line 106 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                    { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "GreatherThanOrEqual", sc.lineno); }
#line default
        break;
      case 37: // exp2 -> exp2, LessThan, exp3
#line 107 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                         { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "LessThan", sc.lineno); }
#line default
        break;
      case 38: // exp2 -> exp2, LessThanOrEqual, exp3
#line 108 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "LessThanOrEqual", sc.lineno); }
#line default
        break;
      case 39: // exp2 -> exp3
#line 109 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
           { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 40: // exp3 -> exp3, Plus, term
#line 112 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                           { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Plus", sc.lineno); }
#line default
        break;
      case 41: // exp3 -> exp3, Minus, term
#line 113 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Minus", sc.lineno); }
#line default
        break;
      case 42: // exp3 -> term
#line 114 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                 { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 43: // term -> term, Multiplies, bitwiseoperation
#line 118 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                             { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Multiplies", sc.lineno); }
#line default
        break;
      case 44: // term -> term, Divides, bitwiseoperation
#line 119 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                          { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Divides", sc.lineno); }
#line default
        break;
      case 45: // term -> bitwiseoperation
#line 120 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 46: // bitwiseoperation -> bitwiseoperation, LogicalOr, operation
#line 123 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                        { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "LogicalOr", sc.lineno); }
#line default
        break;
      case 47: // bitwiseoperation -> bitwiseoperation, LogicalAnd, operation
#line 124 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                         { CurrentSemanticValue.expr = new ExpresionOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "LogicalAnd", sc.lineno); }
#line default
        break;
      case 48: // bitwiseoperation -> operation
#line 125 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                 { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 49: // operation -> OpenPar, expresion, ClosePar
#line 129 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                        {  CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-2].expr; }
#line default
        break;
      case 50: // operation -> simpleoperation, operation
#line 130 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                 {  CurrentSemanticValue.expr = new ExpresionOperation(null, ValueStack[ValueStack.Depth-1].expr, ValueStack[ValueStack.Depth-2].val, sc.lineno); }
#line default
        break;
      case 51: // operation -> number
#line 131 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                    { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].num; }
#line default
        break;
      case 52: // number -> IntNumber
#line 134 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                       { CurrentSemanticValue.num = new Number("0", sc.lineno, ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 53: // number -> RealNumber
#line 135 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                        { CurrentSemanticValue.num = new Number("1", sc.lineno, ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 54: // number -> Ident
#line 136 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                   { CurrentSemanticValue.num = new Number(ValueStack[ValueStack.Depth-1].val, sc.lineno); }
#line default
        break;
      case 55: // number -> True
#line 137 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
            { CurrentSemanticValue.num = new Number("2", sc.lineno, "1"); }
#line default
        break;
      case 56: // number -> False
#line 138 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
             { CurrentSemanticValue.num = new Number("2", sc.lineno, "0"); }
#line default
        break;
      case 57: // simpleoperation -> LogicalNegation
#line 141 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { CurrentSemanticValue.val = "LogicalNegation";  }
#line default
        break;
      case 58: // simpleoperation -> BitwiseNegation
#line 142 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { CurrentSemanticValue.val = "BitwiseNegation"; }
#line default
        break;
      case 59: // simpleoperation -> Minus
#line 143 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
             { CurrentSemanticValue.val = "UnarMinus"; }
#line default
        break;
      case 60: // simpleoperation -> OpenPar, Int, ClosePar
#line 144 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                            { CurrentSemanticValue.val = "IntConversion"; }
#line default
        break;
      case 61: // simpleoperation -> OpenPar, Double, ClosePar
#line 145 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                               { CurrentSemanticValue.val = "DoubleConversion"; }
#line default
        break;
      case 62: // simpleoperation -> error
#line 147 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
     {
				 		Console.WriteLine("  line {0,3}:  syntax error", sc.lineno);
						Settings.errors++;
						yyerrok(); 
						YYAbort();
				 }
#line default
        break;
      case 63: // err -> EOF
#line 155 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
             { YYAbort(); }
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 158 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"

public Scanner sc;

public Parser(Scanner scanner) : base(scanner) 
{ 
	sc = scanner;
}
#line default
}
}
