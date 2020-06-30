using System;
using System.IO;
using System.Collections.Generic;
using GardensPoint;
using System.Globalization;

namespace compiler
{
    public enum VarType
    {
        Int = 0,
        Double = 1,
        Bool = 2,
        Undefined = 3
    }

    public class ValueProperties
    {
        public string name;
        public VarType type;
        public double value;

        public ValueProperties(string _name, VarType _type, double _value)
        {
            name = _name;
            type = _type;
            value = _value;
        }
    }

    public abstract class Expression
    {
        public abstract VarType CheckType();
        public abstract string GenCode(StreamWriter sw, bool loadString = false);
    }


    public abstract class Statement
    {
        public abstract string GenCode(StreamWriter sw);
    }

    public class Compiler
    {
        public static List<string> source;

        public static int Main(string[] args)
        {
            string file;
            FileStream source;
            Console.WriteLine("\nSingle-Pass CIL Code Generator for Multiline Calculator - Gardens Point");
            if (args.Length >= 1)
                file = args[0];
            else
            {
                Console.Write("\nsource file:  ");
                file = Console.ReadLine();
            }
            try
            {
                var sr = new StreamReader(file);
                string str = sr.ReadToEnd();
                sr.Close();
                Compiler.source = new System.Collections.Generic.List<string>(str.Split(new string[] { "\r\n" }, System.StringSplitOptions.None));
                source = new FileStream(file, FileMode.Open);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
                return 1;
            }
            Settings.scanner = new Scanner(source);
            Parser parser = new Parser(Settings.scanner);
            Console.WriteLine();

            sw = new StreamWriter(file + ".il");
            GenProlog();
            bool canParse = parser.Parse();
            if(parser.MyProgram !=  null)
            parser.MyProgram.GenCode(sw);
            //if (!canParse) return 1;
            GenEpilog();
            sw.Close();
            source.Close();
            if (Settings.errors == 0)
                Console.WriteLine("  compilation successful\n");
            else
            {
                Console.WriteLine($"\n  {Settings.errors} errors detected\n");
                File.Delete(file + ".il");
            }
            return Settings.errors == 0 ? 0 : 2;
        }

        public static void EmitCode(string instr = null)
        {
            sw.WriteLine(instr);
        }

        public static void EmitCode(string instr, params object[] args)
        {
            sw.WriteLine(instr, args);
        }

        private static StreamWriter sw;

        private static void GenProlog()
        {
            EmitCode(".assembly extern mscorlib { }");
            EmitCode(".assembly calculator { }");
            EmitCode(".method static void main()");
            EmitCode("{");
            EmitCode(".entrypoint");
            EmitCode(".maxstack  128");
            EmitCode(".try");
            EmitCode("{");
            EmitCode();

            EmitCode("// prolog");

            EmitCode();
        }

        private static void GenEpilog()
        {
            EmitCode("leave EndMain");
            EmitCode("}");
            EmitCode("catch [mscorlib]System.Exception");
            EmitCode("{");
            EmitCode("callvirt instance string [mscorlib]System.Exception::get_Message()");
            EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
            EmitCode("leave EndMain");
            EmitCode("}");
            EmitCode("EndMain: ret");
            EmitCode("}");
        }

        public void EmitErrorCode(string error)
        {
            Console.WriteLine($"{error}");
        }
    }

    public class Program
    {
        List<Declarations> list = new List<Declarations>();
        List<Statement> statList = new List<Statement>();

        public Program(List<Declarations> _list, List<Statement> _statList)
        {
            list = _list;
            statList = _statList;
        }

        public Program(List<Declarations> _list)
        {
            list = _list;
        }

        public Program(List<Statement> _statList)
        {
            statList = _statList;
        }

        public void GenCode(StreamWriter sw)
        {
            for (int i=0; i<list.Count;i++)
            {
                list[i].GenCode(sw);
            }
            for (int i=statList.Count-1; i>=0; i--)
            {
                statList[i].GenCode(sw);
            }
        }
    }


