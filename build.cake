
var target = Argument("target", "Deploy");
var platform = "bin";
var deployment = "Release"; 
var configuration = Argument("configuration", $"{deployment}");
var version = Argument("packageVersion", "0.0.1");
var prerelease = Argument("prerelease", "");

var commonDir = "./code/common";
var resourceDir = "./code/resources";
var componentDir = "./code/components";
var libDir = "./code/libs";
var toolsDir = "./code/tools";

// a full build would be to build the common first, libs second, resources third, components fourth, tools fifth

var deployDir = "H:/servers/Game_Servers/FiveM/core";
var resourceOutputDir = $"{deployDir}/resources";
var toolsOutputDir = $"{deployDir}/tools";
var artificatsOutputDir = "./artifacts";
var componentOutputDir = "H:/components";


class ProjectInformation
{
    public int ProjectType {get; set;}
    public string OutputDir {get ;set;}
    public string Name { get; set; }
    public string FullPath { get; set; }
    public bool IsTestProject { get; set; }
}

string packageVersion;
List<ProjectInformation> resources;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");

    packageVersion = $"{version}{prerelease}";

    resources = GetDirectories(resourceDir+ "/C*").Select(p => new ProjectInformation
    {
        OutputDir = $"{resourceOutputDir}/"+p.GetDirectoryName(),
        Name = p.GetDirectoryName(),
        FullPath = p.FullPath,
        ProjectType = 2,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    }).ToList();
    resources.AddRange(GetDirectories(libDir+ "/*").Select(p => new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+p.GetDirectoryName(),
        Name = p.GetDirectoryName(),
        FullPath = p.FullPath,
        ProjectType = 1,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    }).ToList());

    resources.AddRange(GetDirectories(commonDir+ "/*").Select(p => new ProjectInformation
    {
        OutputDir = $"{artificatsOutputDir}/"+p.GetDirectoryName(),
        Name = p.GetDirectoryName(),
        FullPath = p.FullPath,
        ProjectType = 0,
        //IsTestProject = p.GetFilenameWithoutExtension().ToString().EndsWith("Tests")
    }).ToList());

    resources = resources.OrderBy(e=>e.ProjectType).ToList();

    foreach(var x in resources)
    {
	    Information(x.Name);
	    Information(x.FullPath);
        Information(x.OutputDir);
    }
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
    foreach(var x in resources)
    {
	    Information("Cleaning " + x.OutputDir);
        CleanDirectory(x.OutputDir);
        //CleanDirectory($"{x.FullPath}/src/Proline.{x.Name}.Component/bin/{deployment}");
    } 
});

Task("Restore") 
    .IsDependentOn("Clean")
    .Does(() =>
{
    foreach(var x in resources)
    {
        DotNetRestore(x.FullPath + "/src");
    }
});

Task("Build")
    .IsDependentOn("Restore")
    .ContinueOnError()
    .Does(() =>
{
    foreach(var x in resources)
    {
        if(x.ProjectType == 2)
        {
            CakeExecuteScript($"{x.FullPath}/build.cake");
        }
        else
        {
            DotNetBuild(x.FullPath + "/src", new DotNetBuildSettings
            {
                Configuration = configuration,  
                OutputDirectory = x.OutputDir,
                NoRestore = true
            });
        } 
    }
})
.DeferOnError();

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    foreach(var x in resources)
    {
        DotNetTest(x.FullPath + "/src", new DotNetTestSettings
        {
            Configuration = configuration,
            NoBuild = true,
        });
    }
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
{ 
    foreach(var x in resources)
    {
        if(x.ProjectType == 2)
        { 
            //var inputPath = $"{x.FullPath}/src/Proline.{x.Name}.Component/{platform}/{deployment}";
	        //Information("Copying " + inputPath + " To " + x.OutputDir);
            //CopyDirectory($"{x.FullPath}/src/Proline.{x.Name}.Component/{platform}/{deployment}", x.OutputDir);
            CopyFile($"{x.FullPath}/component.json", x.OutputDir + "/component.json"); 
            CopyDirectory($"{x.FullPath}/data", x.OutputDir + "/data"); 
            CopyDirectory($"{artificatsOutputDir}/ResourceFramework/", x.OutputDir); 
            DeleteFiles(x.OutputDir + "/CitizenFX.Core.*.dll");
        }
    }
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);