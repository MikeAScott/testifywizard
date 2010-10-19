using System;
using System.IO;
using System.Reflection;
using ThoughtWorks.TreeSurgeon.Core;
using SQS.Testify.Core;

namespace SQS.Testify.TestifyConsole
{
	internal class TestifyConsoleMain
	{
		[STAThread]
		private static int Main(string[] args)
		{
			try
			{
				return RunApp(args);
			}
			catch (Exception e)
			{
				Console.WriteLine("Unhandled Exception thrown. Details follow: ");
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
				return -1;
			}
		}

        /// <summary>
        /// Run app via command line
        /// </summary>
        /// <param name="args">args[0] - Project Name, args[1] - Version (2005,2003)</param>
        /// <returns></returns>
		private static int RunApp(string[] args)
		{
			Console.WriteLine("Testify version 1.1");
            Console.WriteLine("Copyright (C) 2007 Bil Simser"); 
			Console.WriteLine("Copyright (C) 2005 Mike Roberts, ThoughtWorks, Inc");
			Console.WriteLine("Copyright (C) 2008 Mike Scott, SQS");
			Console.WriteLine();
            String wd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			if (args.Length < 1)
			{
				Usage();
				return -1;
			}
            if ("-i".Equals(args[0])) {
                if (args.Length <2) {
                    Usage();
                    return -1;
                }
                new TemplateInstaller(wd, Path.Combine(wd, "resources")).Install(args[1]);
                Console.WriteLine("Template " + args[1] + " installed");
                return 0;

            } else {
                string version = (args.Length > 1)?args[1]:"VS2008";
			    Console.WriteLine("Starting Tree Generation for " + args[0]);
			    Console.WriteLine();
			    string outputDirectory = new TreeSurgeonFrontEnd(wd, version).GenerateDevelopmentTree("Testify",args[0],version); 
			    Console.WriteLine("Tree Generation complete. Files can be found at " + outputDirectory);
			    return 0;
            }
		}

		private static void Usage()
		{
			Console.WriteLine("Creates a .NET Development tree");
			Console.WriteLine();
			Console.WriteLine("TestifyConsole projectName template");
            Console.WriteLine("or");
            Console.WriteLine("TestifyConsole -i template.zip (install a new template from a zip");
            Console.WriteLine();
			Console.WriteLine("Please note - project name must not contain spaces. We recommend you use CamelCase for project names.");
		}
	}
}