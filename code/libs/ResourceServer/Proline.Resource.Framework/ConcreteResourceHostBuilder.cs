using System;
using System.Collections.Generic;
using System.Reflection;

namespace Proline.Resource.Framework
{
    internal class ConcreteResourceHostBuilder : IResourceHostBuilder
    {

        public ConcreteResourceHostBuilder()
        {

        }

        public IResourceHost Build()
        {
            //var sourceAssembly = Assembly.GetCallingAssembly();

            //var apiManager = new APIManager(); 

            //var env = new ResourceEnvironment();
            //var app = new ResourceApplication();


            //var startupFullName = (string)_properties["Startup"];
            //var startupType = sourceAssembly.GetType(startupFullName);
            //var startup = Activator.CreateInstance(startupType);

            //var configServicesMethod = startupType.GetMethod("ConfigureServices");
            //var configMethod = startupType.GetMethod("Configure");

            //var service = new ResourceService();

            //configServicesMethod.Invoke(startup, new object[] { service });
            //configMethod.Invoke(startup, new object[] { app, env });

            return new ResourceHost();
        }

        public IResourceHostBuilder UseSetting(string key, string value)
        {
            //_properties.Add(key, value);
            //return this;
            return null;
        }
    }
}