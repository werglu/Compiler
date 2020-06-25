// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  WG
// DateTime: 25.06.2020 23:25:13
// UserName: HP
// Input file <E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y - 25.06.2020 23:24:02>

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
#line 12 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
{
public string  val;
public char    type;
public VarType varType; 
public List<Declarations> list;
public Declarations listNode;
public Number num;
public Expression expr;
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
  // Verbatim content from E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y - 25.06.2020 23:24:02
#line 8 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
	public Program MyProgram {get; set;}	
#line default
  // End verbatim content from E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y - 25.06.2020 23:24:02

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[65];
  private static State[] states = new State[117];
  private static string[] nonTerms = new string[] {
      "declarations", "declaration", "number", "operation", "bitwiseoperation", 
      "expresion", "exp", "exp2", "exp3", "term", "simpleoperation", "start", 
      "$accept", "line", "end", "statement", "statement1", "err", };

  static Parser() {
    states[0] = new State(new int[]{4,4,2,116},new int[]{-12,1,-14,3});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{35,5});
    states[5] = new State(new int[]{36,11,39,14,13,107,11,110,12,113,35,17,7,21,10,26,38,28,9,29,5,91,40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70,8,100},new int[]{-15,6,-1,7,-16,103,-2,105,-17,15,-6,98,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[6] = new State(-3);
    states[7] = new State(new int[]{36,11,39,14,35,17,7,21,10,26,38,28,9,29,5,91,40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70,8,100},new int[]{-15,8,-16,9,-17,15,-6,98,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[8] = new State(-4);
    states[9] = new State(new int[]{36,11,39,14},new int[]{-15,10});
    states[10] = new State(-5);
    states[11] = new State(new int[]{37,12,39,13});
    states[12] = new State(-8);
    states[13] = new State(-9);
    states[14] = new State(-10);
    states[15] = new State(new int[]{35,17,7,21,10,26,38,28,9,29,5,91,40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70,8,100,36,-17,39,-17},new int[]{-16,16,-17,15,-6,98,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[16] = new State(-16);
    states[17] = new State(new int[]{36,20,35,17,7,21,10,26,38,28,9,29,5,91,40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70,8,100},new int[]{-17,18,-6,98,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[18] = new State(new int[]{36,19});
    states[19] = new State(-18);
    states[20] = new State(-19);
    states[21] = new State(new int[]{33,22});
    states[22] = new State(new int[]{40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70},new int[]{-6,23,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[23] = new State(new int[]{34,24});
    states[24] = new State(new int[]{35,17,7,21,10,26,38,28,9,29,5,91,40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70,8,100},new int[]{-17,25,-6,98,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[25] = new State(-20);
    states[26] = new State(new int[]{38,27});
    states[27] = new State(-21);
    states[28] = new State(-22);
    states[29] = new State(new int[]{43,32,40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70},new int[]{-6,30,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[30] = new State(new int[]{38,31});
    states[31] = new State(-23);
    states[32] = new State(new int[]{38,33});
    states[33] = new State(-24);
    states[34] = new State(new int[]{16,35,19,-55,20,-55,29,-55,30,-55,27,-55,28,-55,21,-55,22,-55,23,-55,24,-55,25,-55,26,-55,17,-55,18,-55,38,-55,34,-55});
    states[35] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-7,36,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[36] = new State(new int[]{17,37,18,50,38,-29,34,-29});
    states[37] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-8,38,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[38] = new State(new int[]{21,39,22,52,23,74,24,76,25,78,26,80,17,-31,18,-31,38,-31,34,-31});
    states[39] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-9,40,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[40] = new State(new int[]{27,41,28,54,21,-34,22,-34,23,-34,24,-34,25,-34,26,-34,17,-34,18,-34,38,-34,34,-34});
    states[41] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-10,42,-5,72,-4,71,-11,60,-3,65});
    states[42] = new State(new int[]{29,43,30,56,27,-41,28,-41,21,-41,22,-41,23,-41,24,-41,25,-41,26,-41,17,-41,18,-41,38,-41,34,-41});
    states[43] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-5,44,-4,71,-11,60,-3,65});
    states[44] = new State(new int[]{19,45,20,58,29,-44,30,-44,27,-44,28,-44,21,-44,22,-44,23,-44,24,-44,25,-44,26,-44,17,-44,18,-44,38,-44,34,-44});
    states[45] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-4,46,-11,60,-3,65});
    states[46] = new State(-47);
    states[47] = new State(new int[]{13,83,11,85,12,87,33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-7,48,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[48] = new State(new int[]{34,49,17,37,18,50});
    states[49] = new State(-50);
    states[50] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-8,51,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[51] = new State(new int[]{21,39,22,52,23,74,24,76,25,78,26,80,17,-32,18,-32,38,-32,34,-32});
    states[52] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-9,53,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[53] = new State(new int[]{27,41,28,54,21,-35,22,-35,23,-35,24,-35,25,-35,26,-35,17,-35,18,-35,38,-35,34,-35});
    states[54] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-10,55,-5,72,-4,71,-11,60,-3,65});
    states[55] = new State(new int[]{29,43,30,56,27,-42,28,-42,21,-42,22,-42,23,-42,24,-42,25,-42,26,-42,17,-42,18,-42,38,-42,34,-42});
    states[56] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-5,57,-4,71,-11,60,-3,65});
    states[57] = new State(new int[]{19,45,20,58,29,-45,30,-45,27,-45,28,-45,21,-45,22,-45,23,-45,24,-45,25,-45,26,-45,17,-45,18,-45,38,-45,34,-45});
    states[58] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-4,59,-11,60,-3,65});
    states[59] = new State(-48);
    states[60] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-4,61,-11,60,-3,65});
    states[61] = new State(-51);
    states[62] = new State(-58);
    states[63] = new State(-59);
    states[64] = new State(-60);
    states[65] = new State(-52);
    states[66] = new State(-53);
    states[67] = new State(-54);
    states[68] = new State(-55);
    states[69] = new State(-56);
    states[70] = new State(-57);
    states[71] = new State(-49);
    states[72] = new State(new int[]{19,45,20,58,29,-46,30,-46,27,-46,28,-46,21,-46,22,-46,23,-46,24,-46,25,-46,26,-46,17,-46,18,-46,38,-46,34,-46});
    states[73] = new State(new int[]{29,43,30,56,27,-43,28,-43,21,-43,22,-43,23,-43,24,-43,25,-43,26,-43,17,-43,18,-43,38,-43,34,-43});
    states[74] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-9,75,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[75] = new State(new int[]{27,41,28,54,21,-36,22,-36,23,-36,24,-36,25,-36,26,-36,17,-36,18,-36,38,-36,34,-36});
    states[76] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-9,77,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[77] = new State(new int[]{27,41,28,54,21,-37,22,-37,23,-37,24,-37,25,-37,26,-37,17,-37,18,-37,38,-37,34,-37});
    states[78] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-9,79,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[79] = new State(new int[]{27,41,28,54,21,-38,22,-38,23,-38,24,-38,25,-38,26,-38,17,-38,18,-38,38,-38,34,-38});
    states[80] = new State(new int[]{33,47,31,62,32,63,28,64,41,66,42,67,40,68,14,69,15,70},new int[]{-9,81,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[81] = new State(new int[]{27,41,28,54,21,-39,22,-39,23,-39,24,-39,25,-39,26,-39,17,-39,18,-39,38,-39,34,-39});
    states[82] = new State(new int[]{27,41,28,54,21,-40,22,-40,23,-40,24,-40,25,-40,26,-40,17,-40,18,-40,38,-40,34,-40});
    states[83] = new State(new int[]{34,84});
    states[84] = new State(-61);
    states[85] = new State(new int[]{34,86});
    states[86] = new State(-62);
    states[87] = new State(new int[]{34,88});
    states[88] = new State(-63);
    states[89] = new State(new int[]{21,39,22,52,23,74,24,76,25,78,26,80,17,-33,18,-33,38,-33,34,-33});
    states[90] = new State(new int[]{17,37,18,50,38,-30,34,-30});
    states[91] = new State(new int[]{33,92});
    states[92] = new State(new int[]{40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70},new int[]{-6,93,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[93] = new State(new int[]{34,94});
    states[94] = new State(new int[]{35,17,7,21,10,26,38,28,9,29,5,91,40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70,8,100},new int[]{-17,95,-6,98,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[95] = new State(new int[]{6,96,35,-25,7,-25,10,-25,38,-25,9,-25,5,-25,40,-25,33,-25,31,-25,32,-25,28,-25,41,-25,42,-25,14,-25,15,-25,8,-25,36,-25,39,-25});
    states[96] = new State(new int[]{35,17,7,21,10,26,38,28,9,29,5,91,40,34,33,47,31,62,32,63,28,64,41,66,42,67,14,69,15,70,8,100},new int[]{-17,97,-6,98,-7,90,-8,89,-9,82,-10,73,-5,72,-4,71,-11,60,-3,65});
    states[97] = new State(-26);
    states[98] = new State(new int[]{38,99});
    states[99] = new State(-27);
    states[100] = new State(new int[]{40,101});
    states[101] = new State(new int[]{38,102});
    states[102] = new State(-28);
    states[103] = new State(new int[]{36,11,39,14},new int[]{-15,104});
    states[104] = new State(-6);
    states[105] = new State(new int[]{13,107,11,110,12,113,36,-12,39,-12,35,-12,7,-12,10,-12,38,-12,9,-12,5,-12,40,-12,33,-12,31,-12,32,-12,28,-12,41,-12,42,-12,14,-12,15,-12,8,-12},new int[]{-1,106,-2,105});
    states[106] = new State(-11);
    states[107] = new State(new int[]{40,108});
    states[108] = new State(new int[]{38,109});
    states[109] = new State(-13);
    states[110] = new State(new int[]{40,111});
    states[111] = new State(new int[]{38,112});
    states[112] = new State(-14);
    states[113] = new State(new int[]{40,114});
    states[114] = new State(new int[]{38,115});
    states[115] = new State(-15);
    states[116] = new State(-7);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-13, new int[]{-12,3});
    rules[2] = new Rule(-12, new int[]{-14});
    rules[3] = new Rule(-14, new int[]{4,35,-15});
    rules[4] = new Rule(-14, new int[]{4,35,-1,-15});
    rules[5] = new Rule(-14, new int[]{4,35,-1,-16,-15});
    rules[6] = new Rule(-14, new int[]{4,35,-16,-15});
    rules[7] = new Rule(-14, new int[]{2});
    rules[8] = new Rule(-15, new int[]{36,37});
    rules[9] = new Rule(-15, new int[]{36,39});
    rules[10] = new Rule(-15, new int[]{39});
    rules[11] = new Rule(-1, new int[]{-2,-1});
    rules[12] = new Rule(-1, new int[]{-2});
    rules[13] = new Rule(-2, new int[]{13,40,38});
    rules[14] = new Rule(-2, new int[]{11,40,38});
    rules[15] = new Rule(-2, new int[]{12,40,38});
    rules[16] = new Rule(-16, new int[]{-17,-16});
    rules[17] = new Rule(-16, new int[]{-17});
    rules[18] = new Rule(-17, new int[]{35,-17,36});
    rules[19] = new Rule(-17, new int[]{35,36});
    rules[20] = new Rule(-17, new int[]{7,33,-6,34,-17});
    rules[21] = new Rule(-17, new int[]{10,38});
    rules[22] = new Rule(-17, new int[]{38});
    rules[23] = new Rule(-17, new int[]{9,-6,38});
    rules[24] = new Rule(-17, new int[]{9,43,38});
    rules[25] = new Rule(-17, new int[]{5,33,-6,34,-17});
    rules[26] = new Rule(-17, new int[]{5,33,-6,34,-17,6,-17});
    rules[27] = new Rule(-17, new int[]{-6,38});
    rules[28] = new Rule(-17, new int[]{8,40,38});
    rules[29] = new Rule(-6, new int[]{40,16,-7});
    rules[30] = new Rule(-6, new int[]{-7});
    rules[31] = new Rule(-7, new int[]{-7,17,-8});
    rules[32] = new Rule(-7, new int[]{-7,18,-8});
    rules[33] = new Rule(-7, new int[]{-8});
    rules[34] = new Rule(-8, new int[]{-8,21,-9});
    rules[35] = new Rule(-8, new int[]{-8,22,-9});
    rules[36] = new Rule(-8, new int[]{-8,23,-9});
    rules[37] = new Rule(-8, new int[]{-8,24,-9});
    rules[38] = new Rule(-8, new int[]{-8,25,-9});
    rules[39] = new Rule(-8, new int[]{-8,26,-9});
    rules[40] = new Rule(-8, new int[]{-9});
    rules[41] = new Rule(-9, new int[]{-9,27,-10});
    rules[42] = new Rule(-9, new int[]{-9,28,-10});
    rules[43] = new Rule(-9, new int[]{-10});
    rules[44] = new Rule(-10, new int[]{-10,29,-5});
    rules[45] = new Rule(-10, new int[]{-10,30,-5});
    rules[46] = new Rule(-10, new int[]{-5});
    rules[47] = new Rule(-5, new int[]{-5,19,-4});
    rules[48] = new Rule(-5, new int[]{-5,20,-4});
    rules[49] = new Rule(-5, new int[]{-4});
    rules[50] = new Rule(-4, new int[]{33,-7,34});
    rules[51] = new Rule(-4, new int[]{-11,-4});
    rules[52] = new Rule(-4, new int[]{-3});
    rules[53] = new Rule(-3, new int[]{41});
    rules[54] = new Rule(-3, new int[]{42});
    rules[55] = new Rule(-3, new int[]{40});
    rules[56] = new Rule(-3, new int[]{14});
    rules[57] = new Rule(-3, new int[]{15});
    rules[58] = new Rule(-11, new int[]{31});
    rules[59] = new Rule(-11, new int[]{32});
    rules[60] = new Rule(-11, new int[]{28});
    rules[61] = new Rule(-11, new int[]{33,13,34});
    rules[62] = new Rule(-11, new int[]{33,11,34});
    rules[63] = new Rule(-11, new int[]{33,12,34});
    rules[64] = new Rule(-18, new int[]{3});
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
#line 35 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                 { Console.WriteLine("start");  YYAccept(); }
#line default
        break;
      case 3: // line -> Program, OpenBraces, end
#line 38 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { MyProgram = new Program(new List<Declarations>());  }
#line default
        break;
      case 4: // line -> Program, OpenBraces, declarations, end
#line 39 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                { MyProgram = new Program(ValueStack[ValueStack.Depth-2].list);  }
#line default
        break;
      case 5: // line -> Program, OpenBraces, declarations, statement, end
#line 40 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                    { MyProgram = new Program(ValueStack[ValueStack.Depth-3].list);  }
#line default
        break;
      case 6: // line -> Program, OpenBraces, statement, end
#line 41 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                       { MyProgram = new Program(new List<Declarations>()); }
#line default
        break;
      case 7: // line -> error
#line 43 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
   { 
				Console.WriteLine("  line {0,3}:  syntax error",sc.linen);
				Settings.errors++;
				yyerrok(); 
				YYAbort();
			}
#line default
        break;
      case 8: // end -> CloseBraces, Eof
#line 51 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                        {  YYAccept();  }
#line default
        break;
      case 9: // end -> CloseBraces, Error
#line 53 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
   {
	  	Console.WriteLine("  line {0,3}:  syntax error",sc.linen);
		Settings.errors++;
		yyerrok(); 
	  }
#line default
        break;
      case 10: // end -> Error
#line 59 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
   { 
	  	Console.WriteLine("  line {0,3}:  syntax error",sc.linen);
		Settings.errors++;
		yyerrok(); 
	  }
#line default
        break;
      case 11: // declarations -> declaration, declarations
#line 66 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                         { ValueStack[ValueStack.Depth-1].list.Add(ValueStack[ValueStack.Depth-2].listNode); CurrentSemanticValue.list = ValueStack[ValueStack.Depth-1].list; }
#line default
        break;
      case 12: // declarations -> declaration
#line 67 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                            { CurrentSemanticValue.list = new List<Declarations>{ValueStack[ValueStack.Depth-1].listNode};  }
#line default
        break;
      case 13: // declaration -> Bool, Ident, Semicolon
#line 70 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { CurrentSemanticValue.listNode = new Declarations(ValueStack[ValueStack.Depth-2].val, 2); }
#line default
        break;
      case 14: // declaration -> Int, Ident, Semicolon
#line 71 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                         { CurrentSemanticValue.listNode = new Declarations(ValueStack[ValueStack.Depth-2].val, 0); }
#line default
        break;
      case 15: // declaration -> Double, Ident, Semicolon
#line 72 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                            { CurrentSemanticValue.listNode = new Declarations(ValueStack[ValueStack.Depth-2].val, 1); }
#line default
        break;
      case 24: // statement1 -> Write, String, Semicolon
#line 85 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { }
#line default
        break;
      case 29: // expresion -> Ident, Assign, exp
#line 93 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { CurrentSemanticValue.expr = new ExpresionOperation(new Number(ValueStack[ValueStack.Depth-3].val), ValueStack[ValueStack.Depth-1].expr, "Assign"); }
#line default
        break;
      case 30: // expresion -> exp
#line 94 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
          { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 31: // exp -> exp, Or, exp2
#line 97 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                        { CurrentSemanticValue.expr = new ExpOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Or"); }
#line default
        break;
      case 32: // exp -> exp, And, exp2
#line 98 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                   { CurrentSemanticValue.expr = new ExpOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "And"); }
#line default
        break;
      case 33: // exp -> exp2
#line 99 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
           { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 34: // exp2 -> exp2, Equal, exp3
#line 102 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                            { CurrentSemanticValue.expr = new Exp2Operation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Equal"); }
#line default
        break;
      case 35: // exp2 -> exp2, NotEqual, exp3
#line 103 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                               { CurrentSemanticValue.expr = new Exp2Operation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "NotEqual"); }
#line default
        break;
      case 36: // exp2 -> exp2, GreatherThan, exp3
#line 104 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { CurrentSemanticValue.expr = new Exp2Operation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "GreatherThan"); }
#line default
        break;
      case 37: // exp2 -> exp2, GreatherThanOrEqual, exp3
#line 105 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                    { CurrentSemanticValue.expr = new Exp2Operation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "GreatherThanOrEqual"); }
#line default
        break;
      case 38: // exp2 -> exp2, LessThan, exp3
#line 106 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                         { CurrentSemanticValue.expr = new Exp2Operation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "LessThan"); }
#line default
        break;
      case 39: // exp2 -> exp2, LessThanOrEqual, exp3
#line 107 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                { CurrentSemanticValue.expr = new Exp2Operation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "LessThanOrEqual"); }
#line default
        break;
      case 40: // exp2 -> exp3
#line 108 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
           { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 41: // exp3 -> exp3, Plus, term
#line 111 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                           { CurrentSemanticValue.expr = new Exp3Operation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Plus"); }
#line default
        break;
      case 42: // exp3 -> exp3, Minus, term
#line 112 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { CurrentSemanticValue.expr = new Exp3Operation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Minus"); }
#line default
        break;
      case 43: // exp3 -> term
#line 113 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                 { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 44: // term -> term, Multiplies, bitwiseoperation
#line 117 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                             { CurrentSemanticValue.expr = new TermOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Multiplies"); }
#line default
        break;
      case 45: // term -> term, Divides, bitwiseoperation
#line 118 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                          { CurrentSemanticValue.expr = new TermOperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "Divides"); }
#line default
        break;
      case 46: // term -> bitwiseoperation
#line 119 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 47: // bitwiseoperation -> bitwiseoperation, LogicalOr, operation
#line 122 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                        { CurrentSemanticValue.expr = new Bitwiseoperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "LogicalOr"); }
#line default
        break;
      case 48: // bitwiseoperation -> bitwiseoperation, LogicalAnd, operation
#line 123 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                                         { CurrentSemanticValue.expr = new Bitwiseoperation(ValueStack[ValueStack.Depth-3].expr, ValueStack[ValueStack.Depth-1].expr, "LogicalAnd"); }
#line default
        break;
      case 49: // bitwiseoperation -> operation
#line 124 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                 { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 50: // operation -> OpenPar, exp, ClosePar
#line 128 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                  {  }
#line default
        break;
      case 51: // operation -> simpleoperation, operation
#line 129 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                 {  CurrentSemanticValue.expr = new Operation(null, ValueStack[ValueStack.Depth-1].expr, ValueStack[ValueStack.Depth-2].val); }
#line default
        break;
      case 52: // operation -> number
#line 130 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                    { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].num; }
#line default
        break;
      case 53: // number -> IntNumber
#line 133 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                       { CurrentSemanticValue.num = new Number("0", ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 54: // number -> RealNumber
#line 134 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                        { CurrentSemanticValue.num = new Number("1", ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 55: // number -> Ident
#line 135 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                   { CurrentSemanticValue.num = new Number(ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 56: // number -> True
#line 136 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
            { CurrentSemanticValue.num = new Number("2", "1"); }
#line default
        break;
      case 57: // number -> False
#line 137 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
             { CurrentSemanticValue.num = new Number("2", "0"); }
#line default
        break;
      case 58: // simpleoperation -> LogicalNegation
#line 140 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { CurrentSemanticValue.val = "LogicalNegation";  }
#line default
        break;
      case 59: // simpleoperation -> BitwiseNegation
#line 141 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                                   { CurrentSemanticValue.val = "BitwiseNegation"; }
#line default
        break;
      case 60: // simpleoperation -> Minus
#line 142 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
             { CurrentSemanticValue.val = "Minus"; }
#line default
        break;
      case 61: // simpleoperation -> OpenPar, Bool, ClosePar
#line 143 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                             { CurrentSemanticValue.val = "BoolConversion"; }
#line default
        break;
      case 62: // simpleoperation -> OpenPar, Int, ClosePar
#line 144 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                            { CurrentSemanticValue.val = "IntConversion"; }
#line default
        break;
      case 63: // simpleoperation -> OpenPar, Double, ClosePar
#line 145 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
                               { CurrentSemanticValue.val = "DoubleConversion"; }
#line default
        break;
      case 64: // err -> EOF
#line 148 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
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

#line 151 "E:\MetodyTranslacji\github\Compilator\Compilator\kompilator.y"
 int lineno=1;

public Scanner sc;

public Parser(Scanner scanner) : base(scanner) 
{ 
	sc = scanner;
}
#line default
}
}
