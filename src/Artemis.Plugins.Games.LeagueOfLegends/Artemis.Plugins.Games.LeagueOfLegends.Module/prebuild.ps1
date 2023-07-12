# Write the data to the source generator directory
$targetDirectory = $PSScriptRoot

# Make HTTP request to retrieve versions
$versionsString = Invoke-RestMethod -Uri "https://ddragon.leagueoflegends.com/api/versions.json"

# Deserialize the JSON response
$versions = $versionsString -split '\s+'

# Get the latest version
$latestVersion = $versions[0]

# Get the path to the files
$championsPath = Join-Path $targetDirectory "champion.json"
$itemsPath = Join-Path $targetDirectory "item.json"

# Delete the files if they exist
if (Test-Path $championsPath) { Remove-Item $championsPath }
if (Test-Path $itemsPath) { Remove-Item $itemsPath }

# Download the files
Invoke-WebRequest -Uri "https://ddragon.leagueoflegends.com/cdn/$latestVersion/data/en_US/champion.json" -OutFile (Join-Path $targetDirectory "champion.json")
Invoke-WebRequest -Uri "https://ddragon.leagueoflegends.com/cdn/$latestVersion/data/en_US/item.json" -OutFile (Join-Path $targetDirectory "item.json")

