using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using Core.TheBlocksProblem;

namespace ConsoleRunner
{
    public class Program
    {
        public class Options
        {
            [Option('p', "problem", Required = true, HelpText = "The name of the problem to run.")]
            public string Problem { get; set; }

            [Option('o', "output", Required = true, HelpText = "The destination directory for the output files.")]
            public string OutputDirectory { get; set; }

        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.Problem == "TheBlocksProblem")
                    {
                        var theBlocksProblemSolution = new TheBlocksProblemSolution();
                        
                        var lines = GetTestFileLines();

                        var enumerable = theBlocksProblemSolution.GetSolution(lines);

                        File.WriteAllLines("c:\\temp\\output000.txt", enumerable);
                    }
                    else
                    {
                        Console.WriteLine("Nothing to run.");
                    }
                });
        }

        public static IEnumerable<string> GetTestFileLines()
        {
            var file = new System.IO.StreamReader(@"TheBlocksProblem\\input000.txt");

            string line;

            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
        }

    }
}
