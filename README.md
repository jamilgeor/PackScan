# Pack Scan
CLI tool for scanning NuGet package dependancies for known vulnerabilities.

Pack Scan makes use of the Sonatype OSS Index API to check your projects NuGet package dependancies which are known to have security vulnerabilities.

## Install
```
dotnet tool install -g packscan
```

## Run
```
packscan -f myproject.csproj
packscan -f packages.config
```
