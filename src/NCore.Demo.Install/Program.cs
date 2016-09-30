using System.Diagnostics;
using NCore.Demo.Install.Helpers;

namespace NCore.Demo.Install
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Runner().Run();
            var process = Process.Start("RunMigrations.bat");
            process.WaitForExit();
            Process.Start("LastDBMigrate.log.txt");
        }
    }
}