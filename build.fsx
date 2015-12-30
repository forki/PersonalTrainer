#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile
open Fake.Git

let version = "0.9.0"

let buildDir = "./build/"
let commitHash = Information.getCurrentSHA1 ".git"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "SetVersions" (fun _ ->
    CreateCSharpAssemblyInfo "./src/CommonAssemblyInfo.cs"
        [Attribute.Product "PERSONAL TRAINER"
         Attribute.Copyright "Copyright (c) 2015, 2106 Figroll"
         Attribute.Version version
         Attribute.FileVersion version
         Attribute.Trademark commitHash
         Attribute.ComVisible false]
)

Target "BuildApp" (fun _ ->
    !! "src/**/*.csproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "BuildApp-Output: "
)

"Clean"
  ==> "SetVersions"
  ==> "BuildApp"

RunTargetOrDefault "BuildApp"