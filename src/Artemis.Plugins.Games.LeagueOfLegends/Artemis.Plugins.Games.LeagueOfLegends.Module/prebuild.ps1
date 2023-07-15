# List of filenames to download
$fileNames = @("champion.json", "item.json", "map.json", "runesReforged.json")

# Get the path to the folder
$folder = Join-Path $PSScriptRoot "gen-data"

# Create the folder if it doesn't exist
if (!(Test-Path $folder)) { New-Item -ItemType Directory -Path $folder }

# Get the path to the files
$filePaths = $fileNames | ForEach-Object { Join-Path $folder $_ }

# Make HTTP request to retrieve versions
$versionsString = Invoke-RestMethod -Uri "https://ddragon.leagueoflegends.com/api/versions.json"

# Deserialize the JSON response
$versions = $versionsString -split '\s+'

# Get the latest version
$latestVersion = $versions[0]

# Check if files exist and get their versions
foreach ($filePath in $filePaths) {
    if (Test-Path $filePath) {
        $fileVersion = (Get-Content $filePath | ConvertFrom-Json).version
    }
    else{
        $fileVersion = ""
    }

    if ($fileVersion -ne $latestVersion) {
        # Delete the file if it exists
        if (Test-Path $filePath) { Remove-Item $filePath }
        # Download the file
        $fileName = $filePath | Split-Path -Leaf
        Invoke-WebRequest -Uri "https://ddragon.leagueoflegends.com/cdn/$latestVersion/data/en_US/$fileName" -OutFile $filePath
    }
}