﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Corex.IO.Tools
{
    public class Tool
    {
        public Process Process { get; set; }
        public string Filename { get; set; }
        public string Args { get; set; }
        public void Run()
        {
            var sb = new StringBuilder();
            Action<string> Log = t =>
                {
                    Console.WriteLine(t);
                    sb.AppendLine(t);
                };
            Process = new Process
            {
                StartInfo = new ProcessStartInfo
                    {
                        FileName = Filename,
                        Arguments = Args,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
            };
            Process.StartInfo.RedirectStandardError = true;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.Start();
            Process.BeginOutputReadLine();
            Process.BeginErrorReadLine();
            Process.OutputDataReceived += (s, e) => Log(e.Data);
            Process.ErrorDataReceived += (s, e) => Log(e.Data);
            Process.WaitForExit();
            if (Process.ExitCode != 0)
            {
                sb.Insert(0, "Exit code=" + Process.ExitCode + "\n");
                var s = sb.ToString();
                //File.AppendAllText(@"c:\temp\Tool.log", s);
                throw new Exception(s);
            }
        }

    }

    public class Tool<T> where T : class, new()
    {
        public Tool()
        {
            InnerTool = new Tool();
        }
        public Tool InnerTool { get; set; }
        public string Filename { get; set; }
        public T Args { get; set; }
        public Process Process { get; set; }
        public void Run()
        {
            var ser = new ToolArgsSerializer<T>();
            if (Args != null)
                InnerTool.Args = ser.Write(Args);
            InnerTool.Filename = Filename;
            InnerTool.Run();
            Process = InnerTool.Process;

        }
    }

}