    public static class Settings  //class contains 'global' variables and 'global' methods
    {
        public static int errors = 0;
        public static int number = 0;

        public static Dictionary<string, Tuple<VarType, double>> varDictionary = new Dictionary<string, Tuple<VarType, double>>();
        public static Scanner scanner;
        public static List<int> linenumbers = new List<int>();

        public static void EmitCode(StreamWriter sw, string instr = null)
        {
            sw.WriteLine(instr);
        }

        public static void EmitCode(StreamWriter sw, string instr, params object[] args)
        {
            sw.WriteLine(instr, args);
        }

        public static void IncreaseNumber()
        {
            number++;
        }
    }

    public class Declarations
    {
        public string name;
        public VarType varType;
        public double value;
        public int lineno;

        public Declarations(string _name, int _varType, int _lineno, double _value = 0)
        {
            lineno = _lineno;
            if (Settings.varDictionary.ContainsKey(_name))
            {
                Console.WriteLine($"Variable already declared - variable with name {_name} already declared!");
                Settings.errors++;
                return;
            }

            name = _name;
            if (_varType == 1)
                varType = VarType.Double;
            if (_varType == 2)
                varType = VarType.Bool;
            if (_varType == 0)
                varType = VarType.Int;

            Settings.varDictionary.Add(name, new Tuple<VarType, double>(varType, value));
        }

        public void GenCode(StreamWriter sw)
        {
            string typ = "";
            if (varType == VarType.Double) typ = "float64 D_";
            if (varType == VarType.Int) typ = "int32 I_";
            if (varType == VarType.Bool) typ = "bool B_";
            Settings.EmitCode(sw, $".locals init ({typ}{name})");
        }
    }

    public class Number : Expression
    {
        public VarType varType;
        public double value;
        public string name = "";
        public int lineno;
        public bool ident = false;
        public Number(string _varType, int _lineno, string _value = "")
        {
            lineno = _lineno;
            if (_varType == "0")
                varType = VarType.Int;
            else if (_varType == "1")
                varType = VarType.Double;
            else if (_varType == "2")
                varType = VarType.Bool;
            else
            {
                ident = true;
                if (!Settings.varDictionary.ContainsKey(_varType))
                {
                    Console.WriteLine($"Undeclared variable - variable with name {_varType} is not declared! - line: " + lineno.ToString());
                    Settings.errors++;
                    return;
                }

                name = _varType;
                varType = Settings.varDictionary[_varType].Item1;
                value = Settings.varDictionary[_varType].Item2;
                return;
            }

            value = Double.Parse(_value.Replace('.', ','));
        }

        public override VarType CheckType()
        {
            return varType;
        }

