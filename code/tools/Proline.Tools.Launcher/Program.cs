using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Project.Online.Launcher
{

    public class Program
    {
        private static object _defaultMaxClients;

        public Program()
        {

        }

        public static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
            //System.Console.ReadKey();
        }

        private void Run()
        {
            FXServerConfig serviceConfigSection =
   ConfigurationManager.GetSection("fxServerConfig") as FXServerConfig;

            ResourceElement serviceConfig = serviceConfigSection.Resources[0];
            Console.WriteLine(serviceConfig.Name); 

            Console.WriteLine("FX Config Generated");
            var configDir = Path.Combine(Directory.GetCurrentDirectory(), "server.cfg");
            var fxConfig = BuildFXServerConfigString(serviceConfigSection);
            File.WriteAllText(configDir, fxConfig);

            Console.WriteLine("Launching Server...");
            LaunchServerInstance();
            Thread.Sleep(5000);
            Console.WriteLine("Launching Client...");
            LaunchClientInstance();
        }

        private void LaunchClientInstance()
        {
            //Process.Start("fivem://connect/localhost:30120");
        }

        private static void LaunchServerInstance()
        {
            var configDir = Path.Combine(Directory.GetCurrentDirectory(), "server.cfg");
            string serverDir = ConfigurationManager.AppSettings["FXServerDir"];
            var serverExeDir = Path.Combine(serverDir, "FXserver.exe");
            Process.Start(serverExeDir, "+exec " + configDir);
        }

        private static string BuildFXServerConfigString(FXServerConfig serviceConfigSection)
        {
            var startResources = "";
            for (int i = 0; i < serviceConfigSection.Resources.Count; i++)
            {
                startResources += $"start {serviceConfigSection.Resources[i].Name}" + (i + 1 < serviceConfigSection.Resources.Count ? "\n" : "");
            }


            var tags = @"sets tags """;
            for (int i = 0; i < serviceConfigSection.Tags.Count; i++)
            {
                tags += $"{serviceConfigSection.Tags[i].Name}" + (i + 1 < serviceConfigSection.Tags.Count ? "," : "");
            }
            tags += @"""";

            var principals = "";
            for (int i = 0; i < serviceConfigSection.Principals.Count; i++)
            {
                principals += $"add_principal identifier.{serviceConfigSection.Principals[i].Identifier} group.admin " + (i + 1 < serviceConfigSection.Principals.Count ? "\n" : "");
            }

            var groups = "";
            for (int i = 0; i < serviceConfigSection.Groups.Count; i++)
            {
                groups += $"add_ace group.{serviceConfigSection.Groups[i].Name} command.{serviceConfigSection.Groups[i].Type} {serviceConfigSection.Groups[i].Access} " +  (i + 1 < serviceConfigSection.Groups.Count ? "\n" : "");
            }

            var endPoints = @"endpoint_add_tcp ""0.0.0.0:30120""" + "\n" + @"endpoint_add_udp ""0.0.0.0:30120""";

            var serverIconPath = string.IsNullOrEmpty(ConfigurationManager.AppSettings["serverIcon"]) ? "#load_server_icon" : "load_server_icon " + ConfigurationManager.AppSettings["serverIcon"]; 
            var scriptHookAllowed = "sv_scriptHookAllowed " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["scriptHookAllowed"]) ? 0.ToString() : int.Parse(ConfigurationManager.AppSettings["scriptHookAllowed"]).ToString()); 
            var authMaxVariance = "sv_authMaxVariance " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["authMaxVariance"]) ? 0.ToString() : int.Parse(ConfigurationManager.AppSettings["authMaxVariance"]).ToString()); 
            var authMinTrust = "sv_authMinTrust " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["authMinTrust"]) ? 0.ToString() : int.Parse(ConfigurationManager.AppSettings["authMinTrust"]).ToString());
            var steamAPIKey = "set steam_webApiKey " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["steamAPIKey"]) ? @"""""" : ConfigurationManager.AppSettings["steamAPIKey"]) ;
            var licenceKey = "sv_licenseKey " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["licenceKey"]) ? @"""""" : ConfigurationManager.AppSettings["licenceKey"]);
            var projectName = "set sv_projectName " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["projectName"]) ? @"""""" : @"""" +  ConfigurationManager.AppSettings["projectName"]) + @"""";
            var projectDesc = "set sv_projectDesc " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["projectDesc"]) ? @"""""" : @"""" + ConfigurationManager.AppSettings["projectDesc"]) + @""""; 
            var enableVisability = string.IsNullOrEmpty(ConfigurationManager.AppSettings["isVisable"]) ? "" : (bool.Parse(ConfigurationManager.AppSettings["isVisable"]) ? "sv_master1 " : "#sv_master1 ") + @"""""";
            var serverName = "sv_hostname " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["serverName"]) ? "fxserver" : @"""" + ConfigurationManager.AppSettings["serverName"]) + @"""";
            var maxClients = "sv_maxclients " + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["maxClients"]) ? _defaultMaxClients.ToString() : int.Parse(ConfigurationManager.AppSettings["maxClients"]).ToString());
            var rconPassword = string.IsNullOrEmpty(ConfigurationManager.AppSettings["rconPassword"]) ? "" : $"rcon_password {ConfigurationManager.AppSettings["rconPassword"]}";


            var project = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{9}\n{10}\n{11}\n{12}",
                serverIconPath, scriptHookAllowed, authMaxVariance, authMinTrust, steamAPIKey, licenceKey, 
                projectName, projectDesc, enableVisability, serverName, maxClients, rconPassword, tags);
            var format = Path.Combine(Directory.GetCurrentDirectory(), "data", "fxserverformat.txt");
            return string.Format(File.ReadAllText(format), endPoints, startResources, project, groups, principals); 

        }
    }
}
