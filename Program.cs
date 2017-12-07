using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using McMaster.Extensions.CommandLineUtils;

namespace Haiku
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication(false);
            app.Name = "haiku";
            app.HelpOption("-?|-h|--help");

            var haiku = new WebSite();

            // Entry Point
            app.OnExecute(() =>
                {
                    app.ShowHelp();
                    return 0;
                });

            // New Project command
            app.Command("new", (command) =>
                {
                    command.Description = "Creates a new project, optional folder path.";
                    command.HelpOption("-?|-h|--help");

                    var pathArgument = command.Argument("[path]", "New project location.");

                    command.OnExecute(() =>
                        {
                            var path = pathArgument.Value ?? "HaikuWebsite";

                            haiku.New(path);

                            return 0;
                        });

                });

            // Build Project command
            app.Command("build", (command) =>
                {
                    command.Description = "Builds existing project, optional folder path.";
                    command.HelpOption("-?|-h|--help");

                    var pathArgument = command.Argument("[path]", "Existing project location.");

                    command.OnExecute(() =>
                        {
                            var path = pathArgument.Value ?? Directory.GetCurrentDirectory();

                            haiku.Build(path);

                            return 0;
                        });
                });

            app.Execute(args);
        }
    }
}
