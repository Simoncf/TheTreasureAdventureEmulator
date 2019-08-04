using Autofac;
using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreasureAdventure.Businesslogic;

namespace TheTreasureAdventureEmulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Thread to get (Escape) to exit application at anytime
            var closeConsole = new Thread(StartEmulator);
            closeConsole.SetApartmentState(ApartmentState.STA);
            closeConsole.Start();

            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                // do nothing until escape
            }

            Environment.Exit(0);

        }

        private static void StartEmulator()
        {
            // Autofac
            Startup startup = new Startup();
            var container =  startup.ConfigureAutoFac();
            
            using (var scope = container.BeginLifetimeScope())
            {
                var consolerEmulator = scope.Resolve<IMazeEmulator>();
                consolerEmulator.MainEmulate();
            }
        }
    }
}
