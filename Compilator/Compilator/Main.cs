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
        Bool = 2
    }
    public abstract class SyntaxTree
    {
        public char type;
        public int line = -1;
        public abstract char CheckType();
        public abstract string GenCode();
    }

    public class ListNode
    {
        public string name;
        public VarType type;

        public ListNode(string _name, VarType _type)
        {
            name = _name;
            type = _type;
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
            Scanner scanner = new Scanner(source);
            Parser parser = new Parser(scanner);
            Console.WriteLine();

            sw = new StreamWriter(file + ".il");
            GenProlog();
            bool canParse = parser.Parse();
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
            EmitCode(".locals init ( float64 temp )");
            for (char c = 'a'; c <= 'z'; ++c)
            {
                EmitCode($".locals init ( int32 _i{c} )");
                EmitCode($".locals init ( float64 _r{c} )");
            }
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
        List<Declarations> list;

        public Program(List<Declarations> _list)
        {
            list = _list;
        }
    }


    public static class Settings
    {
        public static int errors = 0;
        public static Dictionary<string, (VarType, double)> varDictionary = new Dictionary<string, (VarType, double)>();
    }

    public class Declarations
    {
        public string name;
        public VarType varType;
        public double value;

        public Declarations(string _name, int _varType, double _value = 0)
        {
            if (Settings.varDictionary.ContainsKey(_name))
            {
                Console.WriteLine($"Variable with name {_name} already declared!");
                Settings.errors++;
                return;
            }

            name = _name;
            if(_varType == 1)
                varType = VarType.Double;
            if (_varType == 2)
                varType = VarType.Bool;
            if (_varType == 0)
                varType = VarType.Int;

            Settings.varDictionary.Add(name, (varType, value));
        }



    }

    public class Number
    {
        public VarType varType;
        public double value;

        public Number (string _varType, string _value = "")
        {
            if (_varType == "0")
                varType = VarType.Int;
            if (_varType == "1")
                varType = VarType.Double;
            if (_varType == "2")
                varType = VarType.Bool;
            else
            {
                if (!Settings.varDictionary.ContainsKey(_varType))
                {
                    Console.WriteLine($"Variable with name {_varType} is not declared!");
                    Settings.errors++;
                    return;
                }

                varType = Settings.varDictionary[_varType].Item1;
                varType = Settings.varDictionary[_varType].Item1;
                value = Settings.varDictionary[_varType].Item2;
                return;
            }

            value = Double.Parse(_value);
        }
    }

    public abstract class Expression
    {

    }

    public class SimpleOperation : Expression
    {
        Expression LExp;
        Expression RExp;

        public SimpleOperation(Number num)
        {
        }

    }

    public class Operation : Expression
    {
        Expression LExp;
        Expression RExp;
        string operationType;

        public Operation(Number num, Expression _lexp, Expression _rexp, string _operationType) 
        {
            LExp = _lexp;
            RExp = _rexp;
            operationType = _operationType;
        }


    }
}
