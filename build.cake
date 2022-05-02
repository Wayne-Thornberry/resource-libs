
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
var resourceDir = "./code/resources/client";
var modulesDir = "./ext/modules/client";
var levelscriptDir = "./ext/levelscripts";


var resourceDataDir = "./data/resources/client";
var moduleDataDir = "./data/modules/client";


// a full build would be to build the common first, libs second, resources third, components fourth, tools fifth

var deployDir = "E:/servers/Game_Servers/FiveM/core";
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

ProjectInformation modulesPack;
ProjectInformation levelScriptsPack;
ProjectInformation resourceLoaderPack;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////


CakeExecuteScript($"./code/libs/ResourceClient/build.cake"); 
CakeExecuteScript($"./code/resources/client/ModualLoader/build.cake"); 
CakeExecuteScript($"./ext/modules/client/ClassicOnline/build.cake"); 
CakeExecuteScript($"./ext/levelscripts/ClassicScripts/build.cake"); 




Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");

    packageVersion = $"{version}{prerelease}"; 
    var dir = Context.Environment.WorkingDirectory;
    resourceName = dir.GetDirectoryName();

    outPutDir = $"{artificatsOutputDir}/"+dir.GetDirectoryName();

    var moduleLoader = resourceDir+"/ModualLoader";
    var classicOnline =  modulesDir+"/ClassicOnline";
    var levelscripts = levelscriptDir+"/ClassicScripts";

    var dirPaht = new DirectoryPath(moduleLoader);
    var dirPaht2 = new DirectoryPath(classicOnline);
    var dirPaht3 = new DirectoryPath(levelscripts);


    resourceLoaderPack = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+dirPaht.GetDirectoryName(),
        Name = dirPaht.GetDirectoryName(),
        FullPath = moduleLoader,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(resourceLoaderPack.Name);
    Information(resourceLoaderPack.FullPath);
    Information(resourceLoaderPack.OutputDir);

    modulesPack = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+dirPaht2.GetDirectoryName(),
        Name = dirPaht2.GetDirectoryName(),
        FullPath = classicOnline,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(modulesPack.Name);
    Information(modulesPack.FullPath);
    Information(modulesPack.OutputDir);

    levelScriptsPack = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+dirPaht3.GetDirectoryName(),
        Name = dirPaht3.GetDirectoryName(),
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
	    Information("Cleaning " + outPutDir);
        CleanDirectory(outPutDir);
});

Task("Deploy")
    .IsDependentOn("Clean")
    .Does(() =>
{ 
            var resourceDeployDir = $"{resourceOutputDir}/{resourceName}";
            CleanDirectory(resourceDeployDir);
	        Information("Cleaned " + resourceDeployDir);
 

             // Copy any working data files to the resource, data should probably be its own repo
            if(DirectoryExists($"{artificatsOutputDir}/{resourceLoaderPack.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{resourceLoaderPack.Name}", resourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{resourceLoaderPack.Name}" + " To " + resourceDeployDir); 
            }

             if(DirectoryExists($"{artificatsOutputDir}/{modulesPack.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{modulesPack.Name}", resourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{modulesPack.Name}" + " To " + resourceDeployDir); 
            }

             if(DirectoryExists($"{artificatsOutputDir}/{levelScriptsPack.Name}"))
            { 
                CopyDirectory($"{artificatsOutputDir}/{levelScriptsPack.Name}", resourceDeployDir); 
                Information($"Copied {artificatsOutputDir}/{levelScriptsPack.Name}" + " To " + resourceDeployDir); 
            }

            
            // Copy any working data files to the resource, data should probably be its own repo
            if(DirectoryExists($"{resourceDataDir}/{resourceLoaderPack.Name}"))
            { 
                CopyDirectory($"{resourceDataDir}/{resourceLoaderPack.Name}", resourceDeployDir); 
	            Information($"Copied {resourceDataDir}/{resourceLoaderPack.Name}" + " To " + resourceDeployDir);
            }

             if(DirectoryExists($"{moduleDataDir}/{modulesPack.Name}"))
            { 
                CopyDirectory($"{moduleDataDir}/{modulesPack.Name}", resourceDeployDir); 
	            Information($"Copied {moduleDataDir}/{modulesPack.Name}" + " To " + resourceDeployDir);
            }

            //  if(DirectoryExists($"{dataDir}/{levelScriptsPack.Name}"))
            // { 
            //     CopyDirectory($"{dataDir}/{levelScriptsPack.Name}", resourceDeployDir); 
	        //     Information($"Copied {dataDir}/{levelScriptsPack.Name}" + " To " + resourceDeployDir);
            // }


            // Delete this to avoid causing issues with loading and running the .net libraries
            DeleteFiles(resourceDeployDir + "/CitizenFX.Core.*.dll");
	        Information($"Deleted " + resourceDeployDir + "/CitizenFX.Core.*.dll");
});

// //////////////////////////////////////////////////////////////////////
// // EXECUTION
// //////////////////////////////////////////////////////////////////////

RunTarget(target);