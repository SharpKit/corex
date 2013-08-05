﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corex.IO;
using Corex.IO.Tools;

namespace CorexDemo
{
    class Program
    {



        static void foo()
        {
            FsPath xx = "C:\\";
            var xxx = xx + "abc" + "def\\";

        }
        static void Main(string[] args)
        {
            foo();
            var args2 = new string[] { "/Name:Hello" };
            var tool = new ToolArgsInfo<Program>();
            var x = tool.Parse(args2);
            Console.WriteLine(tool.Generate(x));
            Console.WriteLine(tool.GenerateHelp());
        }
        public string Name { get; set; }
    }

    class MyAppArgs
    {
        public string Command { get; set; }
        public string Name { get; set; }
    }
}
