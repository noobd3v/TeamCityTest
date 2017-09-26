#tool "nuget:?package=OpenCover"

var target = Argument("target", "Clean");

Task("Default")
  .IsDependentOn("Generate-Artifacts");
  
Task("Clean")
	.Does(() =>
{
	MSBuild("./TeamCityTest/TeamCityTest.csproj", new MSBuildSettings()
		  .WithTarget("Clean"));
});

Task("Restore")
  .IsDependentOn("Clean")
  .Does(() =>
  {
    NuGetRestore("TeamCityTest.sln");
  });

Task("Build")
	.IsDependentOn("Restore")
  .Does(() =>
{
  MSBuild("TeamCityTest.sln");
});

Task("Generate-Artifacts")
	.IsDependentOn("Coverage-Report")
	.Does(() =>
	{
		MSBuild("./TeamCityTest/TeamCityTest.csproj", new MSBuildSettings()
		  .WithProperty("DeployOnBuild", "true")
		  .WithProperty("WebPublishMethod", "Package")
		  .WithProperty("PackageAsSingleFile", "true")
		  .WithProperty("SkipInvalidConfigurations", "true"));
	});

Task("msTest")
  .IsDependentOn("Build")
    .Does(() =>
{
  MSTest("./UnitTest/bin/Debug/UnitTest.dll");
});

Task("Coverage-Report")
	.IsDependentOn("msTest")
		.Does(() =>
		{
			OpenCover(tool => {
			  tool.MSTest("./UnitTest/bin/Debug/UnitTest.dll");
			  },
			  new FilePath("./coverage.xml"),
			  new OpenCoverSettings()
				.WithFilter("+[TeamCityTest]*"));
		});
RunTarget(target);