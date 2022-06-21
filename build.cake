
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
var resourcePath = "./code/resources"; 
var modulesPath = "./ext/modules";
var levelscriptDir = "./ext/levelscripts";

var resourceDataDir = "./data/resources"; 
var moduleDataDir = "./data/modules"; 


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
ProjectInformation modulesPack; 
ProjectInformation levelScriptsPack;
ProjectInformation core; 

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

var serverArgs = new Dictionary<string,string>();
serverArgs.Add("configuration", "Server") ;
var clientArgs = new Dictionary<string,string>();
clientArgs.Add("configuration", "Client") ;

CakeExecuteScript($"./code/libs/ResourceLibs/build.cake", new CakeSettings(){ Arguments = clientArgs}); 
CakeExecuteScript($"./code/libs/ResourceLibs/build.cake", new CakeSettings(){ Arguments = serverArgs}); 
CakeExecuteScript($"./code/libs/ModuleFramework/build.cake",  new CakeSettings(){ Arguments = clientArgs}); 
CakeExecuteScript($"./code/libs/ModuleFramework/build.cake",  new CakeSettings(){ Arguments = serverArgs}); 
CakeExecuteScript($"./code/tools/ServerLauncher/build.cake"); 
CakeExecuteScript($"./code/resources/ProlineCore/build.cake",  new CakeSettings(){ Arguments = clientArgs});  
CakeExecuteScript($"./code/resources/ProlineCore/build.cake",  new CakeSettings(){ Arguments = serverArgs});  
CakeExecuteScript($"./ext/modules/ClassicModules/build.cake",  new CakeSettings(){ Arguments = clientArgs}); // a script should exist here to build and compile all modules into one package 
//CakeExecuteScript($"./ext/modules/ClassicModules/build.cake",  new CakeSettings(){ Arguments = serverArgs}); // a script should exist here to build and compile all modules into one package 
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
    var corePath = resourcePath+"/ProlineCore";  
    var modulesCPath =  modulesPath+"/ClassicModules";  
    var levelscripts = levelscriptDir+"/ClassicScripts";

    
    var serverLauncherDirPath = new DirectoryPath(serverLauncherPath);
    var coreDirPath = new DirectoryPath(corePath); 
    var modulesDirPath = new DirectoryPath(modulesCPath);  
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
    core = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+coreDirPath.GetDirectoryName(),
        Name = coreDirPath.GetDirectoryName(),
        FullPath = corePath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(core.Name);
    Information(core.FullPath);
    Information(core.OutputDir);

    // We can then deploy the modules
    modulesPack = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+modulesDirPath.GetDirectoryName(),
        Name = modulesDirPath.GetDirectoryName(),
        FullPath = modulesCPath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(modulesPack.Name);
    Information(modulesPack.FullPath);
    Information(modulesPack.OutputDir); 

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
            if(DirectoryExists($"{artificatsOutputDir}/{core.Name}/client"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{core.Name}/client", clientResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{core.Name}/client" + " To " + clientResourceDeployDir); 
            }
            if(DirectoryExists($"{artificatsOutputDir}/{core.Name}/server"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{core.Name}/server", serverResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{core.Name}/server" + " To " + serverResourceDeployDir); 
            }

             if(DirectoryExists($"{artificatsOutputDir}/{modulesPack.Name}/client"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{modulesPack.Name}/client", clientResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{modulesPack.Name}/client" + " To " + clientResourceDeployDir); 
            }
            if(DirectoryExists($"{artificatsOutputDir}/{modulesPack.Name}/server"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{modulesPack.Name}/server", serverResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{modulesPack.Name}/server" + " To " + serverResourceDeployDir); 
            }


             if(DirectoryExists($"{artificatsOutputDir}/{levelScriptsPack.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{levelScriptsPack.Name}", clientResourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{levelScriptsPack.Name}" + " To " + clientResourceDeployDir); 
            }

            
            // Copy any working data files to the resource, data should probably be its own repo
            if(DirectoryExists($"{resourceDataDir}/{core.Name}/client"))
            { 
                CopyDirectory($"{resourceDataDir}/{core.Name}/client", clientResourceDeployDir); 
	            Information($"Copied {resourceDataDir}/{core.Name}/client" + " To " + clientResourceDeployDir);
            }
            if(DirectoryExists($"{resourceDataDir}/{core.Name}/server"))
            { 
                CopyDirectory($"{resourceDataDir}/{core.Name}/server", serverResourceDeployDir); 
	            Information($"Copied {resourceDataDir}/{core.Name}/server" + " To " + serverResourceDeployDir);
            }

             if(DirectoryExists($"{moduleDataDir}/{modulesPack.Name}/client"))
            { 
                CopyDirectory($"{moduleDataDir}/{modulesPack.Name}/client", clientResourceDeployDir); 
	            Information($"Copied {moduleDataDir}/{modulesPack.Name}/client" + " To " + clientResourceDeployDir);
            }
             if(DirectoryExists($"{moduleDataDir}/{modulesPack.Name}/server"))
            { 
                CopyDirectory($"{moduleDataDir}/{modulesPack.Name}/server", serverResourceDeployDir); 
	            Information($"Copied {moduleDataDir}/{modulesPack.Name}/server" + " To " + serverResourceDeployDir);
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