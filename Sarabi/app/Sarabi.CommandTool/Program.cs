using System;
using log4net;
using log4net.Config;
using Neoworks.Utils;
using Neoworks.Utils.CommandLine;
[assembly: XmlConfigurator(Watch = true)]
namespace Sarabi.CommandTool
{
    class Program
    {
        private static ILog _log = LogManager.GetLogger(typeof (Program));

        static void Main(string[] args)
        {
            try
            {
                _log.Debug("Starting CommandTool");
                UnhandledExceptionUtil.Register();
                var parser = CommandLineParser2.Initialise(args);
                if (parser.ParseArguments(Console.Out))
                {
                    var commands = parser.ParseCommands(Console.Out);

                    if (commands != null)
                    {
                        RunCommands.Execute(commands, Console.Out);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                Console.ReadKey();
            }
            finally
            {
                _log.Debug("Exiting CommandTool");
            }
        }
    }
}
