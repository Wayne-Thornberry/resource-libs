
var target = Argument("target", "Deploy");
var platform = "bin"; 
var configuration = Argument("configuration", "release");
var version = Argument("packageVersion", "0.0.1");
var prerelease = Argument("prerelease", "");

var commonDir = "./code/common";
var resourceDir = "./code/resources";
var componentDir = "./code/components";
var libDir = "./code/libs";
var toolsDir = "./code/tools";

// a full build would be to build the common first, libs second, resources third, components fourth, tools fifth

var artificatsOutputDir = "./artifacts";


class ProjectInformation
{
    public int ProjectType {get; set;}
    public string OutputDir {get ;set;}
    public string Name { get; set; }
    public string FullPath { get; set; }
    public bool IsTestProject { get; set; }
}

string packageVersion;
string outputDir;
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
    outputDir = $"{artificatsOutputDir}";///{dir.GetDirectoryName()}/";

    resource = new ProjectInformation
    { 
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
	    Information("Cleaning " + outputDir);
        CleanDirectory(outputDir);
});

Task("Restore") 
    .IsDependentOn("Clean")
    .Does(() =>
{
       //DotNetRestore(resource.FullPath);
});

Task("Build")
    .IsDependentOn("Restore")
    .ContinueOnError()
    .Does(() =>
{
        DotNetBuild(resource.FullPath, new DotNetBuildSettings
        {
            Configuration = configuration,  
            OutputDirectory = outputDir
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
  
  
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);