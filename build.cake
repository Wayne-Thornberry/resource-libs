
var target = Argument("target", "Deploy");
var platform = "bin";
var deployment = "Release"; 
var configuration = Argument("configuration", $"{deployment}");
var version = Argument("packageVersion", "0.0.1");
var prerelease = Argument("prerelease", "");

var commonDir = "./code/common";
var libDir = "./code/libs";
var componentDir = "./code/components";
var toolsDir = "./code/tools";

var clientResourcePath = "./code/resources/client";
var serverResourcePath = "./code/resources/server";

var clientModulesPath = "./ext/modules/client";
var serverModulesPath = "./ext/modules/server";

var levelscriptDir = "./ext/levelscripts";

var clientResourceDataDir = "./data/resources/client";
var serverResourceDataDir = "./data/resources/server";
var clientModuleDataDir = "./data/modules/client";
var serverModuleDataDir = "./data/modules/server";


// a full build would be to build the common first, libs second, resources third, components fourth, tools fifth

var deployDir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"/ProjectOnline");
var resourceOutputDir = $"{deployDir}/resources";
var toolsOutputDir = $"{deployDir}/tools";
var artificatsOutputDir = "./artifacts";

//Information("Dependent on ResourceFramework");
//CakeExecuteScript($"{libDir}/{resourceFramework}/build.cake"); 


class ProjectInformation
{
    public int ProjectType {get; set;}
    public string OutputDir {get ;set;}
    public string Name { get; set; }
    public string FullPath { get; set; }
    public bool IsTestProject { get; set; }
}

string packageVersion;
string outPutDir;
string resourceName;

ProjectInformation serverLauncher;
ProjectInformation clientModulesPack;
ProjectInformation serverModulesPack;
ProjectInformation levelScriptsPack;
ProjectInformation clientCore;
ProjectInformation serverCore;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////


CakeExecuteScript($"./code/libs/ResourceCommon/build.cake"); 
CakeExecuteScript($"./code/libs/ResourceClient/build.cake"); 
CakeExecuteScript($"./code/libs/ResourceServer/build.cake"); 
CakeExecuteScript($"./code/tools/ServerLauncher/build.cake"); 
CakeExecuteScript($"./code/resources/client/Client-Core/build.cake"); 
CakeExecuteScript($"./code/resources/server/Server-Core/build.cake"); 
CakeExecuteScript($"./ext/modules/client/Client-Modules/build.cake"); // a script should exist here to build and compile all modules into one package
CakeExecuteScript($"./ext/modules/server/Server-Modules/build.cake"); // a script should exist here to build and compile all modules into one package
CakeExecuteScript($"./ext/levelscripts/ClassicScripts/build.cake");  // a script should exist here to build and compile all modules into one package




Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");

    packageVersion = $"{version}{prerelease}"; 
    var dir = Context.Environment.WorkingDirectory;
    resourceName = dir.GetDirectoryName();

    outPutDir = $"{artificatsOutputDir}/"+dir.GetDirectoryName();

    
    var serverLauncherPath = toolsDir+"/ServerLauncher";

    var clientCorePath = clientResourcePath+"/Client-Core";
    var serverCorePath = serverResourcePath+"/Server-Core";

    var clientModulesCPath =  clientModulesPath+"/Client-Modules";
    var serverModulesCPath =  serverModulesPath+"/Server-Modules";

    var levelscripts = levelscriptDir+"/ClassicScripts";

    
    var serverLauncherDirPath = new DirectoryPath(serverLauncherPath);

    var clientCoreDirPath = new DirectoryPath(clientCorePath);
    var serverCoreDirPath = new DirectoryPath(serverCorePath);
    
    var clientModulesDirPath = new DirectoryPath(clientModulesCPath);
    var serverModulesDirPath = new DirectoryPath(serverModulesCPath);
 
    var levelScriptDirPath = new DirectoryPath(levelscripts);

        // We need to deploy the core parts of PO, server and client first
    serverLauncher = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+serverLauncherDirPath.GetDirectoryName(),
        Name = serverLauncherDirPath.GetDirectoryName(),
        FullPath = serverLauncherPath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(serverLauncher.Name);
    Information(serverLauncher.FullPath);
    Information(serverLauncher.OutputDir);

    // We need to deploy the core parts of PO, server and client first
    clientCore = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+clientCoreDirPath.GetDirectoryName(),
        Name = clientCoreDirPath.GetDirectoryName(),
        FullPath = clientCorePath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(clientCore.Name);
    Information(clientCore.FullPath);
    Information(clientCore.OutputDir);

    serverCore = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+serverCoreDirPath.GetDirectoryName(),
        Name = serverCoreDirPath.GetDirectoryName(),
        FullPath = serverCorePath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(serverCore.Name);
    Information(serverCore.FullPath);
    Information(serverCore.OutputDir);

    // We can then deploy the modules
    clientModulesPack = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+clientModulesDirPath.GetDirectoryName(),
        Name = clientModulesDirPath.GetDirectoryName(),
        FullPath = clientModulesCPath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(clientModulesPack.Name);
    Information(clientModulesPack.FullPath);
    Information(clientModulesPack.OutputDir);

    serverModulesPack = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+serverModulesDirPath.GetDirectoryName(),
        Name = serverModulesDirPath.GetDirectoryName(),
        FullPath = serverModulesCPath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(serverModulesPack.Name);
    Information(serverModulesPack.FullPath);
    Information(serverModulesPack.OutputDir);

    levelScriptsPack = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+levelScriptDirPath.GetDirectoryName(),
        Name = levelScriptDirPath.GetDirectoryName(),
        FullPath = levelscripts,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(levelScriptsPack.Name);
    Information(levelScriptsPack.FullPath);
    Information(levelScriptsPack.OutputDir);
    
});
 
