using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

namespace PBRTSharp
{

    internal class PBRTOptions
    {
        public readonly int NumberOfRenderThreads;
        public readonly bool QuickRender;
        public readonly bool QuietRender;
        public readonly bool Verbose;
        public readonly FileInfo DestinationFile;
        public PBRTOptions(int NumberOfRenderThreads, bool QuickRender, bool QuietRender, bool Verbose, FileInfo DestinationFile)
        {
            this.NumberOfRenderThreads = NumberOfRenderThreads;
            this.QuickRender = QuickRender;
            this.QuietRender = QuietRender;
            this.Verbose = Verbose;
            this.DestinationFile = DestinationFile;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                new Option(new[]{"--nthreads", "--ncores" }, $"Number of rendering threads (default {Environment.ProcessorCount - 1})")
                {
                    Argument = new Argument<int>(getDefaultValue: () => Environment.ProcessorCount - 1)
                },
                new Option("--outfile", "The destination image location")
                {
                    Argument = new Argument<FileInfo>(getDefaultValue: ()=> new FileInfo("./out.png")),
                },
                new Option("--quiet", "Suppress log information")
                {
                    Argument = new Argument<bool>(getDefaultValue : ()=> false)
                },
                new Option("--verbose", "Display extra log information")
                {
                    Argument = new Argument<bool>(getDefaultValue : ()=> false)
                },
                new Option("--quick", "Quick render")
                {
                    Argument = new Argument<bool>(getDefaultValue : ()=> false)
                },
                new Argument<IEnumerable<FileInfo>>("filenames")
            };

            PBRTOptions pbrtOptions;
            IEnumerable<FileInfo> inputFilenames;

            rootCommand.Handler = CommandHandler.Create<int, FileInfo, bool, bool, bool, IEnumerable<FileInfo>>((nthreads, outfile, quiet, verbose, quick, filenames) =>
            {
                inputFilenames = filenames;
                pbrtOptions = new PBRTOptions(
                    NumberOfRenderThreads: nthreads,
                    QuickRender: quick,
                    QuietRender: quiet,
                    Verbose: verbose,
                    DestinationFile: outfile
                );
            });

            // Parse the incoming args and invoke the handler
            _ = rootCommand.Invoke(args);
        }
    }
}
