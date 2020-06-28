using System;
using System.IO;
using System.Collections.Generic;
using GardensPoint;

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

        public ValueProperties(string _name, VarType _type,double _value)
        {
            name = _name;
            type = _type;
            value = _value;
        }
    }



    public class Compiler
    {

      //  public static int errors = 0;

        public static List<string> source;

       // public static Dictionary<string, (VarType, double)> varDictionary; 

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
            Console.WriteLine("program");
            Console.WriteLine("{");

            for (int i=0; i<list.Count;i++)
            {
                list[i].GenCode(sw);
            }
            for (int i=statList.Count-1; i>=0; i--)
            {
                statList[i].GenCode(sw);
            }
            Console.WriteLine("}");

        }
    }


    public static class Settings
    {
        public static int errors = 0;
        public static Dictionary<string, (VarType, double)> varDictionary = new Dictionary<string, (VarType, double)>();
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
                Console.WriteLine($"Variable with name {_name} already declared!");
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

            Settings.varDictionary.Add(name, (varType, value));
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
                    Console.WriteLine($"Variable with name {_varType} is not declared! - line: " + lineno.ToString());
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

        public override ValueProperties GetValue()
        {
            return new ValueProperties(name, varType, value);
        }

        public override string GenCode(StreamWriter sw)
        {
            if(varType == VarType.Int && !ident)
            {
                Settings.EmitCode(sw, $"ldc.i4.s {(int)value}");
            }
            else if (varType == VarType.Bool && !ident)
            {
                int val = value == 0 ? 0 : 1;
                Settings.EmitCode(sw, $"ldc.i4.{val}");
            }
            else if (varType == VarType.Double && !ident)
            {
                Settings.EmitCode(sw, $"ldc.r8 {value}");
            }
            else if(ident)
            {
                if (varType == VarType.Int)
                    Settings.EmitCode(sw, $"ldloca I_{name}");
                if (varType == VarType.Bool)
                    Settings.EmitCode(sw, $"ldloca B_{name}");
                if (varType == VarType.Double)
                    Settings.EmitCode(sw, $"ldloca D_{name}");
            }
            return null;
        }
    }

    public abstract class Expression
    {
        public abstract VarType CheckType();
        public abstract ValueProperties GetValue();
        public abstract string GenCode(StreamWriter sw);
    }


    public class ExpresionOperation : Expression
    {
        Expression LExp;
        Expression RExp;
        string operationType;
        int lineno;

        public ExpresionOperation(Expression _lexp, Expression _rexp, string _operationType, int _lineno)
        {
            LExp = _lexp;
            RExp = _rexp;
            operationType = _operationType;
            lineno = _lineno;
        }

        public override ValueProperties GetValue()
        {
            if (operationType == "UnarMinus")
            {
                ValueProperties vp = RExp.GetValue();
                return new ValueProperties(vp.name, vp.type, -vp.value);
            }
            if(operationType == "BitwiseNegation")
            {
                ValueProperties vp = RExp.GetValue();
                int val = ~((int)vp.value);
                return new ValueProperties(vp.name, vp.type, val);

            }
            if (operationType == "LogicalNegation")
            {
                ValueProperties vp = RExp.GetValue();
                bool val = !(vp.value == 0 ? false : true);
                return new ValueProperties(vp.name, VarType.Bool, val == false ? 0 : 1);

            }
            if (operationType == "IntConversion")
            {
                ValueProperties vp = RExp.GetValue();
                double val = Math.Truncate(vp.value);
                return new ValueProperties(vp.name, VarType.Int, val);
            }
            if (operationType == "DoubleConversion")
            {
                ValueProperties vp = RExp.GetValue();
                double val = Math.Truncate(vp.value);
                return new ValueProperties(vp.name, VarType.Double, val);
            }
            if (operationType == "LogicalOr" )
            {
                ValueProperties Lvp = LExp.GetValue();
                ValueProperties Rvp = RExp.GetValue();
                if(Lvp.type == VarType.Int && Rvp.type == VarType.Int)
                {
                    int lval = (int) Lvp.value;
                    int rval = (int)Rvp.value;

                    return new ValueProperties(null, VarType.Int, lval | rval);
                }
            }
            if (operationType == "LogicalAnd")
            {
                ValueProperties Lvp = LExp.GetValue();
                ValueProperties Rvp = RExp.GetValue();
                if (Lvp.type == VarType.Int && Rvp.type == VarType.Int)
                {
                    int lval = (int)Lvp.value;
                    int rval = (int)Rvp.value;

                    return new ValueProperties(null, VarType.Int, lval & rval);
                }
            }
            if (operationType == "Multiplies" || operationType == "Divides" || operationType == "Plus" || operationType == "Minus")
            {
                ValueProperties Lvp = LExp.GetValue();
                ValueProperties Rvp = RExp.GetValue();

                if (Lvp.type == VarType.Int && Rvp.type == VarType.Int)
                {
                    int lval = (int)Lvp.value;
                    int rval = (int)Rvp.value;
                    if(operationType == "Multiplies")
                        return new ValueProperties(null, VarType.Int, lval * rval);
                    if (operationType == "Divides")
                    {
                        if(rval == 0)
                        {
                            Console.WriteLine("Divide by 0 is not allowed!");
                            Settings.errors++;
                        }
                        else
                        {
                            return new ValueProperties(null, VarType.Int, lval / rval);
                        }
                    }
                    if (operationType == "Plus")
                        return new ValueProperties(null, VarType.Int, lval + rval);
                    if (operationType == "Minus")
                        return new ValueProperties(null, VarType.Int, lval - rval);

                }
                else if ((Lvp.type == VarType.Double || Lvp.type == VarType.Int) && (Rvp.type == VarType.Double || Rvp.type == VarType.Int))
                {
                    double lval = Lvp.value;
                    double rval = Rvp.value;
                    if (operationType == "Multiplies")
                        return new ValueProperties(null, VarType.Double, lval * rval);
                    if (operationType == "Divides")
                    {
                        if (rval == 0)
                        {
                            Console.WriteLine("Divide by 0 is not allowed!");
                            Settings.errors++;
                        }
                        else
                        {
                            return new ValueProperties(null, VarType.Double, lval / rval);
                        }
                    }
                    if (operationType == "Plus")
                        return new ValueProperties(null, VarType.Double, lval + rval);
                    if (operationType == "Minus")
                        return new ValueProperties(null, VarType.Double, lval - rval);
                }
            }
            if (operationType == "GreatherThan" || operationType == "LessThanOrEqual" || operationType == "LessThan" || operationType == "GreatherThanOrEqual")
            {
                ValueProperties Lvp = LExp.GetValue();
                ValueProperties Rvp = RExp.GetValue();
                if ((Lvp.type == VarType.Double || Lvp.type == VarType.Int) && (Rvp.type == VarType.Double || Rvp.type == VarType.Int))
                {
                    var lval = Lvp.value;
                    var rval = Rvp.value;

                    if (operationType == "GreatherThan")
                    {
                        double val = lval > rval ? 1 : 0;
                        return new ValueProperties(null, VarType.Bool, val);
                    }
                    if (operationType == "LessThanOrEqual")
                    {
                        double val = lval <= rval ? 1 : 0;
                        return new ValueProperties(null, VarType.Bool, val);
                    }
                    if (operationType == "LessThan")
                    {
                        double val = lval < rval ? 1 : 0;
                        return new ValueProperties(null, VarType.Bool, val);
                    }
                    if (operationType == "GreatherThanOrEqual")
                    {
                        double val = lval >= rval ? 1 : 0;
                        return new ValueProperties(null, VarType.Bool, val);
                    }
                }
            }
            if (operationType == "Equal" || operationType == "NotEqual")
            {
                ValueProperties Lvp = LExp.GetValue();
                ValueProperties Rvp = RExp.GetValue();
                if (((Lvp.type == VarType.Double || Lvp.type == VarType.Int) && (Rvp.type == VarType.Double || Rvp.type == VarType.Int)) || (Lvp.type == VarType.Bool && Rvp.type == VarType.Bool))
                {
                    var lval = Lvp.value;
                    var rval = Rvp.value;
                    if (operationType == "Equal")
                    {
                        double val = lval == rval ? 1 : 0;
                        return new ValueProperties(null, VarType.Bool, val);
                    }
                    if (operationType == "NotEqual")
                    {
                        double val = lval != rval ? 1 : 0;
                        return new ValueProperties(null, VarType.Bool, val);
                    }
                }
            }
            if (operationType == "Or" || operationType == "And")
            {
                ValueProperties Lvp = LExp.GetValue();
                ValueProperties Rvp = RExp.GetValue();
                if (Lvp.type == VarType.Bool && Rvp.type == VarType.Bool)
                {
                    bool lval = Lvp.value == 0? false : true ;
                    bool rval = Rvp.value == 0 ? false : true;
                    if(operationType == "Or")
                    {
                        double val = lval || rval ? 1 : 0;
                        return new ValueProperties(null, VarType.Bool, val);
                    }
                    if (operationType == "And")
                    {
                        double val = lval && rval ? 1 : 0;
                        return new ValueProperties(null, VarType.Bool, val);
                    }

                }
            }

            if (operationType == "Assign")
            {
                ValueProperties Lvp = LExp.GetValue();
                ValueProperties Rvp = RExp.GetValue();
                if ((Lvp.type == VarType.Double && (Rvp.type == VarType.Double || Rvp.type == VarType.Int)) || (Lvp.type == VarType.Int && Rvp.type == VarType.Int) || (Lvp.type == VarType.Bool && Rvp.type == VarType.Bool))
                {
                    if(!Settings.varDictionary.ContainsKey(Lvp.name))
                    {
                        Console.WriteLine("Cannot assign to non declared value!");
                        Settings.errors++;
                    }
                    else
                    {
                        Settings.varDictionary[Lvp.name] = (Lvp.type, Rvp.value);
                        return new ValueProperties(Lvp.name, Lvp.type, Rvp.value);
                    }
                }
            }
            return new ValueProperties(null, VarType.Undefined, 0);
        }


        public override string GenCode(StreamWriter sw)
        {
            if(operationType == "Assign")
            {
                RExp.GenCode(sw);
                var Lval = LExp.GetValue();
                //var Rval = RExp.GetValue();
                if (Lval.type == VarType.Bool)
                {
                    //int val = Rval.value == 0 ? 0 : 1;
                    //Settings.EmitCode(sw, $"ldc.i4.{val}");
                    Settings.EmitCode(sw, $"stloc.s B_{Lval.name}");
                }
                if (Lval.type == VarType.Double)
                {
                    //Settings.EmitCode(sw, $"ldc.r8 {Rval.value}");
                    Settings.EmitCode(sw, $"stloc.s D_{Lval.name}");
                }
                if (Lval.type == VarType.Int)
                {
                    //Settings.EmitCode(sw, $"ldc.i4.s {Rval.value}");
                    Settings.EmitCode(sw, $"stloc.s I_{Lval.name}");
                }
            }
            return null;
        }

        public override VarType CheckType()
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
                   // if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be int! - line: " + lineno.ToString());
                    }
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
                    //if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be int or double! - line: " + lineno.ToString());
                    }
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
                    //if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be int! - line: " + lineno.ToString());
                    }
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
                   // if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be bool! - line: " + lineno.ToString());
                    }
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
                   // if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be bool, int or double! - line: " + lineno.ToString());
                    }
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
                   // if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be bool, int or double! - line: " + lineno.ToString());
                    }
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
                    //if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be int or double! - line: " + lineno.ToString());
                    }
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
                    //if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be int or double! - line: " + lineno.ToString());
                    }
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
                   // if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be bool, int or double! - line: " + lineno.ToString());
                    }
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
                    //if (!Settings.linenumbers.Contains(lineno))
                    {
                        Settings.linenumbers.Add(lineno);
                        Console.WriteLine($"Type mismatch {operationType} - both should be bool! - line: " + lineno.ToString());
                    }
                    Settings.errors++;
                }
                return VarType.Undefined;
            }

            if (operationType == "Assign")
            {
                VarType RExpCheckType = RExp.CheckType();
                VarType LExpCheckType = LExp.CheckType();
                if (LExpCheckType == VarType.Double && (RExpCheckType == VarType.Double ))
                {
                    return VarType.Double;
                }
                if( (LExpCheckType == VarType.Int && RExpCheckType == VarType.Int) || (RExpCheckType == VarType.Double && RExp.GetValue().value==0))
                {
                    return VarType.Int;
                }
                if (LExpCheckType == VarType.Bool && RExpCheckType == VarType.Bool)
                {
                    return VarType.Bool;
                }


               // if (!Settings.linenumbers.Contains(lineno))
                {
                    Settings.linenumbers.Add(lineno);
                    Console.WriteLine($"Type mismatch {operationType} - cannot assign two different types! - line: " + lineno.ToString());
                }
                Settings.errors++;
                
                return VarType.Undefined;
            }

            return VarType.Undefined;

        }
    }

    public abstract class Statement
    {
        public abstract void GenCode(StreamWriter sw);
    }

    public class EmptyStatement : Statement
    {
        public override void GenCode(StreamWriter sw) {  }

    }

    public class ReturnStatement : Statement
    {
        public override void GenCode(StreamWriter sw)
        {
            Settings.EmitCode(sw, "leave EndMain");
              
        }
    }

    public class WhileStatement : Statement
    {
        public Expression exp;
        public Statement stat;

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

        public override void GenCode(StreamWriter sw) { }

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

        public override void GenCode(StreamWriter sw)
        {
            if(str != null)
            {
                Settings.EmitCode(sw, $"ldstr {str}");                
                Settings.EmitCode(sw, "call void [mscorlib]System.Console::Write(string)");
            }
            else if(exp != null)
            {
                var value = exp.GetValue();
                if (value.type == VarType.Double)
                {
                    var culture = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.000000}", value.value);
                    Settings.EmitCode(sw, $"ldstr \"{culture}\"");
                    Settings.EmitCode(sw, "call void [mscorlib]System.Console::Write(string)");

                }
                if (value.type == VarType.Bool)
                {
                    string val = value.value == 0 ? "False" : "True";
                    Settings.EmitCode(sw, $"ldstr \"{val}\"");
                    Settings.EmitCode(sw, "call void [mscorlib]System.Console::Write(string)");
                }
                if (value.type == VarType.Int)
                {
                    int val = (int)value.value;
                    Settings.EmitCode(sw, $"ldstr \"{val.ToString()}\"");
                    Settings.EmitCode(sw, "call void [mscorlib]System.Console::Write(string)");
                }
            }
        }
    }

    public class IfStatement : Statement
    {
        public Expression exp = null;
        public Statement stat;
        public IfStatement(Expression _exp, Statement _stat)
        {
            _exp.CheckType();

            exp = _exp;
            stat = _stat;
        }

        public override void GenCode(StreamWriter sw) { }

    }

    public class IfElseStatement : Statement
    {
        public Expression exp = null;
        public Statement stat;
        public Statement elseStat;

        public IfElseStatement(Expression _exp, Statement _stat, Statement _elsestat)
        {
            _exp.CheckType();

            exp = _exp;
            stat = _stat;
            elseStat = _elsestat;
        }

        public override void GenCode(StreamWriter sw) { }

    }

    public class ReadStatement : Statement
    {
        public Number number;
        public ReadStatement(Number _number)
        {
            _number.CheckType();

            number = _number;
        }

        public override void GenCode(StreamWriter sw) { }

    }

    public class StatementStatement : Statement
    {
       // public Statement statement = null;
        public Expression exp = null;

        //public StatementStatement(Statement _statement)
        //{
        //    statement = _statement;
        //}
        public StatementStatement(Expression _exp)
        {
            _exp.CheckType();
            exp = _exp;
        }

        public override void GenCode(StreamWriter sw)
        {
            if(exp != null)
            {
                exp.GenCode(sw);
            }
        }

    }

    public class ListStatement : Statement
    {
        public List<Statement> statement;

        public ListStatement(List<Statement> _statement)
        {
            statement = _statement;
        }

        public override void GenCode(StreamWriter sw)
        {

        }


    }
}