Task("Clean") 
    //.WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
	    // Information("Cleaning " + outPutDir);
        // CleanDirectory(outPutDir);
});

Task("Deploy")
    .IsDependentOn("Clean")
    .Does(() =>
{ 
            var clientResourceDeployDir = $"{resourceOutputDir}/client-core";
            var serverResourceDeployDir = $"{resourceOutputDir}/server-core";
            CleanDirectory(deployDir);
	        Information("Cleaned " + deployDir);
 
            if(DirectoryExists($"{artificatsOutputDir}/{serverLauncher.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{serverLauncher.Name}", deployDir); 
                Information($"Copied {artificatsOutputDir}/{serverLauncher.Name}" + " To " + deployDir); 
            }

             // Copy any working data files to the resource, data should probably be its own repo
            if(DirectoryExists($"{artificatsOutputDir}/{clientCore.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{clientCore.Name}", clientResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{clientCore.Name}" + " To " + clientResourceDeployDir); 
            }
            if(DirectoryExists($"{artificatsOutputDir}/{serverCore.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{serverCore.Name}", serverResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{serverCore.Name}" + " To " + serverResourceDeployDir); 
            }

             if(DirectoryExists($"{artificatsOutputDir}/{clientModulesPack.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{clientModulesPack.Name}", clientResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{clientModulesPack.Name}" + " To " + clientResourceDeployDir); 
            }
              if(DirectoryExists($"{artificatsOutputDir}/{serverModulesPack.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{serverModulesPack.Name}", serverResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{serverModulesPack.Name}" + " To " + serverResourceDeployDir); 
            }


             if(DirectoryExists($"{artificatsOutputDir}/{levelScriptsPack.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{levelScriptsPack.Name}", clientResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{levelScriptsPack.Name}" + " To " + clientResourceDeployDir); 
            }

            
            // Copy any working data files to the resource, data should probably be its own repo
            if(DirectoryExists($"{clientResourceDataDir}/{clientCore.Name}"))
            { 
                CopyDirectory($"{clientResourceDataDir}/{clientCore.Name}", clientResourceDeployDir); 
	            Information($"Copied {clientResourceDataDir}/{clientCore.Name}" + " To " + clientResourceDeployDir);
            }
            if(DirectoryExists($"{serverResourceDataDir}/{serverCore.Name}"))
            { 
                CopyDirectory($"{serverResourceDataDir}/{serverCore.Name}", serverResourceDeployDir); 
	            Information($"Copied {serverResourceDataDir}/{serverCore.Name}" + " To " + serverResourceDeployDir);
            }

             if(DirectoryExists($"{clientModuleDataDir}/{clientModulesPack.Name}"))
            { 
                CopyDirectory($"{clientModuleDataDir}/{clientModulesPack.Name}", clientResourceDeployDir); 
	            Information($"Copied {clientModuleDataDir}/{clientModulesPack.Name}" + " To " + clientResourceDeployDir);
            }
             if(DirectoryExists($"{serverModuleDataDir}/{serverModulesPack.Name}"))
            { 
                CopyDirectory($"{serverModuleDataDir}/{serverModulesPack.Name}", serverResourceDeployDir); 
	            Information($"Copied {serverModuleDataDir}/{serverModulesPack.Name}" + " To " + serverResourceDeployDir);
            }
 
            // Delete this to avoid causing issues with loading and running the .net libraries
            DeleteFiles(clientResourceDeployDir + "/CitizenFX.Core.*.dll");
	        Information($"Deleted " + clientResourceDeployDir + "/CitizenFX.Core.*.dll");

            
            DeleteFiles(serverResourceDeployDir + "/CitizenFX.Core.*.dll");
	        Information($"Deleted " + serverResourceDeployDir + "/CitizenFX.Core.*.dll");
});

// //////////////////////////////////////////////////////////////////////
// // EXECUTION
// //////////////////////////////////////////////////////////////////////

RunTarget(target);