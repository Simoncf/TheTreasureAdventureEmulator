using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureAdventure.Businesslogic.Configuration;

namespace TheTreasureAdventureEmulator
{
    public class Startup
    {



        // public IContainer ApplicationContainer { get; private set; }

        public IContainer ConfigureAutoFac()
        {
            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DiBuinessLogic>();
           // containerBuilder.Populate(services);
           // this.ApplicationContainer = containerBuilder.Build();
      
           // var serviceProvider = new AutofacServiceProvider(this.ApplicationContainer);

            return containerBuilder.Build();
        }
    }
}
