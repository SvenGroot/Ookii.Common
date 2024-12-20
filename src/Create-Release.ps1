param(
    [Parameter()][Switch]$PackageOnly,
    [Parameter()][string]$LocalNugetPath
)

# This script is used to create a distribution folder that can be packaged into a zip file for release.
$projectName = "Ookii.Common"
$projectDir = Join-Path $PSScriptRoot $projectName
$publishDir = Join-Path $projectDir "bin" "publish"
$zipDir = Join-Path $publishDir "zip"
New-Item $publishDir -ItemType Directory -Force | Out-Null
Remove-Item "$publishDir/*" -Recurse -Force
New-Item $zipDir -ItemType Directory -Force | Out-Null

[xml]$project = Get-Content (Join-Path $PSScriptRoot "$projectName/$projectName.csproj")
$frameworks = $project.Project.PropertyGroup.TargetFrameworks -split ";"
[xml]$props = Get-Content (Join-Path $PSScriptRoot "Directory.Build.Props")
$versionPrefix = $props.Project.PropertyGroup.VersionPrefix
$versionSuffix = $props.Project.PropertyGroup.VersionSuffix
if ($versionSuffix) {
    $version = "$versionPrefix-$versionSuffix"
} else {
    $version = $versionPrefix
}


# Clean and build deterministic.
dotnet clean "$PSScriptRoot" --configuration release
dotnet build "$PSScriptRoot" --configuration release /p:ContinuousIntegrationBuild=true

if (-not $PackageOnly) {
    # Publish each version of the library.
    foreach ($framework in $frameworks) {
        if ($framework) {
            dotnet publish --no-build "$PSScriptRoot/$projectName" --configuration Release --framework $framework --output "$zipDir/$framework"
        }
    }

    # Copy global items.
    Copy-Item "$PSScriptRoot/../license.txt" $zipDir

    # Create readme.txt files.
    $url = "https://github.com/SvenGroot/$projectName"
    "$projectName $version","For documentation and other information, see:",$url | Set-Content "$zipDir/readme.txt"

    Compress-Archive "$zipDir/*" "$publishDir/$projectName$version.zip"
}

# Copy packages
dotnet pack "$projectDir" --no-build --configuration Release --output "$publishDir"

if ($LocalNugetPath) {
    Copy-Item "$publishDir\*.nupkg" $LocalNugetPath
    Copy-Item "$publishDir\*.snupkg" $LocalNugetPath
    Remove-Item -Recurse "~/.nuget/packages/$($projectName.ToLower())/*"
}