<img src="https://travis-ci.org/jamilgeor/PackScan.svg?branch=master" alt="Build Status"/>

# Pack Scan

CLI tool for scanning NuGet package dependancies for known vulnerabilities.

Pack Scan makes use of the Sonatype OSS Index API to check your projects NuGet package dependancies which are known to have security vulnerabilities.

### INSTALL
```
dotnet tool install -g packscan
```

### SYNOPSIS         

packscan [-af] [file|nuget]

### DESCRIPTION

List all nuget package dependancies, and their vulnerability status, including information about the vulnerabilities.

The following options are available.

<table>
  <tr>
    <td>-f, --file</td><td>either a .csproj or packages.config file containing nuget references.</td>
  </tr>
  <tr>
    <td>-v, --verbose</td><td>display verbose output, including vulnerability description and references.</td>
  </tr>
</table>

Exit status:<br/>
<table>
  <tr>
    <td>0</td><td>if OK,</td>
  </tr>
  <tr>
    <td>1</td><td>if packages contain a vulnerability</td>
  </tr>
</table>

Examples of use:<br/>
List all vulnerability statuses for packages in specified file.<br/>
```
packscan -f packages.config
```
List vulnerability status for specified package/version.<br/>
```
packscan LibGit2Sharp@0.2.0 -v
```

### AUTHOR

Written by Jamil Geor

### COPYRIGHT

Copyright © 2018 Jamil Geor. This is free software: you are free to change and redistribute it. There is NO WARRANTY, to the extent permitted by law.
