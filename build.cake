var target = Argument("target", "Build");

Task("Default")
  .IsDependentOn("msTest");

Task("Build")
  .Does(() =>
{
  MSBuild("TeamCityTest.sln");
});

Task("msTest")
  .IsDependentOn("Build")
    .Does(() =>
{
  MSTest("./UnitTest/bin/Debug/UnitTest.dll");
});

RunTarget(target);