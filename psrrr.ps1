# ----------------------------
# Path to the project
# ----------------------------
$csprojPath = "D:\programming\MyWork\MAINSELFPROJECT\His\src\HIS.Api.csproj"

# ----------------------------
# Find all empty folders in the project
# ----------------------------
$projectFolder = Split-Path $csprojPath
$emptyFolders = Get-ChildItem -Path $projectFolder -Recurse -Directory | Where-Object { ($_ | Get-ChildItem) -eq $null }

# ----------------------------
# Create Folder entries
# ----------------------------
$folderEntries = foreach ($f in $emptyFolders) {
    $relativePath = $f.FullName.Substring($projectFolder.Length + 1) -replace '\\','/' # relative path with forward slashes
    "    <Folder Include=`"$relativePath/`" />"
}

# ----------------------------
# Insert into .csproj
# ----------------------------
if ($folderEntries.Count -gt 0) {
    $lines = Get-Content $csprojPath
    $index = $lines.LastIndexOf("</Project>")
    $lines = $lines[0..($index-1)] + "<ItemGroup>" + $folderEntries + "</ItemGroup>" + $lines[$index..($lines.Count-1)]
    Set-Content $csprojPath $lines
    Write-Host "✅ Added $($folderEntries.Count) empty folders to $csprojPath"
} else {
    Write-Host "No empty folders found."
}