        public override string GenCode(StreamWriter sw, bool loadString = false)
        {
            if(varType == VarType.Int && !ident)
            {
                Settings.EmitCode(sw, $"ldc.i4 {(int)value}");
            }
            else if (varType == VarType.Bool && !ident)
            {
                int val = value == 0 ? 0 : 1;
                Settings.EmitCode(sw, $"ldc.i4.{val}");
            }
            else if (varType == VarType.Double && !ident)
            {
                var culture = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.000000000000000}", value);
                Settings.EmitCode(sw, $"ldc.r8 {culture}");
            }
            else if (ident)
            {
                if (varType == VarType.Int)
                    Settings.EmitCode(sw, $"ldloc I_{name}");
                if (varType == VarType.Bool)
                    Settings.EmitCode(sw, $"ldloc B_{name}");
                if (varType == VarType.Double)
                    Settings.EmitCode(sw, $"ldloc D_{name}");
            }
            return null;
        }
    }

    public class ExpresionOperation : Expression
    {
        public Expression LExp;
        public Expression RExp;
        public string operationType;
        int lineno;

        public ExpresionOperation(Expression _lexp, Expression _rexp, string _operationType, int _lineno)
        {
            LExp = _lexp;
            RExp = _rexp;
            operationType = _operationType;
            lineno = _lineno;
        }

        public override string GenCode(StreamWriter sw, bool loadString = false)
        {
            string s = "";
            if(operationType == "Assign")
            {
                RExp.GenCode(sw);
                var Lval = LExp.CheckType();
                var Rval = RExp.CheckType();
                
                if (Lval == VarType.Bool)
                {
                    Settings.EmitCode(sw, "dup");

                    Settings.EmitCode(sw, $"stloc B_{(LExp as Number).name}");
                }
                if (Lval == VarType.Double)
                {
                    if(Rval== VarType.Int)
                    {
                        Settings.EmitCode(sw, "conv.r8");
                    }
                    Settings.EmitCode(sw, "dup");

                    Settings.EmitCode(sw, $"stloc D_{(LExp as Number).name}");
                }
                if (Lval == VarType.Int)
                {
                    Settings.EmitCode(sw, "dup");

                    Settings.EmitCode(sw, $"stloc I_{(LExp as Number).name}");
                }
            }
            if (operationType == "UnarMinus")
            {
                RExp.GenCode(sw);
                Settings.EmitCode(sw, "neg");        
            }
            if(operationType== "BitwiseNegation")
            {
                RExp.GenCode(sw);
                Settings.EmitCode(sw, "not");
            }
            if ( operationType == "LogicalNegation")
            {
                RExp.GenCode(sw);
                Settings.EmitCode(sw, "ldc.i4.0");
                Settings.EmitCode(sw, "ceq");

            }
            if (operationType == "IntConversion")
            {
                RExp.GenCode(sw);
                Settings.EmitCode(sw, "conv.i4");
            }
            if (operationType == "DoubleConversion")
            {
                RExp.GenCode(sw);
                Settings.EmitCode(sw, "conv.r8");
            }
            if (operationType == "Multiplies" || operationType == "Divides" || operationType == "Plus" || operationType == "Minus")
            {
                bool check = false;
                var Ltype = LExp.CheckType();
                var Rtype = RExp.CheckType();

                if (Ltype == VarType.Double || Rtype == VarType.Double) { check = true; }
                LExp.GenCode(sw);
                if(check)
                {
                    if(Ltype == VarType.Int)
                    {
                        Settings.EmitCode(sw, "conv.r8");
                    }
                }
                RExp.GenCode(sw);
                if (check)
                {
                    if (Rtype == VarType.Int)
                    {
                        Settings.EmitCode(sw, "conv.r8");
                    }
                }
                if (operationType == "Multiplies")
                    Settings.EmitCode(sw, "mul");
                if (operationType == "Divides")
                    Settings.EmitCode(sw, "div");
                if (operationType == "Plus")
                    Settings.EmitCode(sw, "add");
                if (operationType == "Minus")
                    Settings.EmitCode(sw, "sub");
            }
            if (operationType == "GreatherThan" || operationType == "LessThanOrEqual" || operationType == "LessThan" || operationType == "GreatherThanOrEqual")
            {
                bool check = false;
                var Ltype = LExp.CheckType();
                var Rtype = RExp.CheckType();

                if (Ltype == VarType.Double || Rtype == VarType.Double) { check = true; }
                LExp.GenCode(sw);
                if (check)
                {
                    if (Ltype == VarType.Int)
                    {
                        Settings.EmitCode(sw, "conv.r8");
                    }
                }
                RExp.GenCode(sw);
                if (check)
                {
                    if (Rtype == VarType.Int)
                    {
                        Settings.EmitCode(sw, "conv.r8");
                    }
                }

                if (operationType == "GreatherThan") { Settings.EmitCode(sw, "cgt"); }
                if (operationType == "LessThanOrEqual")
                {
                    Settings.EmitCode(sw, "cgt");
                    Settings.EmitCode(sw, "ldc.i4.0");
                    Settings.EmitCode(sw, "ceq");
                }
                if (operationType == "LessThan") { Settings.EmitCode(sw, "clt"); }
                if (operationType == "GreatherThanOrEqual")
                {
                    Settings.EmitCode(sw, "clt");
                    Settings.EmitCode(sw, "ldc.i4.0");
                    Settings.EmitCode(sw, "ceq");
                }
            }
            if (operationType == "Equal" || operationType == "NotEqual")
            {
                bool check = false;
                var Ltype = LExp.CheckType();
                var Rtype = RExp.CheckType();

                if (Ltype == VarType.Double || Rtype == VarType.Double) { check = true; }
                LExp.GenCode(sw);
                if (check)
                {
                    if (Ltype == VarType.Int)
                    {
                        Settings.EmitCode(sw, "conv.r8");
                    }
                }
                RExp.GenCode(sw);
                if (check)
                {
                    if (Rtype == VarType.Int)
                    {
                        Settings.EmitCode(sw, "conv.r8");
                    }
                }
                if (operationType == "Equal") { Settings.EmitCode(sw, "ceq"); }
                if (operationType == "NotEqual")
                {
                    Settings.EmitCode(sw, "ceq");
                    Settings.EmitCode(sw, "ldc.i4.0");
                    Settings.EmitCode(sw, "ceq");
                }
            }
            if (operationType == "Or")
            {
                string et1 = String.Concat("et", Settings.number.ToString());
                Settings.IncreaseNumber();
                string et2 = String.Concat("et", Settings.number.ToString());
                Settings.IncreaseNumber();
                string et3 = String.Concat("et", Settings.number.ToString());
                Settings.IncreaseNumber();

                LExp.GenCode(sw);
                Settings.EmitCode(sw, $"brtrue {et1}");
                RExp.GenCode(sw);
                Settings.EmitCode(sw, $"brtrue {et1}");
                Settings.EmitCode(sw, $"br {et2}");

                Settings.EmitCode(sw, $"{et1}: nop ldc.i4.1");
                Settings.EmitCode(sw, $"br {et3}");

                Settings.EmitCode(sw, $"{et2}: nop ldc.i4.0");

                Settings.EmitCode(sw, $"{et3}: nop");

            }
            if (operationType == "And")
            {
                string et1 = String.Concat("ft", Settings.number.ToString());
                Settings.IncreaseNumber();
                string et2 = String.Concat("ft", Settings.number.ToString());
                Settings.IncreaseNumber();
                string et3 = String.Concat("ft", Settings.number.ToString());

                Settings.IncreaseNumber();

                LExp.GenCode(sw);
                Settings.EmitCode(sw, $"brfalse {et1}");
                RExp.GenCode(sw);
                Settings.EmitCode(sw, $"brfalse {et1}");
                Settings.EmitCode(sw, $"br {et2}");

                Settings.EmitCode(sw, $"{et1}: nop ldc.i4.0");
                Settings.EmitCode(sw, $"br {et3}");

                Settings.EmitCode(sw, $"{et2}: nop ldc.i4.1");

                Settings.EmitCode(sw, $"{et3}: nop");

            }
            if (operationType == "LogicalOr")
            {
                LExp.GenCode(sw);
                RExp.GenCode(sw);
                Settings.EmitCode(sw, "or");
            }
            if (operationType == "LogicalAnd")
            {
                LExp.GenCode(sw);
                RExp.GenCode(sw);
                Settings.EmitCode(sw, "and");
            }
            return s;
        }

        public override VarType CheckType()  // function checks and returns type of expression and write error when there is a type mismatch
        {
            if (operationType == "LogicalOr" || operationType == "LogicalAnd")
            {
                VarType LexpCheckType = LExp.CheckType();
                VarType RexpCheckType = RExp.CheckType();

                if (LexpCheckType == VarType.Int && RexpCheckType == VarType.Int)
                {
                    return VarType.Int;
                }
                if (RexpCheckType != VarType.Undefined && LexpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be int! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if(operationType == "UnarMinus")
            {
                VarType RExpCheckType = RExp.CheckType();

                if (RExpCheckType == VarType.Int)
                {
                    return VarType.Int;
                }
                if(RExpCheckType == VarType.Double)
                {
                    return VarType.Double;
                }
                if (RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be int or double! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if(operationType == "BitwiseNegation")
            {
                VarType RExpCheckType = RExp.CheckType();

                if (RExpCheckType == VarType.Int)
                {
                    return VarType.Int;
                }
                if (RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be int! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if (operationType == "LogicalNegation")
            {
                VarType RExpCheckType = RExp.CheckType();

                if (RExpCheckType == VarType.Bool)
                {
                    return VarType.Bool;
                }
                if (RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be bool! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if(operationType == "IntConversion")
            {
                VarType RExpCheckType = RExp.CheckType();

                if (RExpCheckType != VarType.Undefined)
                {
                    return VarType.Int;
                }
                if (RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be bool, int or double! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if (operationType == "DoubleConversion")
            {
                VarType RExpCheckType = RExp.CheckType();

                if (RExpCheckType != VarType.Undefined)
                {
                    return VarType.Double;
                }
                if (RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be bool, int or double! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if(operationType == "Multiplies" || operationType == "Divides" || operationType == "Plus" || operationType == "Minus")
            {
                VarType RExpCheckType = RExp.CheckType();
                VarType LExpCheckType = LExp.CheckType();

                if (LExpCheckType == VarType.Int && RExpCheckType == VarType.Int)
                {
                    return VarType.Int;
                }
                if((LExpCheckType == VarType.Double || LExpCheckType == VarType.Int) && (RExpCheckType == VarType.Double || RExpCheckType == VarType.Int))
                {
                    return VarType.Double;
                }
                if (LExpCheckType != VarType.Undefined && RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be int or double! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if (operationType == "GreatherThan" || operationType == "LessThanOrEqual" || operationType == "LessThan" || operationType == "GreatherThanOrEqual")
            {
                VarType RExpCheckType = RExp.CheckType();
                VarType LExpCheckType = LExp.CheckType();

                if ((LExpCheckType == VarType.Double || LExpCheckType == VarType.Int) && (RExpCheckType == VarType.Double || RExpCheckType == VarType.Int))
                {
                    return VarType.Bool;
                }
                if (LExpCheckType != VarType.Undefined && RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be int or double! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }


            if(operationType == "Equal" || operationType == "NotEqual")
            {
                VarType RExpCheckType = RExp.CheckType();
                VarType LExpCheckType = LExp.CheckType();

                if ((LExpCheckType == VarType.Double || LExpCheckType == VarType.Int) && (RExpCheckType  == VarType.Double || RExpCheckType == VarType.Int))
                {
                    return VarType.Bool;
                }

                if (LExpCheckType == VarType.Bool && RExpCheckType == VarType.Bool)
                {
                    return VarType.Bool;
                }
                if (LExpCheckType != VarType.Undefined && RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be bool, int or double! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;

            }
            if (operationType == "Or" || operationType == "And")
            {
                VarType RExpCheckType = RExp.CheckType();
                VarType LExpCheckType = LExp.CheckType();

                if (LExpCheckType == VarType.Bool && RExpCheckType == VarType.Bool)
                {
                    return VarType.Bool;
                }
                if (LExpCheckType != VarType.Undefined && RExpCheckType != VarType.Undefined)
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - both should be bool! - line: " + lineno.ToString());
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if (operationType == "Assign")
            {
                VarType RExpCheckType = RExp.CheckType();
                VarType LExpCheckType = LExp.CheckType();
                if (LExpCheckType == VarType.Double && (RExpCheckType == VarType.Double || RExpCheckType == VarType.Int))
                {
                    return VarType.Double;
                }
                if (LExpCheckType == VarType.Int && RExpCheckType == VarType.Int)
                {
                    return VarType.Int;
                }
                if (LExpCheckType == VarType.Bool && RExpCheckType == VarType.Bool)
                {
                    return VarType.Bool;
                }

                Settings.linenumbers.Add(lineno);
                Console.WriteLine($"Type mismatch {operationType} - cannot assign two different types! - line: " + lineno.ToString());
                Settings.errors++;
                
                return VarType.Undefined;
            }

            return VarType.Undefined;
        }
    }

    public class EmptyStatement : Statement
    {
        public override string GenCode(StreamWriter sw)
        {
            Settings.EmitCode(sw, "nop"); return null;
        }
    }

    public class ReturnStatement : Statement
    {
        public override string GenCode(StreamWriter sw)
        {
            Settings.EmitCode(sw, "leave EndMain");
            return null;
        }
    }

    public class WhileStatement : Statement
    {
        public Expression exp = null;
        public Statement stat = null;

        public WhileStatement(Expression _exp, Statement _stat)
        {
            if (_exp.CheckType() != VarType.Bool && _exp.CheckType() != VarType.Undefined)
            {
                Console.WriteLine($"Type mismatch - expression in while should be bool type!");
                Settings.errors++;
            }
            exp = _exp;
            stat = _stat;
        }

        public override string GenCode(StreamWriter sw)
        {
            string et1 = String.Concat("et", Settings.number.ToString());
            Settings.IncreaseNumber();
            string et2 = String.Concat("et", Settings.number.ToString());
            Settings.IncreaseNumber();
            string et3 = String.Concat("et", Settings.number.ToString());
            Settings.IncreaseNumber();

            Settings.EmitCode(sw, $"br {et1}");
            Settings.EmitCode(sw, $"{et2}:" );
            stat.GenCode(sw);
            Settings.EmitCode(sw, $"{et1}:");
            exp.GenCode(sw);
            Settings.EmitCode(sw, $"brtrue {et2}");

            return null;
        }
    }

    public class WriteStatement : Statement
    {
        public Expression exp = null;
        public string str = null;

        public WriteStatement(Expression _exp)
        {
            _exp.CheckType();
            exp = _exp;
        }

        public WriteStatement(string _str)
        {
            str = _str;
        }

        public override string GenCode(StreamWriter sw)
        {
            if(str != null)
            {
                Settings.EmitCode(sw, $"ldstr {str}");                
                Settings.EmitCode(sw, "call void [mscorlib]System.Console::Write(string)");
            }
            else if(exp != null)
            {
                var value = exp.CheckType();
                ExpresionOperation ee = exp as ExpresionOperation;
                if (value == VarType.Double)
                {
                    Settings.EmitCode(sw, "call class [mscorlib] System.Globalization.CultureInfo[mscorlib] System.Globalization.CultureInfo::get_InvariantCulture()");
                    Settings.EmitCode(sw, "ldstr \"{0:0.000000}\" ");
                    exp.GenCode(sw);
                    Settings.EmitCode(sw, "box [mscorlib]System.Double");
                    Settings.EmitCode(sw, "call string [mscorlib]System.String::Format(class [mscorlib]System.IFormatProvider,string,object)");
                    Settings.EmitCode(sw, "call void [mscorlib]System.Console::Write(string)");                    
                }
                if (value == VarType.Bool)
                {
                    exp.GenCode(sw);
                    Settings.EmitCode(sw, "call void [mscorlib]System.Console::Write(bool)");
                }
                if (value == VarType.Int)
                {
                    exp.GenCode(sw);
                    Settings.EmitCode(sw, "call void [mscorlib]System.Console::Write(int32)");
                }
            }
            return null;
        }
    }

    public class IfStatement : Statement
    {
        public Expression exp = null;
        public Statement stat;
        public IfStatement(Expression _exp, Statement _stat)
        {
            var type = _exp.CheckType();

            if (type != VarType.Bool && type != VarType.Undefined)
            {
                Console.WriteLine($"Type mismatch - expression in if should be bool type!");
                Settings.errors++;
            }
            exp = _exp;
            stat = _stat;
        }

        public override string GenCode(StreamWriter sw)
        {
            string et1 = String.Concat("et", Settings.number.ToString());
            Settings.IncreaseNumber();

            exp.GenCode(sw);
            Settings.EmitCode(sw, $"brfalse {et1}");
            stat.GenCode(sw);
            Settings.EmitCode(sw, $"{et1}: nop");

            return null;
        }
    }

    public class IfElseStatement : Statement
    {
        public Expression exp = null;
        public Statement stat;
        public Statement elseStat;

        public IfElseStatement(Expression _exp, Statement _stat, Statement _elsestat)
        {
            var type = _exp.CheckType();
            if (type != VarType.Bool && type != VarType.Undefined)
            {
                Console.WriteLine($"Type mismatch - expression in while should be bool type!");
                Settings.errors++;
            }
            exp = _exp;

            stat = _stat;
            elseStat = _elsestat;
        }

        public override string GenCode(StreamWriter sw)
        {
            string et1 = String.Concat("et", Settings.number.ToString()); Settings.IncreaseNumber();
            string et2 = String.Concat("et", Settings.number.ToString()); Settings.IncreaseNumber();

            exp.GenCode(sw);
            Settings.EmitCode(sw, $"brfalse {et1}");
            stat.GenCode(sw);
            Settings.EmitCode(sw, $"br {et2}");

            Settings.EmitCode(sw, $"{et1}: nop");
            elseStat.GenCode(sw);
            Settings.EmitCode(sw, $"{et2}: nop");

            return null;
        }
    }

    public class ReadStatement : Statement
    {
        public Number number;
        public ReadStatement(Number _number)
        {
            _number.CheckType();
            number = _number;
        }

        public override string GenCode(StreamWriter sw)
        {
            Settings.EmitCode(sw, "call string [mscorlib]System.Console::ReadLine()");
            if(number.varType == VarType.Double)
            {
                // Settings.EmitCode(sw, " call float64[mscorlib]System.Double::Parse(string)");
                Settings.EmitCode(sw, "call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
                Settings.EmitCode(sw, " call float64[mscorlib]System.Double::Parse(string, class [mscorlib]System.IFormatProvider)");               
                Settings.EmitCode(sw, $"stloc D_{number.name}");
            }
            if (number.varType == VarType.Bool)
            {
                Settings.EmitCode(sw, " call bool[mscorlib]System.Boolean::Parse(string)");
                Settings.EmitCode(sw, $"stloc B_{number.name}");
            }
            if (number.varType == VarType.Int)
            {
                Settings.EmitCode(sw, " call int32[mscorlib]System.Int32::Parse(string)");
                Settings.EmitCode(sw, $"stloc I_{number.name}");
            }
            return null;
        }

    }

    public class StatementStatement : Statement
    {
        public Expression exp = null;

        public StatementStatement(Expression _exp)
        {
            _exp.CheckType();
            exp = _exp;
        }

        public override string GenCode(StreamWriter sw)
        {
            string s = "";
            if(exp != null)
            {
                s = exp.GenCode(sw);

                Settings.EmitCode(sw, "pop");
            }
            return s;
        }
    }

    public class ListStatement : Statement
    {
        public List<Statement> statement;

        public ListStatement(List<Statement> _statement)
        {
            statement = _statement;
        }

        public override string GenCode(StreamWriter sw)
        {
            for(int i=statement.Count-1; i>=0;i--)
            {
                statement[i].GenCode(sw);
            }
            return null;
        }
    }
}
