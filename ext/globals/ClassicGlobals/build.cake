
var target = Argument("target", "Deploy");
var platform = "bin";
var deployment = "Release"; 
var configuration = Argument("configuration", $"{deployment}");
var version = Argument("packageVersion", "0.0.1");
var prerelease = Argument("prerelease", "");

var commonDir = "./../code/common";
var resourceDir = "./../code/resources";
var componentDir = "./../code/components";
var libDir = "./../../../libs";
var dataDir = "./../../../../data/resources/client";
var toolsDir = "./../code/tools";
var resourceFramework = "ResourceFramework";

// a full build would be to build the common first, libs second, resources third, components fourth, tools fifth

var deployDir = "E:/servers/Game_Servers/FiveM/core";
var resourceOutputDir = $"{deployDir}/resources";
var artificatsOutputDir = "./../../../artifacts";

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
ProjectInformation resource;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");

    packageVersion = $"{version}{prerelease}"; 
    var dir = Context.Environment.WorkingDirectory;

    resource = new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+dir.GetDirectoryName(),
        Name = dir.GetDirectoryName(),
        FullPath = dir.FullPath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    };
    

	    Information(resource.Name);
	    Information(resource.FullPath);
        Information(resource.OutputDir);
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
	    Information("Cleaning " + resource.OutputDir);
        CleanDirectory(resource.OutputDir);
});

Task("Restore") 
    .IsDependentOn("Clean")
    .Does(() =>
{
        DotNetRestore(resource.FullPath);
});

Task("Build")
    .IsDependentOn("Restore")
    .ContinueOnError()
    .Does(() =>
{
        DotNetBuild(resource.FullPath, new DotNetBuildSettings
        {
            Configuration = configuration,  
            OutputDirectory = resource.OutputDir,
            NoRestore = true
        });
})
.DeferOnError();

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
      DotNetTest(resource.FullPath, new DotNetTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
        });
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
{ 
    // if(resource.ProjectType == 2)
    //     { 
    //         var resourceDeployDir = $"{resourceOutputDir}/{resource.Name}";
    //         CleanDirectory(resourceDeployDir);
	//         Information("Cleaned " + resourceDeployDir);
 
    //         // Copy any working data files to the resource, data should probably be its own repo
    //         if(DirectoryExists($"{dataDir}/{resource.Name}"))
    //         { 
    //             CopyDirectory($"{dataDir}/{resource.Name}", resourceDeployDir); 
	//             Information($"Copied {dataDir}/{resource.Name}" + " To " + resourceDeployDir);
    //         }

    //         CopyDirectory($"{artificatsOutputDir}/{resource.Name}", resourceDeployDir); 
	//         Information($"Copied {artificatsOutputDir}/{resource.Name}" + " To " + resourceDeployDir); 

    //         // Delete this to avoid causing issues with loading and running the .net libraries
    //         DeleteFiles(resourceDeployDir + "/CitizenFX.Core.*.dll");
	//         Information($"Deleted " + resourceDeployDir + "/CitizenFX.Core.*.dll");
    //     }
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);