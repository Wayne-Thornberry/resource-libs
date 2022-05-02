
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

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");

    packageVersion = $"{version}{prerelease}"; 
    var dir = Context.Environment.WorkingDirectory;
    outPutDir = $"{artificatsOutputDir}/"+dir.GetDirectoryName();
    resourceName = dir.GetDirectoryName();

    var moduleLoader = resourceDir+"/ModualLoader";
    var classicOnline =  modulesDir+"/ClassicOnline";
    var levelscripts = levelscriptDir+"/ClassicScripts";

    var dirPaht = new DirectoryPath(moduleLoader);
    var dirPaht2 = new DirectoryPath(classicOnline);
    var dirPaht3 = new DirectoryPath(levelscripts);

    resourceLoaderPack = new ProjectInformation
    {
        OutputDir = outPutDir,
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
        OutputDir = outPutDir,
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
        OutputDir = outPutDir,
        Name = dirPaht3.GetDirectoryName(),
        FullPath = levelscripts,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };

    Information(levelScriptsPack.Name);
    Information(levelScriptsPack.FullPath);
    Information(levelScriptsPack.OutputDir);
    
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
 
Task("Clean") 
    //.WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
	    Information("Cleaning " + outPutDir);
        CleanDirectory(outPutDir);
});

Task("Restore") 
    .IsDependentOn("Clean")
    .Does(() =>
{
        DotNetRestore(levelScriptsPack.FullPath);
        DotNetRestore(modulesPack.FullPath);
        DotNetRestore(resourceLoaderPack.FullPath);
});

Task("Build")
    .IsDependentOn("Restore")
    .ContinueOnError()
    .Does(() =>
{
        DotNetBuild(levelScriptsPack.FullPath, new DotNetBuildSettings
        {
            Configuration = configuration,  
            OutputDirectory = levelScriptsPack.OutputDir,
            NoRestore = true
        });
        DotNetBuild(modulesPack.FullPath, new DotNetBuildSettings
        {
            Configuration = configuration,  
            OutputDirectory = modulesPack.OutputDir,
            NoRestore = true
        });
        DotNetBuild(resourceLoaderPack.FullPath, new DotNetBuildSettings
        {
            Configuration = configuration,  
            OutputDirectory = resourceLoaderPack.OutputDir,
            NoRestore = true
        });
})
.DeferOnError();

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
      DotNetTest(levelScriptsPack.FullPath, new DotNetTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
        });
    DotNetTest(modulesPack.FullPath, new DotNetTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
        });
    DotNetTest(resourceLoaderPack.FullPath, new DotNetTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
        });
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
{ 
            var resourceDeployDir = $"{resourceOutputDir}/{resourceName}";
            CleanDirectory(resourceDeployDir);
	        Information("Cleaned " + resourceDeployDir);
 
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

            CopyDirectory($"{artificatsOutputDir}/{resourceName}", resourceDeployDir); 
	        Information($"Copied {artificatsOutputDir}/{resourceName}" + " To " + resourceDeployDir); 

            // Delete this to avoid causing issues with loading and running the .net libraries
            DeleteFiles(resourceDeployDir + "/CitizenFX.Core.*.dll");
	        Information($"Deleted " + resourceDeployDir + "/CitizenFX.Core.*.dll");
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);