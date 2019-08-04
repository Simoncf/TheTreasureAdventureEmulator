using AtlasCopco.Integration.Maze;
using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TreasureAdventure.Businesslogic.Configuration
{
    public class DiBuinessLogic: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);


          

            //var assemblies = new List<Assembly>();
            //assemblies.AddRange(
            //    Directory.EnumerateFiles(Directory.GetCurrentDirectory(), 
            //    "*.dll", SearchOption.AllDirectories)
            //        .Select(Assembly.LoadFrom)
            //);

            //foreach (var assembly in assemblies)
            //{
            //    builder.RegisterAssemblyTypes(assembly)
            //        .Where(t => t.GetInterfaces()
            //        .Any(i => i.IsAssignableFrom(typeof(IMazeIntegration))))
            //        .AsImplementedInterfaces()
            //        .InstancePerRequest();
            //}
            //builder.RegisterType<MazeEmulator>().As<IMazeEmulator>();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
        }
    }
}
